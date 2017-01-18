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
                dataGridView1.Rows.Add(game.title, game.price);
            }
        }
    }
}
