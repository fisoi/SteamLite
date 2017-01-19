using Steam_Lite_Project.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Steam_Lite_Project
{
    public partial class MainForm : Form
    {
        private string loggedUser;
        private SignInForm signInForm;

        private User currentUser;

        private Game shopSelectedGame;

        public MainForm(string username, SignInForm signInForm)
        {
            InitializeComponent();
            loggedUser = username;
            this.signInForm = signInForm;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            signInForm.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //SHOP
            this.Text = "Steam Lite " + loggedUser;

            List<GameItem> shopGames = SQLManager.GetShopGames();

            foreach(GameItem game in shopGames)
            {
                dataGridView1.Rows.Add(game.title, game.price + "$");
            }

            //WISHLIST
            UpdateWishlist();

            //LIBRARY
            UpdateLibrary();

            //PROFILE
            UpdateProfile();

            //DATA BOXES
            try
            {
                UpdateGameDataBox(dataGridView1.Rows[0].Cells[0].Value.ToString());
                UpdateGameDataLibraryBox(dataGridView3.Rows[0].Cells[0].Value.ToString());
            }
            catch { }
        }

        private void UpdateLibrary()
        {
            dataGridView3.Rows.Clear();
            List<LibraryGame> libraryGames = SQLManager.GetLibrary(loggedUser);

            foreach (LibraryGame game in libraryGames)
            {
                dataGridView3.Rows.Add(SQLManager.GetGameFromID(game.GID).title, game.hoursPlayed, (game.installed ? "Install" : "Uninstall"));
            }
        }

        private void UpdateProfile()
        {
            currentUser = SQLManager.GetUser(loggedUser);

            label14.Text = currentUser.profileName;
            label16.Text = SQLManager.GetCountryFromID(currentUser.CID);

            textBox10.Text = currentUser.profileName;

            List<Country> countryes = SQLManager.GetCountryes();
            foreach (Country country in countryes)
            {
                comboBox1.Items.Add(country.name);
            }
            comboBox1.SelectedItem = SQLManager.GetCountryFromID(currentUser.CID);

            UpdateFriends(currentUser.UID);
        }

        private void UpdateFriends(string UID)
        {
            List<string> friends = SQLManager.GetFriends(UID);
            foreach (string friend in friends)
            {
                dataGridView4.Rows.Add(friend);
            }
        }

        private void UpdateWishlist()
        {
            dataGridView2.Rows.Clear();
            List<GameItem> wishlistGames = SQLManager.GetWishlistGames(loggedUser);

            foreach (GameItem game in wishlistGames)
            {
                dataGridView2.Rows.Add(game.title, game.price + "$");
            }
        }

        private void UpdateGameDataBox(string gameTitle)
        {
            label1.Text = gameTitle;

            Game game = SQLManager.GetGame(gameTitle);
            richTextBox1.Text = game.description;
            textBox1.Text = game.releaseDate;
            textBox3.Text = game.reviewScore.ToString();
            label6.Text = "Price: " + game.price.ToString() + "$";

            Publisher publisher = SQLManager.GetPublisher(game.PID);
            textBox2.Text = publisher.publisherName;
            richTextBox4.Text = publisher.description;

            textBox4.Text = SQLManager.GetState(game.SID);

            richTextBox3.Text = SQLManager.GetTags(game.GID);

            shopSelectedGame = game;
        }
        
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count > 0 && dataGridView1.SelectedRows.Count > 0)
            {
                UpdateGameDataBox(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

                button2.Enabled = true;
                button7.Enabled = true;
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0 && dataGridView2.SelectedRows.Count > 0)
            {
                UpdateGameDataBox(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());

                button2.Enabled = false;
                button7.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if(row.Cells[0].Value.ToString() == dataGridView1.SelectedRows[0].Cells[0].Value.ToString())
                {
                    //game already on wishlist
                    return;
                }
            }

            SQLManager.InsertWish(loggedUser, dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            UpdateWishlist();
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count > 0 && dataGridView3.SelectedRows.Count > 0)
            {
                UpdateGameDataLibraryBox(dataGridView3.SelectedRows[0].Cells[0].Value.ToString());
                button3.Enabled = true;
            }
        }

        private void UpdateGameDataLibraryBox(string gameTitle)
        {
            Game game = SQLManager.GetGame(gameTitle);
            richTextBox2.Text = game.description;
            textBox8.Text = game.releaseDate;
            textBox6.Text = game.reviewScore.ToString();
            
            textBox5.Text = SQLManager.GetState(game.SID);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            shopSelectedGame.reviewScore = (shopSelectedGame.reviewsAmount++ * shopSelectedGame.reviewScore + trackBar1.Value) /
                            shopSelectedGame.reviewsAmount;

            button7.Enabled = false;
            textBox3.Text = shopSelectedGame.reviewScore.ToString();

            SQLManager.UpdateReview(shopSelectedGame);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            currentUser.profileName = textBox10.Text;
            currentUser.CID = SQLManager.GetIDFromCountry(comboBox1.SelectedItem.ToString()).ToString();

            SQLManager.UpdateUserProfile(currentUser);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (currentUser.password == textBox12.Text)
            {
                if (textBox13.Text.Length >= 5 && textBox14.Text == textBox13.Text)
                {
                    currentUser.password = textBox13.Text;
                    SQLManager.UpdateUserProfile(currentUser);

                    textBox12.Text = null;
                    textBox13.Text = null;
                    textBox14.Text = null;
                }
                else MessageBox.Show("Passwords do not mach or too short!");
            }
            else MessageBox.Show("Incorrect old password!");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string newFriend = SQLManager.ProfileNameFromUID(SQLManager.UserIDFromName(textBox9.Text));
            if (newFriend != null)
            {
                dataGridView4.Rows.Add(newFriend);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQLManager.InsertInLibrary(currentUser, shopSelectedGame);
            UpdateLibrary();
        }
    }
}
