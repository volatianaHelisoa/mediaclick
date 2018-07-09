using RedactApplication.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RedactApplication.Controllers
{

    public class TemplateController : Controller
    {

        private redactapplicationEntities db = new redactapplicationEntities();
        public static Guid _userId;
        // GET: Template
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListTemplate()
        {

            return View();

        }
        public ActionResult Theme1()
        {
           
            return View();
        }

        private string SavePhoto(HttpPostedFileBase file)
        {
            string path = Server.MapPath("~/images/Themes/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (file != null)
            {
                string fileName = Path.GetFileName(file.FileName);
                file.SaveAs(path + fileName);
                return  "/images/Logo/" + fileName;
            }
            return "";
        }

        [HttpPost]
        public ActionResult SaveTheme(MODELEViewModel  model, HttpPostedFileBase logoUrl, 
            HttpPostedFileBase menu1_paragraphe1_photoUrl, HttpPostedFileBase menu1_paragraphe2_photoUrl,
            HttpPostedFileBase menu2_paragraphe1_photoUrl, HttpPostedFileBase menu2_paragraphe2_photoUrl,
            HttpPostedFileBase menu3_paragraphe1_photoUrl, HttpPostedFileBase menu3_paragraphe2_photoUrl,
           HttpPostedFileBase menu4_paragraphe1_photoUrl, HttpPostedFileBase menu4_paragraphe2_photoUrl,
           HttpPostedFileBase photoALaUneUrl)
        {

            string path = Server.MapPath("~/images/Themes/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            MODELE newmodel = new MODELE();
            newmodel.logoUrl = SavePhoto(logoUrl);
            
            /*Menu 1 */

            newmodel.menu1_titre = model.menu1_titre;

            newmodel.menu1_paragraphe1_titre = model.menu1_paragraphe1_titre;
            newmodel.menu1_paragraphe1_photoUrl = SavePhoto(menu1_paragraphe1_photoUrl);
            newmodel.menu1_contenu1 = model.menu1_contenu1;

            newmodel.menu1_paragraphe2_titre = model.menu1_paragraphe2_titre;
            newmodel.menu1_paragraphe2_photoUrl = SavePhoto(menu1_paragraphe2_photoUrl);
            newmodel.menu1_contenu2 = model.menu1_contenu2;

            /*Menu 2 */
            newmodel.menu2_titre = model.menu2_titre;                     
            
            newmodel.menu2_paragraphe1_titre = model.menu2_paragraphe1_titre;
            newmodel.menu2_paragraphe2_titre = model.menu2_paragraphe2_titre;
            newmodel.menu2_paragraphe1_photoUrl = SavePhoto(menu2_paragraphe1_photoUrl); ;
            newmodel.menu2_paragraphe2_photoUrl = SavePhoto(menu2_paragraphe2_photoUrl); ;
            newmodel.menu2_contenu1 = model.menu2_contenu1;
            newmodel.menu2_contenu2 = model.menu2_contenu2;

            /*Menu 3 */    
            newmodel.menu3_titre = model.menu3_titre;

            newmodel.menu3_paragraphe1_titre = model.menu3_paragraphe1_titre;
            newmodel.menu3_paragraphe2_titre = model.menu3_paragraphe2_titre;
            newmodel.menu3_paragraphe1_photoUrl = SavePhoto(menu3_paragraphe1_photoUrl); ;
            newmodel.menu3_paragraphe2_photoUrl = SavePhoto(menu3_paragraphe2_photoUrl); ;
            newmodel.menu3_contenu1 = model.menu3_contenu1;
            newmodel.menu3_contenu2 = model.menu3_contenu2;

            /*Menu 4 */
                    
            newmodel.menu4_titre = model.menu4_titre;
                        
            newmodel.menu4_paragraphe1_titre = model.menu4_paragraphe1_titre;
            newmodel.menu4_paragraphe2_titre = model.menu4_paragraphe2_titre;
            newmodel.menu4_paragraphe1_photoUrl = SavePhoto(menu4_paragraphe1_photoUrl);
            newmodel.menu4_paragraphe2_photoUrl = SavePhoto(menu4_paragraphe2_photoUrl);
            newmodel.menu4_contenu1 = model.menu4_contenu1;
            newmodel.menu4_contenu2 = model.menu4_contenu2;


            /*A la une*/
            newmodel.photoALaUneUrl = SavePhoto(photoALaUneUrl);

            newmodel.modeleId = new Guid();
            Session["modeleId"] = newmodel.modeleId;

            db.MODELEs.Add(newmodel);
            try
            {
                int res = db.SaveChanges();
                if (res > 0)
                    return View("CreateTemplate");
                else
                    return View("ErrorException");
            }
            catch(Exception ex)
            {
                return View("Theme1");
            }              
        }


        public ActionResult CreateTemplate()
        {
            Templates val = new Templates();
            TEMPLATEViewModel templateVm = new TEMPLATEViewModel();
            templateVm.ListProjet = val.GetListProjetItem();
            templateVm.ListTheme = val.GetListThemeItem();
           
            return View(templateVm);
        }



        public ActionResult Theme2()
        {

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

            TEMPLATE newtemplate = new TEMPLATE();
            newtemplate.dateCreation = DateTime.Now;
            newtemplate.ftpUser = model.ftpUser;
            newtemplate.ftpPassword = model.ftpPassword;
            newtemplate.modeleId = (Guid)Session["modeleId"];

            newtemplate.userId = _userId;
            newtemplate.PROJET = db.PROJETS.Find(selectedProjetId);
            newtemplate.projetId = selectedProjetId;
            newtemplate.THEME = db.THEMES.Find(selectedThemeId);
            newtemplate.themeId = selectedThemeId;
            newtemplate.templateId = new Guid();

            db.TEMPLATEs.Add(newtemplate);
            try
            {
                res = db.SaveChanges();
                if (res > 0)
                    return View("ListTemplate");
               else
                    return View("ErrorException");
            }
            catch (Exception ex)
            {
                return View("Theme1");
            
            }

            

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

            return View();
        }

        //[HttpPost]
        //public ActionResult SaveTheme3(TEMPLATEViewModel model, IEnumerable<HttpPostedFileBase> files)
        //{
        //    var path = "";
        //    foreach (var file in files)
        //    {
        //        if (file.ContentLength > 0)
        //        {
        //            var fileName = Path.GetFileName(file.FileName);
        //            path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
        //            file.SaveAs(path);

        //        }
        //    }
        //    MODELE3 newmodel = new MODELE3();
        //    newmodel.logoUrl = path;
        //    newmodel.menu1_titre = model.menu1_titre;
        //    newmodel.menu2_titre = model.menu2_titre;
        //    newmodel.menu3_titre = model.menu3_titre;
        //    newmodel.menu4_titre = model.menu4_titre;
        //    newmodel.menu1_photoALaUneUrl = path;
        //    newmodel.menu1_paragraphe1_titre = model.menu1_paragraphe1_titre;
        //    newmodel.menu1_paragraphe2_titre = model.menu1_paragraphe2_titre;
        //    newmodel.menu1_paragraphe_photoUrl = path;
        //    newmodel.menu1_contenu1 = model.menu1_contenu1;
        //    newmodel.menu1_contenu2 = model.menu1_contenu2;
        //    newmodel.menu2_photoALaUneUrl = path;
        //    newmodel.menu2_paragraphe1_titre = model.menu2_paragraphe1_titre;
        //    newmodel.menu2_paragraphe2_titre = model.menu2_paragraphe2_titre;
        //    newmodel.menu2_paragraphe_photoUrl = path;
        //    newmodel.menu2_contenu1 = model.menu2_contenu1;
        //    newmodel.menu2_contenu2 = model.menu2_contenu2;
        //    newmodel.menu3_photoALaUneUrl = path;
        //    newmodel.menu3_paragraphe1_titre = model.menu3_paragraphe1_titre;
        //    newmodel.menu3_paragraphe2_titre = model.menu3_paragraphe2_titre;
        //    newmodel.menu3_paragraphe_photoUrl = path;
        //    newmodel.menu3_contenu1 = model.menu3_contenu1;
        //    newmodel.menu3_contenu2 = model.menu3_contenu2;
        //    newmodel.menu4_photoALaUneUrl = path;
        //    newmodel.menu4_paragraphe1_titre = model.menu4_paragraphe1_titre;
        //    newmodel.menu4_paragraphe2_titre = model.menu4_paragraphe2_titre;
        //    newmodel.menu4_paragraphe_photoUrl = path;
        //    newmodel.menu4_contenu1 = model.menu4_contenu1;
        //    newmodel.menu4_contenu2 = model.menu4_contenu2;

        //    db.MODELE3.Add(newmodel);
        //    try
        //    {
        //        db.SaveChanges();
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        return View();
        //    }
         

        //}

        public ActionResult Theme4()
        {

            return View();
        }

        //[HttpPost]
        //public ActionResult SaveTheme4(TEMPLATEViewModel model, IEnumerable<HttpPostedFileBase> files)
        //{
        //    var path = "";
        //    foreach (var file in files)
        //    {
        //        if (file.ContentLength > 0)
        //        {
        //            var fileName = Path.GetFileName(file.FileName);
        //            path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
        //            file.SaveAs(path);
        //        }
        //    }
        //    MODELE4 newmodel = new MODELE4();
        //    newmodel.logoUrl = path;
        //    newmodel.photoALaUneUrl = path;
        //    newmodel.paragraphe1_titre = model.menu1_paragraphe1_titre;
        //    newmodel.paragraphe2_titre = model.menu1_paragraphe2_titre;
        //    newmodel.paragraphe_photoUrl = path;
        //    newmodel.contenu1 = model.menu1_contenu1;
        //    newmodel.contenu2 = model.menu1_contenu2;

        //    db.MODELE4.Add(newmodel);
        //    try
        //    {
        //        db.SaveChanges();
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        return View();
        //    }
        //    return View();

        //    return View();

        //}
    }
}