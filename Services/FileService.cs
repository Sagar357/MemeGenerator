using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MemeGenerator.Services
{
    public class FileService
    {
        public static string Upload(HttpPostedFileBase file ,string uploadpath) 
        {
            string message = string.Empty;
            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(uploadpath,
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                   message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                message = "You have not specified a file.";
            }
            return message;
        }
    }
}