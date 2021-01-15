using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemeGenerator.Models
{
    public class File_Model
    {
        public int fileid { get; set; }
        public string attachmentcode { get; set; }
        public string filepath { get; set; }
        public string bloburl { get; set; }



    }
    public class File_list
    {
        public File_list()
        {
            this.list = new List<File_Model>();
        }
        public List<File_Model> list { get; set; }
        public string message { get; set; }


    }

    public class memecontact
    {
        public int customerId { get; set; }
        public string customerName { get; set; }
        public string customerMobile { get; set; }
        public string customerMessage { get; set; }
        public string customerEmail { get; set; }

        public string createdDate { get; set; }
        public string modifiedDate { get; set; }
        public string isActive { get; set; }
    }
    //public class ImageModification_Model
    //{
    //    public string filePath { get; set; }
    //    public string fileName { get; set; }
    //    public string Text { get; set; }
    //    public int x { get; set; } 
    //   public  int y { get; set; }
    //}
}