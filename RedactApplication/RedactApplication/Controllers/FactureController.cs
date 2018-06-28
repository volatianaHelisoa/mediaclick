using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using RedactApplication.Models;

namespace RedactApplication.Controllers
{
    public class FactureController : Controller
    {
        private redactapplicationEntities db = new redactapplicationEntities();
        private static Guid _userId;
        // GET: Facture
        public ActionResult Index()
        {
            ViewBag.listeFactureVm = new Factures().GetListFacture();

            return View();
        }

        public ActionResult ListFacture()
        {
            ViewBag.listeFactureVm = new Factures().GetListFacture();

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public FileResult Export(Guid? hash)
        {
            FACTURE fACTURE = db.FACTUREs.Find(hash);
            string GridHtml = "";
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader sr = new StringReader(GridHtml);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "Grid.pdf");
            }
        }

       

        public HtmlString theHtmlTableMomToldYouAbout(FACTURE facture)
        {
            var html = "<html lang = \"en\"><head><meta charset = \"utf-8\" >< title > Media Click </ title > ";
           
 
  
            
            
            return new HtmlString(html);
        }

        // GET: Facture/Details/5
        public ActionResult Details(Guid? hash)
        {
            if (hash == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var factureVm = new Factures().GetDetailsFacture(hash);
          
            return View(factureVm);
        }

        // GET: Facture/Create
        public ActionResult Create()
        {
            Factures val = new Factures();
            FACTUREViewModel factureVm = new FACTUREViewModel();
            factureVm.dateDebut = DateTime.Now.AddMonths(-1).AddDays(1);
            factureVm.dateFin = DateTime.Now;
            factureVm.ListRedacteur = val.GetListRedacteurItem(); 
            return View(factureVm);
        }

        // POST: Facture/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFacture(FACTUREViewModel model, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                 // Exécute le suivi de session utilisateur
                if (!string.IsNullOrEmpty(Request.QueryString["currentid"]))
                {
                    _userId = Guid.Parse(Request.QueryString["currentid"]);
                    Session["currentid"] = Request.QueryString["currentid"];
                }
                else                    
                    _userId = Guid.Parse(HttpContext.User.Identity.Name);

                var selectedRedacteurId = model.listRedacteurId;
                var newFacture = new FACTURE();
                newFacture.dateDebut = model.dateDebut;
                newFacture.dateFin = model.dateFin;
                newFacture.dateEmission = DateTime.Now;
                var commandesFacturer = db.COMMANDEs.Where(x => x.date_livraison >= model.dateDebut &&
                                                                 x.date_livraison <= model.dateFin && (x.STATUT_COMMANDE != null &&
                                                                 x.STATUT_COMMANDE.statut_cmde.Contains("Validé"))).ToList();
                
                var redacteur = db.UTILISATEURs.SingleOrDefault(x => x.userId == model.listRedacteurId);
                var volume = commandesFacturer.Sum(x => x.nombre_mots);
                double montant = Convert.ToDouble(volume.ToString())  * (Convert.ToDouble(redacteur.redactTarif));
                newFacture.montant = montant.ToString("0.0");
                newFacture.etat = false;
                newFacture.redacteurId = model.listRedacteurId;
                newFacture.createurId = _userId;
                newFacture.factureId = Guid.NewGuid();
                int maxRef = (db.FACTUREs.ToList().Count != 0) ? db.FACTUREs.Max(u => u.factureNumero): 0;
                newFacture.factureNumero = maxRef + 1;
                db.FACTUREs.Add(newFacture);

                foreach (var commande in commandesFacturer)
                {
                    commande.factureId = newFacture.factureId;
                }

                int res = db.SaveChanges();
                if (res > 0)
                    return RedirectToAction("ListFacture");               
                   
            }

            return View("ErrorException");
        }

      
        /// <summary>
        /// Charge une liste des factures à supprimer dans la base de données.
        /// </summary>
        /// <param name="hash">List d'id de facture</param>
        /// <returns>bool</returns>
        [Authorize]
        [HttpPost]
        public bool SelecteAllFactureToDelete(string hash)
        {
            try
            {
                // Récupère la liste des id d'utilisateur                
                Session["ListFactureToDelete"] = hash;
                if (Session["ListFactureToDelete"] != null)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return false;
        }

        /// <summary>
        /// Supprime une liste d'Utilisateur dans la base de données.
        /// </summary>
        /// <returns>View</returns>
        [Authorize]
        public ActionResult DeleteAllFactureSelected()
        {
            Guid userSession = new Guid(HttpContext.User.Identity.Name);

            ViewBag.userRole = (new Utilisateurs()).GetUtilisateurRoleToString(userSession);
            try
            {
                bool unique = true;
                if (Session["ListFactureToDelete"] != null)
                {
                    string hash = Session["ListFactureToDelete"].ToString();
                    List<Guid> listIdFacture = new List<Guid>();
                    if (!string.IsNullOrEmpty(hash))
                    {
                        if (!hash.Contains(','))
                        {
                            listIdFacture.Add(Guid.Parse(hash));
                        }
                        else
                        {
                            foreach (var id in (hash).Split(','))
                            {
                                listIdFacture.Add(Guid.Parse(id));
                            }
                            unique = false;
                        }
                    }
                    if (listIdFacture.Count != 0)
                    {
                        redactapplicationEntities db = new Models.redactapplicationEntities();
                        foreach (var factureId in listIdFacture)
                        {

                            //suppression des relations
                            //var commandes = db.COMMANDEs.Where(x => x.factureId == factureId);
                            //foreach (var cmde in commandes)
                            //{
                            //    cmde.factureId = null;
                            //    db.SaveChanges();
                            //}
                            //suppression des factures
                            FACTURE facture = db.FACTUREs.SingleOrDefault(x => x.factureId == factureId);
                            db.FACTUREs.Remove(facture);
                        }
                        db.SaveChanges();

                        if (unique)
                        {
                            return View("DeletedFactureConfirmation");
                        }
                        return View("DeletedAllFactureConfirmation");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return View("ListFacture");
        }

        /// <summary>
        /// Retourne la vue contenant la recherche d'Utilisateur.
        /// </summary>
        /// <param name="searchValue">Chaine de recherche</param>
        /// <returns>View</returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult FactureSearch(string searchValue)
        {
            if (searchValue != null && searchValue != "")
            {
                Session["Infosearch"] = searchValue;
            }
            else
            {
                return View("ListFacture");
            }

            redactapplicationEntities bds = new Models.redactapplicationEntities();
            Guid user = Guid.Parse(HttpContext.User.Identity.Name);
          
            Factures db = new Factures();
            var answer = db.SearchFacture(searchValue);
            if (answer == null || answer.Count == 0)
            {
                List<FACTUREViewModel> listeFacture = new List<FACTUREViewModel>();
                answer = listeFacture;
                ViewBag.SearchUserNoResultat = 1;
            }

            ViewBag.Search = true;
            redactapplicationEntities e = new redactapplicationEntities();
          
            List<FACTUREViewModel> listeDataFactureFiltered = new List<FACTUREViewModel>();

            ViewBag.listeFactureVm = answer;

            return View("ListFacture");
        }

        


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
