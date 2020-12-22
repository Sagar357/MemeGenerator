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
    }
  }
        
    
