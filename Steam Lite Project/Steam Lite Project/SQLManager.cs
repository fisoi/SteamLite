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
            myConnection = new SqlConnection(Properties.Settings.Default.SteamLiteConnectionString);

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

        public static void CloseConnection()
        {
            myConnection.Close();
        }

        public static bool CheckSignIn(string username, string password)
        {
            try
            {
                SqlParameter myParam = new SqlParameter("@param", SqlDbType.VarChar, username.Length);
                myParam.Value = username;

                SqlCommand myCommand = new SqlCommand("SELECT * FROM Users WHERE username=@param", myConnection);
                myCommand.Parameters.Add(myParam);

                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.HasRows)
                {
                    myReader.Read();
                    if (myReader["password"].ToString() == password)
                    {
                        myReader.Close();

                        return true;
                    }
                    MessageBox.Show("Password invalid!");
                }
                else MessageBox.Show("Username invalid!");

                myReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return false;
        }
    }
}


//SqlParameter myParam = new SqlParameter("@param", SqlDbType.VarChar, 25);
//myParam.Value = "Garden Hose";

//                SqlCommand myCommand = new SqlCommand("QUERRY", myConnection);
//myCommand.Parameters.Add(myParam);

//                myCommand.ExecuteNonQuery();