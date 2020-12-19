using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
namespace MemeGenerator.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if(file != null &&file.ContentLength >0 )
            
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

        public ActionResult Index(string Message=null)
        {
            if (!string.IsNullOrEmpty(Message))
            {
                ViewBag.Message = Message;
            }
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}