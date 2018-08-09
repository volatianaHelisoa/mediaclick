using RedactApplication.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RedactApplication.Controllers
{

    public class TemplateController : Controller
    {

        private redactapplicationEntities db = new redactapplicationEntities();
        public static Guid _userId;
        // GET: Template
        public ActionResult Index()
        {
            var modeleId = Session["modeleId"];
            MODELEViewModel modelVm = new MODELEViewModel();
            modelVm = new Modeles().GetDetailsModele(Guid.Parse(modeleId.ToString()));

            return View(modelVm);
        }

        public ActionResult ListTemplate()
        {
            ViewBag.listeTemplateVm = new Templates().GetListTemplate();

            return View();

        }

        public ActionResult ChoiceTemplate()
        {
            return View();

        }
        

        public ActionResult Theme1()
        {
            Session["TemplateName"] = "Theme1";
            return View();
        }

        private string SavePhoto(HttpPostedFileBase file,string templateName)
        {
            string path = Server.MapPath("~/Themes/"+ templateName + "/img/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (file != null)
            {
                string fileName = Path.GetFileName(file.FileName);
                file.SaveAs(path + fileName);
                //return "/Themes/" + templateName + "/img/" + fileName;
                return "img/" + fileName;
            }
            return "";
        }

        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult SaveTheme(MODELEViewModel  model, HttpPostedFileBase logoUrl, 
            HttpPostedFileBase menu1_paragraphe1_photoUrl, HttpPostedFileBase menu1_paragraphe2_photoUrl,
            HttpPostedFileBase menu2_paragraphe1_photoUrl, HttpPostedFileBase menu2_paragraphe2_photoUrl,
            HttpPostedFileBase menu3_paragraphe1_photoUrl, HttpPostedFileBase menu3_paragraphe2_photoUrl,
           HttpPostedFileBase menu4_paragraphe1_photoUrl, HttpPostedFileBase menu4_paragraphe2_photoUrl,
           HttpPostedFileBase photoALaUneUrl, FormCollection collection)
        {
            var templateName = Session["TemplateName"].ToString();
            if (!string.IsNullOrEmpty(collection["nbmenu"]))
            {
                string nbmenu = collection["nbmenu"];
                Session["nbmenu"]  = nbmenu ;
            }
         

            string path = Server.MapPath("~/Themes/"+ templateName );
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            MODELE newmodel = new MODELE();
            newmodel.logoUrl = SavePhoto(logoUrl, templateName);
            
            /*Menu 1 */

            newmodel.menu1_titre = model.menu1_titre;

            newmodel.menu1_paragraphe1_titre = model.menu1_paragraphe1_titre;
            newmodel.menu1_paragraphe1_photoUrl = SavePhoto(menu1_paragraphe1_photoUrl, templateName);
            newmodel.menu1_contenu1 = model.menu1_contenu1;

            newmodel.menu1_paragraphe2_titre = model.menu1_paragraphe2_titre;
            newmodel.menu1_paragraphe2_photoUrl = SavePhoto(menu1_paragraphe2_photoUrl, templateName);
            newmodel.menu1_contenu2 = model.menu1_contenu2;

            /*Menu 2 */
            newmodel.menu2_titre = model.menu2_titre;                     
            
            newmodel.menu2_paragraphe1_titre = model.menu2_paragraphe1_titre;
            newmodel.menu2_paragraphe2_titre = model.menu2_paragraphe2_titre;
            newmodel.menu2_paragraphe1_photoUrl = SavePhoto(menu2_paragraphe1_photoUrl, templateName);
            newmodel.menu2_paragraphe2_photoUrl = SavePhoto(menu2_paragraphe2_photoUrl, templateName);
            newmodel.menu2_contenu1 = model.menu2_contenu1;
            newmodel.menu2_contenu2 = model.menu2_contenu2;

            /*Menu 3 */    
            newmodel.menu3_titre = model.menu3_titre;

            newmodel.menu3_paragraphe1_titre = model.menu3_paragraphe1_titre;
            newmodel.menu3_paragraphe2_titre = model.menu3_paragraphe2_titre;
            newmodel.menu3_paragraphe1_photoUrl = SavePhoto(menu3_paragraphe1_photoUrl, templateName);
            newmodel.menu3_paragraphe2_photoUrl = SavePhoto(menu3_paragraphe2_photoUrl, templateName);
            newmodel.menu3_contenu1 = model.menu3_contenu1;
            newmodel.menu3_contenu2 = model.menu3_contenu2;

            /*Menu 4 */
                    
            newmodel.menu4_titre = model.menu4_titre;
                        
            newmodel.menu4_paragraphe1_titre = model.menu4_paragraphe1_titre;
            newmodel.menu4_paragraphe2_titre = model.menu4_paragraphe2_titre;
            newmodel.menu4_paragraphe1_photoUrl = SavePhoto(menu4_paragraphe1_photoUrl, templateName);
            newmodel.menu4_paragraphe2_photoUrl = SavePhoto(menu4_paragraphe2_photoUrl, templateName);
            newmodel.menu4_contenu1 = model.menu4_contenu1;
            newmodel.menu4_contenu2 = model.menu4_contenu2;


            /*A la une*/
            newmodel.photoALaUneUrl = SavePhoto(photoALaUneUrl, templateName);
            newmodel.site_url = model.site_url;

            newmodel.modeleId = Guid.NewGuid();
            Session["modeleId"] = newmodel.modeleId;

            db.MODELEs.Add(newmodel);
            try
            {
                int res = db.SaveChanges();
                if (res > 0)
                {
                    Templates val = new Templates();
                    TEMPLATEViewModel templateVm = new TEMPLATEViewModel();
                    templateVm.ListProjet = val.GetListProjetItem();
                    templateVm.ListTheme = val.GetListThemeItem();

                    return View("CreateTemplate", templateVm);
                }
                  
                else
                    return View("ErrorException");
            }
            catch(Exception ex)
            {
                return View("Theme1");
            }              
        }


        public string RenderViewAsString(string viewName, object model)
        {
            // create a string writer to receive the HTML code
            StringWriter stringWriter = new StringWriter();

            // get the view to render
            ViewEngineResult viewResult = ViewEngines.Engines.FindView(ControllerContext, viewName, null);
            // create a context to render a view based on a model
            ViewContext viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    new ViewDataDictionary(model),
                    new TempDataDictionary(),
                    stringWriter
                    );

            // render the view to a HTML code
            viewResult.View.Render(viewContext, stringWriter);

            // return the HTML code
            return stringWriter.ToString();
        }


        public ActionResult CreateTemplate(Guid? hash)
        {
            Templates val = new Templates();
            TEMPLATEViewModel templateVm = new TEMPLATEViewModel();
            templateVm.ListProjet = val.GetListProjetItem();
            templateVm.ListTheme = val.GetListThemeItem();
            var modeleId = Session["modeleId"];
            MODELEViewModel modelVm = new MODELEViewModel();
            modelVm = new Modeles().GetDetailsModele(Guid.Parse(modeleId.ToString()));
            templateVm.url = modelVm.site_url;
            return View(templateVm);
        }

        public ActionResult Home()
        {
            var modeleId = Session["modeleId"];
            MODELEViewModel modelVm = new MODELEViewModel();
            modelVm = new Modeles().GetDetailsModele(Guid.Parse(modeleId.ToString()));

            return View(modelVm);
        }


        public ActionResult Theme2()
        {
            Session["TemplateName"] = "Theme2";
            return View();
        }


        public ActionResult SaveTemplate(TEMPLATEViewModel model)
        {
            int res = 0;       
            
            var selectedProjetId = model.listprojetId;
            var selectedThemeId = model.listThemeId;


            if (!string.IsNullOrEmpty(Request.QueryString["currentid"]))
            {
                _userId = Guid.Parse(Request.QueryString["currentid"]);
                Session["currentid"] = Request.QueryString["currentid"];
            }
            else if (!string.IsNullOrEmpty(HttpContext.User.Identity.Name))
            {
                _userId = Guid.Parse(HttpContext.User.Identity.Name);
            }

            MODELEViewModel modelVm = new MODELEViewModel();
            modelVm = new Modeles().GetDetailsModele((Guid)Session["modeleId"]);
            Session["Menu"] = "1";
            var html = RenderViewAsString("Home", modelVm);
           

            TEMPLATE newtemplate = new TEMPLATE();
            newtemplate.dateCreation = DateTime.Now;
            newtemplate.url = model.url;
            newtemplate.ftpUser = model.ftpUser;
            newtemplate.ftpPassword = model.ftpPassword;
            newtemplate.modeleId = (Guid)Session["modeleId"];

            newtemplate.userId = _userId;
            newtemplate.PROJET = db.PROJETS.Find(selectedProjetId);
            newtemplate.projetId = selectedProjetId;
            newtemplate.THEME = db.THEMES.Find(selectedThemeId);
            newtemplate.themeId = selectedThemeId;
            newtemplate.html = html;
            newtemplate.templateId = Guid.NewGuid();

            db.TEMPLATEs.Add(newtemplate);
            try
            {
                res = db.SaveChanges();
                if (res > 0)
                {
                    var templateName = Session["TemplateName"].ToString();

                    int nb_menu = (Session["nbmenu"] == null) ? 1 : int.Parse(Session["nbmenu"].ToString());                   

                    CreateFiles(nb_menu, html);

                    //Send Ftp
                    string pathParent = Server.MapPath("~/Themes/" + templateName);
                    string pathCss = pathParent + "/css/templates-style.css";
                    string pathImg = pathParent + "/img";
                    int result = SendToFtp(model.url, model.ftpUser, model.ftpPassword, pathCss, pathParent, pathImg);

                    //return new FilePathResult(path, "text/html");
                    if (result == 0)
                        return View("CreateTemplateConfirmation");
                    else
                        return View("ErrorException");
                }
                else
                    return View("ErrorException");
            }
            catch (Exception ex)
            {
                return View("ErrorException");
            
            }
        }

        /// <summary>
        /// Charge une liste d'utilisateur à supprimer dans la base de données.
        /// </summary>
        /// <param name="hash">List d'id d'utilisateur</param>
        /// <returns>bool</returns>
        [Authorize]
        [HttpPost]
        public bool SelecteAllTemplateToDelete(string hash)
        {
            try
            {
                // Récupère la liste des id d'utilisateur                
                Session["ListTemplateToDelete"] = hash;
                if (Session["ListTemplateToDelete"] != null)
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
        /// Supprime une liste Template dans la base de données.
        /// </summary>
        /// <returns>View</returns>
        [Authorize]
        public ActionResult DeleteAllTemplateSelected()
        {
            Guid userSession = new Guid(HttpContext.User.Identity.Name);


            try
            {
                bool unique = true;
                if (Session["ListTemplateToDelete"] != null)
                {
                    string hash = Session["ListTemplateToDelete"].ToString();
                    List<Guid> listIdTemplate = new List<Guid>();
                    if (!string.IsNullOrEmpty(hash))
                    {
                        if (!hash.Contains(','))
                        {
                            listIdTemplate.Add(Guid.Parse(hash));
                        }
                        else
                        {
                            foreach (var id in (hash).Split(','))
                            {
                                listIdTemplate.Add(Guid.Parse(id));
                            }
                            unique = false;
                        }
                    }
                    if (listIdTemplate.Count != 0)
                    {
                        redactapplicationEntities db = new Models.redactapplicationEntities();
                        foreach (var templateId in listIdTemplate)
                        {
                            //suppression des commandes
                            TEMPLATE template = db.TEMPLATEs.SingleOrDefault(x => x.templateId == templateId);
                            if (template != null) db.TEMPLATEs.Remove(template);
                        }
                        db.SaveChanges();

                        return View(unique ? "DeleteTemplateConfirmation" : "DeteleAllTemplateConfirmation");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return RedirectToRoute("Home", new RouteValueDictionary {
                { "controller", "Template" },
                { "action", "ListTemplate" }
            });
        }




        private void CreateFiles(int nb_menu, string menu1_html)
        {
            var templateName = Session["TemplateName"].ToString();
            string pathHtml = "";
            MODELEViewModel modelVm = new MODELEViewModel();
            modelVm = new Modeles().GetDetailsModele((Guid)Session["modeleId"]);
            var html = menu1_html;

            for (int i = 1; i <= nb_menu; i++)
            {
                pathHtml = "~/Themes/" + templateName;

                if (i == 1)
                    pathHtml = pathHtml + "/index.html";
                else
                    pathHtml = pathHtml + "/page" + i + ".html";

                Session["Menu"] = i;
                html = RenderViewAsString("Home", modelVm);


                if (!System.IO.File.Exists(Server.MapPath(pathHtml)))
                {
                    FileInfo info = new FileInfo(Server.MapPath(pathHtml));
                }

                using (StreamWriter w = new StreamWriter(Server.MapPath(pathHtml), false))
                {
                    w.WriteLine(html); // Write the text
                    w.Close();
                }
            }
        }

        private int SendToFtp(string ftpurl,string username,string password,string pathCss,string pathHtml,string pathImg)
        {
            int res = 0;
            try
            {
                /*Create directory*/
                FTP ftpClient = new FTP(@"ftp://" + ftpurl + "/", username, password);
                /* Create a New Directory */
                ftpClient.createDirectory("/css");
                ftpClient.createDirectory("/img");
                ftpClient = null;

                string[] htmlPaths = Directory.GetFiles(pathHtml, "*.html");    
                string[] imgPaths = Directory.GetFiles(pathImg, "*.jpg");

                using (WebClient client = new WebClient())
                {
                    client.Credentials = new NetworkCredential(username, password);
                    foreach (var html in htmlPaths)
                    {
                        client.UploadFile(
                            "ftp://" + ftpurl + "/" + Path.GetFileName(html), html);
                    }

                    foreach (var img in imgPaths)
                    {                       
                        client.UploadFile(
                            "ftp://" + ftpurl + "/img/" + Path.GetFileName(img), img);
                    }

                    client.UploadFile(
                           "ftp://" + ftpurl + "/css/" + Path.GetFileName(pathCss), pathCss);
                }
            }
            catch (Exception ex)
            {
                res = 1;
            }
            return res;
        }

        public int SaveTheme2(MODELEViewModel model, IEnumerable<HttpPostedFileBase> files)
        {
            int res = 0;
               var path = "";
            foreach (var file in files){
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                    file.SaveAs(path);
                }
            }
            MODELE newmodel = new  MODELE();
            newmodel.logoUrl = path;
            newmodel.menu1_titre = model.menu1_titre;
            newmodel.menu2_titre = model.menu2_titre;
            newmodel.menu3_titre = model.menu3_titre;
            newmodel.menu4_titre = model.menu4_titre;
            newmodel.menu1_paragraphe1_titre = model.menu1_paragraphe1_titre;
            newmodel.menu1_paragraphe2_titre = model.menu1_paragraphe2_titre;
            newmodel.menu1_paragraphe1_photoUrl = path;
            newmodel.menu1_contenu1 = model.menu1_contenu1;
            newmodel.menu2_paragraphe1_titre = model.menu2_paragraphe1_titre;
            newmodel.menu2_paragraphe2_titre = model.menu2_paragraphe2_titre;
            newmodel.menu2_paragraphe1_photoUrl = path;
            newmodel.menu2_contenu2 = model.menu2_contenu1;
            newmodel.menu3_paragraphe1_titre = model.menu3_paragraphe1_titre;
            newmodel.menu3_paragraphe2_titre = model.menu3_paragraphe2_titre;
            newmodel.menu3_paragraphe1_photoUrl = path;
            newmodel.menu3_contenu1 = model.menu3_contenu1;
            newmodel.menu4_paragraphe1_titre = model.menu4_paragraphe1_titre;
            newmodel.menu4_paragraphe2_titre = model.menu4_paragraphe2_titre;
            newmodel.menu4_paragraphe1_photoUrl = path;
            newmodel.menu4_contenu1 = model.menu4_contenu1;

            db.MODELEs.Add(newmodel);
            try
            {
                res = db.SaveChanges();
               
            }
            catch (Exception ex)
            {
                return res;
            }

            return res;

        }

        public ActionResult Theme3()
        {
            Session["TemplateName"] = "Theme3";
            return View();
        }

     

        public ActionResult Theme4()
        {
            Session["TemplateName"] = "Theme4";
            return View();
        }

      
    }
}