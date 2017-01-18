using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace Steam_Lite_Project
{
    class SQLManager
    {
        static SqlConnection myConnection;

        public static void InitConnection()
        {
            myConnection = new SqlConnection("user id=username;" +
                "password=password;" +
                "server=localhost;" +
                "Trusted_Connection=yes;" +
                "database=database; " +
                "connection timeout=5");

            try
            {
                myConnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        public static bool CheckSignIn()
        {
            try
            {
                SqlDataReader myReader = null;

                SqlCommand myCommand = new SqlCommand("SELECT * FROM Users", myConnection);
                myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    Console.WriteLine(myReader["UID"].ToString() + " " + myReader["username"].ToString() + " " + myReader["password"].ToString());
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}


//SqlParameter myParam = new SqlParameter("@param", SqlDbType.VarChar, 25);
//myParam.Value = "Garden Hose";

//                SqlCommand myCommand = new SqlCommand("QUERRY", myConnection);
//myCommand.Parameters.Add(myParam);

//                myCommand.ExecuteNonQuery();