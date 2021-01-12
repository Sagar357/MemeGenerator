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

        public ActionResult meme(int id)
        {
      
            File_Model response =FileService.GetFileById(id);
            string filePath = Server.MapPath("~/image");
            string path = Path.Combine(filePath, response.filepath);
            return View("meme",response);
       }

        public ActionResult custom()
        {
            return PartialView("partial_custom");
        }

        public ActionResult contact()
        {
            return View("Contact");
        }

        //public ActionResult New()
        //{
        //    return View("meme");
        //}
}
}