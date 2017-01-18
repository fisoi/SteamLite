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
            this.Text = "Steam Lite " + loggedUser;

            List<GameItem> shopGames = SQLManager.GetShopGames();

            foreach(GameItem game in shopGames)
            {
                dataGridView1.Rows.Add(game.title, game.price + "$");
            }

            UpdateGameDataBox(dataGridView1.Rows[0].Cells[0].Value.ToString());
        }

        private void UpdateGameDataBox(string gameTitle)
        {
            label1.Text = gameTitle;

            Game game = SQLManager.GetGame(gameTitle);

            richTextBox1.Text = game.description;
            textBox1.Text = game.releaseDate;
            textBox3.Text = game.reviewScore.ToString();

            label6.Text = "Price: " + game.price.ToString() + "$";
        }
        
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            UpdateGameDataBox(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
        }
    }
}
