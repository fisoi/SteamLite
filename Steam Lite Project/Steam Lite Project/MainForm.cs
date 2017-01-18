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

            UpdateWishlist();
            UpdateGameDataBox(dataGridView1.Rows[0].Cells[0].Value.ToString());

            //LIBRARY

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
        }
        
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count > 0 && dataGridView1.SelectedRows.Count > 0)
            {
                UpdateGameDataBox(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                button2.Enabled = true;
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0 && dataGridView2.SelectedRows.Count > 0)
            {
                UpdateGameDataBox(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                button2.Enabled = false;
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
    }
}
