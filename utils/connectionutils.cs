using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace MemeGenerator.utils
{
    public class connectionutils
    {
        private static SqlConnection con;

        public static SqlConnection getConnection()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["PortalConnectionString"].ToString());
            return con;
        }
    }
}