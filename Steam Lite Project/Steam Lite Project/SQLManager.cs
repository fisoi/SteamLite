using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using Steam_Lite_Project.DataModels;

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

        public static bool CheckSignIn(string username, string password, bool isPublisher)
        {
            try
            {
                SqlParameter myParam1 = new SqlParameter("@param1", SqlDbType.VarChar, username.Length);
                myParam1.Value = username;
                
                SqlCommand myCommand = new SqlCommand("SELECT * FROM " + (isPublisher? "Publishers" : "Users") + " WHERE username=@param1", myConnection); 

                myCommand.Parameters.Add(myParam1);
                
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
        
        public static List<Country> GetCountryes()
        {
            try
            {
                SqlCommand myCommand = new SqlCommand("SELECT * FROM Countries", myConnection);
                SqlDataReader myReader = myCommand.ExecuteReader();

                List<Country> result = new List<Country>();
                
                while(myReader.Read())
                {
                    result.Add(new Country(int.Parse(myReader["CID"].ToString()), myReader["name"].ToString()));
                }

                myReader.Close();
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public static int GetIDFromCountry(string countryName)
        {
            try
            {
                SqlCommand myCommand = new SqlCommand("SELECT * FROM Countries", myConnection);
                SqlDataReader myReader = myCommand.ExecuteReader();
                
                while (myReader.Read())
                {
                    Country country = new Country(int.Parse(myReader["CID"].ToString()), myReader["name"].ToString());
                    if(country.name == countryName)
                    {
                        myReader.Close();
                        return country.ID;
                    }
                }

                myReader.Close();
                return -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return -1;
            }
        }

        public static bool Register(string username, string email, string password, string profileName, int countryID)
        {
            try
            {
                SqlParameter myParam1 = new SqlParameter("@param1", SqlDbType.VarChar, username.Length);
                myParam1.Value = username;

                SqlParameter myParam2 = new SqlParameter("@param2", SqlDbType.VarChar, email.Length);
                myParam2.Value = email;

                SqlParameter myParam3 = new SqlParameter("@param3", SqlDbType.VarChar, password.Length);
                myParam3.Value = password;

                SqlParameter myParam4 = new SqlParameter("@param4", SqlDbType.VarChar, profileName.Length);
                myParam4.Value = profileName;

                SqlParameter myParam5 = new SqlParameter("@param5", SqlDbType.Int);
                myParam5.Value = countryID;

                SqlCommand myCommand = new SqlCommand("INSERT INTO Users (username,password,profileName,CID,email) VALUES (@param1,@param3,@param4,@param5,@param2)", myConnection);
                myCommand.Parameters.AddRange(new SqlParameter[] { myParam1, myParam2, myParam3, myParam4, myParam5 });

                myCommand.ExecuteNonQuery();
                MessageBox.Show("Register for " + username + " was a succes!");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public static bool RegisterPublisher(string username, string email, string password, string publisherName, string companyName, string description)
        {
            try
            {
                SqlParameter myParam1 = new SqlParameter("@param1", SqlDbType.VarChar, username.Length);
                myParam1.Value = username;

                SqlParameter myParam2 = new SqlParameter("@param2", SqlDbType.VarChar, email.Length);
                myParam2.Value = email;

                SqlParameter myParam3 = new SqlParameter("@param3", SqlDbType.VarChar, password.Length);
                myParam3.Value = password;

                SqlParameter myParam4 = new SqlParameter("@param4", SqlDbType.VarChar, publisherName.Length);
                myParam4.Value = publisherName;

                SqlParameter myParam5 = new SqlParameter("@param5", SqlDbType.VarChar, companyName.Length);
                myParam5.Value = companyName;

                SqlParameter myParam6 = new SqlParameter("@param6", SqlDbType.VarChar, description.Length);
                myParam6.Value = description;


                SqlCommand myCommand = new SqlCommand("INSERT INTO Publishers (username,email,password,publisherName,companyName,description) VALUES (@param1,@param2,@param3,@param4,@param5,@param6)", myConnection);
                myCommand.Parameters.AddRange(new SqlParameter[] { myParam1, myParam2, myParam3, myParam4, myParam5, myParam6 });

                myCommand.ExecuteNonQuery();
                MessageBox.Show("Register for " + username + " was a succes!");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public static List<GameItem> GetShopGames()
        {
            try
            {
                SqlCommand myCommand = new SqlCommand("SELECT * FROM Games", myConnection);
                SqlDataReader myReader = myCommand.ExecuteReader();

                List<GameItem> result = new List<GameItem>();

                while (myReader.Read())
                {
                    result.Add(new GameItem(myReader["title"].ToString(), float.Parse(myReader["price"].ToString())));
                }

                myReader.Close();
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public static List<GameItem> GetWishlistGames(string username)
        {
            try
            {
                //GET USER ID FROM USERNAME
                SqlParameter myParam1 = new SqlParameter("@param1", SqlDbType.VarChar, username.Length);
                myParam1.Value = username;

                SqlCommand myCommand = new SqlCommand("SELECT * FROM Users WHERE username=@param1", myConnection);
                myCommand.Parameters.Add(myParam1);

                SqlDataReader myReader = myCommand.ExecuteReader();

                myReader.Read();
                string userID = myReader["UID"].ToString();

                myReader.Close();

                //GET GAME ID'S FOR CURRENT UID
                SqlCommand myCommand1 = new SqlCommand("SELECT * FROM Wishlist WHERE UID=" + userID, myConnection);
                SqlDataReader myReader1 = myCommand1.ExecuteReader();

                List<string> gameIDs = new List<string>();

                while (myReader1.Read())
                {
                    gameIDs.Add(myReader1["GID"].ToString());
                }

                myReader1.Close();

                List<GameItem> result = new List<GameItem>();

                foreach(string ID in gameIDs)
                {
                    Game game = GetGameFromID(ID);
                    result.Add(new GameItem(game.title, game.price));
                }

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public static void InsertWish(string username, string gameTitle)
        {
            try
            {
                //GET USER ID FROM USERNAME
                SqlParameter myParam1 = new SqlParameter("@param1", SqlDbType.VarChar, username.Length);
                myParam1.Value = username;

                SqlCommand myCommand = new SqlCommand("SELECT * FROM Users WHERE username=@param1", myConnection);
                myCommand.Parameters.Add(myParam1);

                SqlDataReader myReader = myCommand.ExecuteReader();

                myReader.Read();
                string userID = myReader["UID"].ToString();

                myReader.Close();

                //GET GAME ID FROM GAMES
                SqlParameter myParam2 = new SqlParameter("@param2", SqlDbType.VarChar, gameTitle.Length);
                myParam2.Value = gameTitle;

                SqlCommand myCommand1 = new SqlCommand("SELECT * FROM Games WHERE title=@param2", myConnection);
                myCommand1.Parameters.Add(myParam2);

                SqlDataReader myReader1 = myCommand1.ExecuteReader();

                myReader1.Read();
                string gameID = myReader1["GID"].ToString();

                myReader1.Close();

                //INSERT
                SqlCommand myCommand2 = new SqlCommand("INSERT INTO Wishlist (UID,GID) VALUES (" + userID + "," + gameID + ")", myConnection);
                myCommand2.ExecuteNonQuery();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        public static Game GetGame(string gameTitle)
        {
            try
            {
                SqlParameter myParam1 = new SqlParameter("@param1", SqlDbType.VarChar, gameTitle.Length);
                myParam1.Value = gameTitle;

                SqlCommand myCommand = new SqlCommand("SELECT * FROM Games WHERE title=@param1", myConnection);
                myCommand.Parameters.Add(myParam1);

                SqlDataReader myReader = myCommand.ExecuteReader();

                myReader.Read();
                Game result = new Game(int.Parse(myReader["GID"].ToString()), int.Parse(myReader["SID"].ToString()), int.Parse(myReader["PID"].ToString()),
                    myReader["title"].ToString(), myReader["description"].ToString(), float.Parse(myReader["price"].ToString()), 
                    myReader["releaseDate"].ToString(), int.Parse(myReader["reviewsAmount"].ToString()), float.Parse(myReader["reviewScore"].ToString()));

                myReader.Close();
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public static Game GetGameFromID(string GID)
        {
            try
            {
                SqlParameter myParam1 = new SqlParameter("@param1", SqlDbType.VarChar, GID.Length);
                myParam1.Value = GID;

                SqlCommand myCommand = new SqlCommand("SELECT * FROM Games WHERE GID=@param1", myConnection);
                myCommand.Parameters.Add(myParam1);

                SqlDataReader myReader = myCommand.ExecuteReader();

                myReader.Read();
                Game result = new Game(int.Parse(myReader["GID"].ToString()), int.Parse(myReader["SID"].ToString()), int.Parse(myReader["PID"].ToString()),
                    myReader["title"].ToString(), myReader["description"].ToString(), float.Parse(myReader["price"].ToString()),
                    myReader["releaseDate"].ToString(), int.Parse(myReader["reviewsAmount"].ToString()), float.Parse(myReader["reviewScore"].ToString()));

                myReader.Close();
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public static Publisher GetPublisher(int ID)
        {
            try
            {
                SqlParameter myParam1 = new SqlParameter("@param1", SqlDbType.Int);
                myParam1.Value = ID;

                SqlCommand myCommand = new SqlCommand("SELECT * FROM Publishers WHERE PID=@param1", myConnection);
                myCommand.Parameters.Add(myParam1);

                SqlDataReader myReader = myCommand.ExecuteReader();

                myReader.Read();
                Publisher result = new Publisher(int.Parse(myReader["PID"].ToString()), myReader["publisherName"].ToString(),
                    myReader["companyName"].ToString(), myReader["description"].ToString());

                myReader.Close();
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public static string GetState(int ID)
        {
            try
            {
                SqlParameter myParam1 = new SqlParameter("@param1", SqlDbType.Int);
                myParam1.Value = ID;

                SqlCommand myCommand = new SqlCommand("SELECT * FROM GameState WHERE SID=@param1", myConnection);
                myCommand.Parameters.Add(myParam1);

                SqlDataReader myReader = myCommand.ExecuteReader();

                myReader.Read();
                string result = myReader["name"].ToString();

                myReader.Close();
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public static string GetTags(int GID)
        {
            try
            {
                SqlParameter myParam1 = new SqlParameter("@param1", SqlDbType.Int);
                myParam1.Value = GID;

                SqlCommand myCommand = new SqlCommand("SELECT * FROM GameTag WHERE GID=@param1", myConnection);
                myCommand.Parameters.Add(myParam1);

                SqlDataReader myReader = myCommand.ExecuteReader();

                List<int> resultID = new List<int>();

                while (myReader.Read())
                {
                    resultID.Add(int.Parse(myReader["TID"].ToString()));
                }

                myReader.Close();

                SqlCommand myCommand2 = new SqlCommand("SELECT * FROM Tags", myConnection);
                SqlDataReader myReader2 = myCommand2.ExecuteReader();

                string result = null;

                while (myReader2.Read())
                {
                    for (int index = 0; index < resultID.Count; index++)
                    {
                        if (int.Parse(myReader2["TID"].ToString()) == resultID[index])
                            result += myReader2["tagName"].ToString() + " ";
                    }
                }

                myReader2.Close();
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
    }
}


//SqlParameter myParam = new SqlParameter("@param", SqlDbType.VarChar, 25);
//myParam.Value = "Garden Hose";

//                SqlCommand myCommand = new SqlCommand("QUERRY", myConnection);
//myCommand.Parameters.Add(myParam);

//                myCommand.ExecuteNonQuery();