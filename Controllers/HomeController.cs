using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using MemeGenerator.Services;
using MemeGenerator.Models;

namespace MemeGenerator.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)

                try
                {
                    string path = Path.Combine(Server.MapPath("~/image"),
                      Path.GetFileName(file.FileName));
                    file.SaveAs(path);

                    ViewBag.ImageUrl = "../image/" + file.FileName;
                    // ViewBag.Message = "File Upload Successfully";

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Error:" + ex.Message.ToString();

                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }

            return View("Index");
        }

        public ActionResult Index(string ImageUrl = null)
        {
            if (!string.IsNullOrEmpty(ImageUrl))
            {
                ViewBag.ImageUrl = ImageUrl;
            }
            File_list fileList = FileService.GetFileList();

            return View(fileList);
        }



        //public ActionResult WriteText(ImageModification_Model model)
        //{
        //    string uploadFile = Server.MapPath("~/image");
        //    model.filePath = Path.Combine(uploadFile, model.fileName);
        //    FileService.WriteText(model);
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
        [Route("Meme/{FileName}")]
           
        public ActionResult meme(string FileName)
        {

            File_Model response = FileService.GetFileById(FileName);
            string filePath = Server.MapPath("~/image");
            string path = Path.Combine(filePath, response.filepath);
            return View("meme", response);
        }

        //public ActionResult custom()
        //{
        //    return PartialView("partial_custom");
        //}

        //public ActionResult contact()
        //{
        //    return View("Contact");
        //}

        public ActionResult Admin()
        {
            return View("View_Admin");
        }

        [HttpPost]
        public ActionResult Admin(LoginModel userCred)
        {
            FileService service = new FileService();
            string status=service.Login(userCred);
            if (status == "Success")
            {
                Session["login"] = status;
                AdminFileEditList response = service.AdminEditList();
                return View("View_AdminEditor", response);
            }
            else
            {
                return RedirectToAction("Admin");
            }

        }
        public ActionResult PrivacyPolicy()
        {
            return View("View_PrivacyPolicy");
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult custom()
        {
            return View();
        }

        public ActionResult AdminEditor()
        {
            if (Session["login"] == null)
                return RedirectToAction("Admin");
            FileService service = new FileService();
            AdminFileEditList response = service.AdminEditList();
            return View("View_AdminEditor", response);
        }

        public ActionResult EditDescription(string FileName)

        {
            File_Model response = FileService.GetFileById(FileName);
            return View("View_DescriptionEditor", response);
        }
       

        [HttpPost]
        public ActionResult EditDescription(File_Model postObject)

        {
            FileService service = new FileService();
            bool response = service.UpdateContent(postObject);
            return (Json(response, JsonRequestBehavior.AllowGet));
        }

        public ActionResult DeleteDescription(int Id)
        {
            FileService service = new FileService();
            if(service.DeleteContent(Id))
            {
                return RedirectToAction("AdminEditor");
            }

            {

                return View("View_AdminEditor");
            }
        }

    }
}
