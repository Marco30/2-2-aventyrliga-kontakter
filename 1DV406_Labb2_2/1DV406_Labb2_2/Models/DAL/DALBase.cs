using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace _1DV406_Labb2_2.Models.DAL//Marco Villegas
{
    public abstract class DALBase
    {
        private static string connectionString; // Fält


        protected SqlConnection CreateConnection()// Skapar och returnerar en referens till ett anslutningsobjekt
        {
            SqlConnection conn = new SqlConnection(connectionString);
            return conn;
        }

        // Kontruktor
        static DALBase()  // Initierar connectionString genom att hämta anslutningssträngen från Web.config
        {
            connectionString = WebConfigurationManager.ConnectionStrings["CustomerConnectionString"].ConnectionString;
        }
    }
}