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
            string value = string.Empty;
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
                string name = Path.GetFileNameWithoutExtension(file.FileName);
                Guid attachmentcode = Guid.NewGuid();
                db.Open();
                DataSet ds = new DataSet();
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@attachmentcode", attachmentcode);
                param[1] = new SqlParameter("@filepath", Path.Combine("/image/", file.FileName));
                param[2] = new SqlParameter("@filename", name.Replace(" " ,"-"));
                param[3] = new SqlParameter("@Description","");


                ds = SqlHelper.ExecuteDataset(db, CommandType.StoredProcedure, "InsertStoredProcedureFile", param);
                value = Path.Combine("../image/", file.FileName);

                message = "success";
            }

            // return message;
            return value;

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
                        //if (!string.IsNullOrEmpty(dr["fileid"].ToString()))
                        //{
                        //  // model.slug = dr["slug"].ToString();
                        //  model.fileid = Convert.ToInt32(dr["fileid"]);
                        //}
                        //else
                        //{
                        //    model.fileid = 0;
                        //}
                        if (!string.IsNullOrEmpty(dr["filepath"].ToString()))
                        {
                            model.filepath = dr["filepath"].ToString();
                        }
                        
                        else
                        {
                            model.filepath = "";
                        }
                        if (!string.IsNullOrEmpty(dr["fileName"].ToString()))
                        {
                            model.fileName = dr["fileName"].ToString();

                        }
                        else
                        {
                            model.fileName = "";
                        }
                        if(!string.IsNullOrEmpty(dr["attachmentcode"].ToString()))
                        {
                            model.attachmentcode = dr["attachmentcode"].ToString();
                        }
                        else
                        {
                            model.attachmentcode = "";
                        }
                        if (!string.IsNullOrEmpty(dr["Description"].ToString()))
                        {
                            model.Description = dr["Description"].ToString();
                        }
                        else
                        {
                            model.Description = "";
                        }
                        if (!string.IsNullOrEmpty(dr["PostName"].ToString()))
                        {
                            model.PostName = dr["PostName"].ToString();
                        }
                        else
                        {
                            model.PostName = "";
                        }
                        if (!string.IsNullOrEmpty(dr["SeoTitle"].ToString()))
                        {
                            model.SeoTitle = dr["SeoTitle"].ToString();
                        }
                        else
                        {
                            model.SeoTitle = "";
                        }
                        if (!string.IsNullOrEmpty(dr["SeoDescription"].ToString()))
                        {
                            model.SeoDescription = dr["SeoDescription"].ToString();
                        }
                        else
                        {
                            model.SeoDescription = "";
                        }
                        if (!string.IsNullOrEmpty(dr["SeoSlug"].ToString()))
                        {
                            model.SeoSlug = dr["SeoSlug"].ToString();
                        }
                        else
                        {
                            model.SeoSlug = "";
                        }
                        listObj.list.Add(model);
                    }
                }

            }
            return listObj;
        }

        public static File_Model GetFileById(string id)
        {
            File_Model model = null;
            using (SqlConnection con = connectionutils.getConnection())
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@slug", id);
                ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "GetAttachment", param);

                if (ds.Tables.Count > 0)
                {

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        model = new File_Model();
                        if (!string.IsNullOrEmpty(dr["fileid"].ToString()))
                        {
                            //model.fileName = dr["fileName"].ToString();
                            model.fileid = Convert.ToInt32(dr["fileid"]);
                        }
                        else
                        {
                            // model.fileName = null;
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
                        if (!string.IsNullOrEmpty(dr["fileName"].ToString()))
                        {
                            model.fileName = dr["fileName"].ToString();
                        }
                        else
                        {
                            model.fileName = "";
                        }
                        if (!string.IsNullOrEmpty(dr["attachmentcode"].ToString()))
                        {
                            model.attachmentcode = dr["attachmentcode"].ToString();
                        }
                        else
                        {
                            model.attachmentcode = "";
                        }
                        if(!string.IsNullOrEmpty(dr["Description"].ToString()))
                        {
                            model.Description = dr["Description"].ToString();
                        }
                        else
                        {
                            model.Description = "";
                        }
                        if(!string.IsNullOrEmpty(dr["PostName"].ToString()))
                        {
                            model.PostName = dr["PostName"].ToString();
                        }
                        else
                        {
                            model.PostName = "";
                        }
                        if(!string.IsNullOrEmpty(dr["SeoTitle"].ToString()))
                        {
                            model.SeoTitle = dr["SeoTitle"].ToString();
                        }
                        else
                        {
                            model.SeoTitle = "";
                        }
                        if(!string.IsNullOrEmpty(dr["SeoDescription"].ToString()))
                        {
                            model.SeoDescription = dr["SeoDescription"].ToString();
                        }
                        else
                        {
                            model.SeoDescription = "";
                        }
                        if(!string.IsNullOrEmpty(dr["SeoSlug"].ToString()))
                        {
                            model.SeoSlug = dr["SeoSlug"].ToString();
                        }
                        else
                        {
                            model.SeoSlug = "";
                        }
                    }
                }

            }
            return model;
        }
        public string Login(LoginModel data)
        {
            int responseCode;
            string status = String.Empty;
            try
            {
                using (SqlConnection con = connectionutils.getConnection())
                {
                    SqlCommand cmd = new SqlCommand("validateUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@adminName", data.adminName);
                    cmd.Parameters.AddWithValue("@adminPassword", data.adminPassword);
                    cmd.Parameters.Add("@outparam", SqlDbType.Int);
                    cmd.Parameters["@outparam"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    responseCode = Convert.ToInt32(cmd.Parameters["@outparam"].Value);

                    if (responseCode == 0)
                        status = "Success";
                    else if (responseCode == -1)
                        status = "Invalid Usarname or Password";
                }
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return (status);
        }

        public AdminFileEditList AdminEditList()
        {
            AdminFileEditList FileEditListObj = new AdminFileEditList();
            try
            {
                using (SqlConnection con = connectionutils.getConnection())
                {
                    List<File_Model> tasklist = new List<File_Model>();
                    SqlCommand cmd = new SqlCommand("getEditList", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    con.Open();
                    da.Fill(ds);
                    con.Close();


                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        tasklist.Add(
                            new File_Model
                            {
                                fileid = Convert.ToInt32(dr["fileid"]),
                                attachmentcode = Convert.ToString(dr["attachmentcode"]),
                                filepath = Convert.ToString(dr["filepath"]),
                                bloburl = Convert.ToString(dr["bloburl"]),
                                fileName = Convert.ToString(dr["fileName"]),
                                Description = Convert.ToString(dr["Description"]),
                                PostName=Convert.ToString(dr["PostName"]),
                                SeoTitle=Convert.ToString(dr["SeoTitle"]),
                                SeoDescription=Convert.ToString(dr["SeoDescription"]),
                                SeoSlug=Convert.ToString(dr["SeoSlug"])




                            }
                            );
                    }
                    FileEditListObj.processlist = tasklist;




                }

            }
            catch (Exception ex)
            {


            }
            return FileEditListObj;

        }
        public bool UpdateContent(File_Model obj)
        {
            bool response=false;
            try
            {
                using (SqlConnection con = connectionutils.getConnection())
                {
                    if (string.IsNullOrEmpty(obj.Description))
                        obj.Description = "";
                    SqlCommand cmd = new SqlCommand("updateDscription", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fileid", obj.fileid);
                    cmd.Parameters.AddWithValue("@attachmentcode", obj.attachmentcode);
                    cmd.Parameters.AddWithValue("@filepath", obj.filepath);
                    cmd.Parameters.AddWithValue("@fileName", obj.fileName);
                    cmd.Parameters.AddWithValue("@Description", obj.Description);
                    cmd.Parameters.AddWithValue("@PostName", obj.PostName);
                    cmd.Parameters.AddWithValue("@SeoTitle", obj.SeoTitle);
                    cmd.Parameters.AddWithValue("@SeoDescription", obj.SeoDescription);
                    cmd.Parameters.AddWithValue("@SeoSlug", obj.SeoSlug);


                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                    {
                        response= true;
                    }
                    else
                    {
                        response= false;
                    }
                }

            }
            catch (Exception ex)
            {

               
            }
            return response;
       }

        public bool DeleteContent(int fileid)
        {
            using (SqlConnection con = connectionutils.getConnection())
            {
                SqlCommand cmd = new SqlCommand("DeleteFile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fileid", fileid);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if(i<=1)
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }

        }

    }
}

        //public static contactus(memecontact contact)
        //{
        //    string status = string.Empty;
        //    try
        //    {
        //        using (SqlConnection con = connectionutils.getConnection())
        //        {

        //        }
        //    }


        //}
    

//      public static string WriteText(ImageModification_Model model)
//      {
//          if(!string.IsNullOrEmpty(model.filePath))
//          {
//              string value = model.Text;

//             using (Bitmap bitmap=new Bitmap(model.filePath, true))
//              {
//                  using (Graphics graphics = Graphics.FromImage(bitmap))
//                  {
//                      Brush brush = new SolidBrush(Color.White);
//                      Font font = new Font("Arial", 60, FontStyle.Italic, GraphicsUnit.Pixel);
//                      SizeF textSize = new SizeF();
//                      textSize = graphics.MeasureString(value ,font);
//                      Point position = new Point(model.x, model.y);
//                      graphics.DrawString(value, font, brush, position);
//                      using (MemoryStream memStr =new MemoryStream())
//                      {
//                          bitmap.Save("new.jpg" ,memStr, ImageFormat.Jpeg);
//                          memStr.Position = 0;

//                      }
//                  }

//              }
//          }
//          return "success";

//      }
//  }
//}


