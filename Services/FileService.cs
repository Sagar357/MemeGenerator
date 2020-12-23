using Microsoft.ApplicationBlocks.Data;
using System;
using MemeGenerator.Models;
using MemeGenerator.utils;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;

namespace MemeGenerator.Services
{
    public class FileService
    {



        public static string Upload(HttpPostedFileBase file, string uploadpath)
        {
            string message = string.Empty;
            if (file != null && file.ContentLength > 0)
            {
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
            }

            else
            {
                message = "You have not specified a file.";
            }



            using (SqlConnection db = connectionutils.getConnection())
            {
                Guid attachmentcode = Guid.NewGuid();
                db.Open();
                DataSet ds = new DataSet();
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@attachmentcode", attachmentcode);
                param[1] = new SqlParameter("@filepath", Path.Combine("/image/", file.FileName));
                ds = SqlHelper.ExecuteDataset(db, CommandType.StoredProcedure, "InsertStoredProcedureFile", param);

                message = "success";
            }

            return message;

        }


        public static File_list GetFileList()
        {
            File_list listObj = null;
            using (SqlConnection con = connectionutils.getConnection())
            {
                con.Open();
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "GetAttachment");

                if (ds.Tables.Count > 0)
                {
                    listObj = new File_list();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        File_Model model = new File_Model();
                        if (!string.IsNullOrEmpty(dr["fileid"].ToString()))
                        {
                            model.fileid = Convert.ToInt32(dr["fileid"]);
                        }
                        else
                        {
                            model.fileid = 0;
                        }
                        if (!string.IsNullOrEmpty(dr["filepath"].ToString()))
                        {
                            model.filepath = dr["filepath"].ToString();
                        }
                        else
                        {
                            model.filepath = "";
                        }
                        listObj.list.Add(model);
                    }
                }

            }
            return listObj;
        }

        public static File_Model GetFileById(int id)
        {
            File_Model model = null;
            using (SqlConnection con = connectionutils.getConnection())
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@fileid" ,id);
                ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "GetAttachment" ,param);

                if (ds.Tables.Count > 0)
                {

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        model = new File_Model();
                        if (!string.IsNullOrEmpty(dr["fileid"].ToString()))
                        {
                            model.fileid = Convert.ToInt32(dr["fileid"]);
                        }
                        else
                        {
                            model.fileid = 0;
                        }
                        if (!string.IsNullOrEmpty(dr["filepath"].ToString()))
                        {
                            model.filepath = dr["filepath"].ToString();
                        }
                        else
                        {
                            model.filepath = "";
                        }

                    }
                }

            }
            return model;
        }
        public static string WriteText(ImageModification_Model model)
        {
            if(!string.IsNullOrEmpty(model.filePath))
            {
                string value = model.Text;
                
               using (Bitmap bitmap=new Bitmap(model.filePath, true))
                {
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        Brush brush = new SolidBrush(Color.White);
                        Font font = new Font("Arial", 60, FontStyle.Italic, GraphicsUnit.Pixel);
                        SizeF textSize = new SizeF();
                        textSize = graphics.MeasureString(value ,font);
                        Point position = new Point(model.x, model.y);
                        graphics.DrawString(value, font, brush, position);
                        using (MemoryStream memStr =new MemoryStream())
                        {
                            bitmap.Save("new.jpg" ,memStr, ImageFormat.Jpeg);
                            memStr.Position = 0;
                            
                        }
                    }
                       
                }
            }
            return "success";

        }
    }
  }
        
    
