using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using MemeGenerator.Services;
using MemeGenerator.Models;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;

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
            File_list  fileList=FileService.GetFileList();
            
            return View(fileList);
        }

        public ActionResult WriteText(ImageModification_Model model)
        {
            string uploadFile = Server.MapPath("~/image");
            model.savePath = uploadFile;
            model.filePath = Path.Combine(uploadFile ,model.fileName);
            WriteTextOp(model);
            FileDownload(model);

            return View();
        }

        public static string WriteTextOp(ImageModification_Model model)
        {
            if (!string.IsNullOrEmpty(model.filePath))
            {
                string value = model.Text;

                using (Bitmap bitmap = new Bitmap(model.filePath, true))
                {
                    try
                    {
                        using (Graphics graphics = Graphics.FromImage(bitmap))
                        {
                            Brush brush = new SolidBrush(Color.White);
                            Font font = new Font("Arial", 60, FontStyle.Italic, GraphicsUnit.Pixel);
                            SizeF textSize = new SizeF();
                            textSize = graphics.MeasureString(value, font);
                            //Point position = new Point(model.x, model.y);

                            Point position = new Point(bitmap.Width - ((int)textSize.Width + model.x), bitmap.Height - ((int)textSize.Height + model.y));
                            graphics.DrawString(value, font, brush, position);
                            //using (MemoryStream memStr = new MemoryStream())
                            //{
                            //    bitmap.Save(memStr, ImageFormat.Png);
                            //    memStr.Position = 0;
                            //    return File(memStr.ToArray(), "image/png", model.fileName);
                            //}
                        }
                        bitmap.Save(Path.Combine(model.savePath ,"newimage.png"));
                        
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                }
            }
            return "success";

        }

        public void FileDownload(ImageModification_Model model)
        {
            string url = Path.Combine(model.savePath , "newimage");
               //string savepath="" ;
               WebClient Client = new WebClient();
               Client.DownloadFile(url, @"D:\himasu");
          
        }


        public ActionResult NewMeme(int id)
        {
            ViewBag.Message = "Your contact page.";
            File_Model response=FileService.GetFileById(id);
            return View("View_NewMeme",response);
        }


    }
}