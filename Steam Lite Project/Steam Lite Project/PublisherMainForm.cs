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
    public partial class PublisherMainForm : Form
    {

        private SignInForm signInForm;
        private Publisher currentPublisher;

        public PublisherMainForm(SignInForm signInForm, Publisher currentPublisher)
        {
            InitializeComponent();
            this.signInForm = signInForm;
            this.currentPublisher = currentPublisher;
        }

        private void PublisherMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            signInForm.Show();
        }

        private void PublisherMainForm_Load(object sender, EventArgs e)
        {
            textBox2.Text = currentPublisher.publisherName;
            textBox1.Text = currentPublisher.companyName;

            richTextBox4.Text = currentPublisher.description;

            List<GameItem> publisherGames = SQLManager.GetPublisherGames(currentPublisher);
            foreach (GameItem publisherGame in publisherGames)
            {
                dataGridView1.Rows.Add(publisherGame.title, publisherGame.price + "$");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length > 1)
            {
                SQLManager.AddGame(currentPublisher, textBox3.Text, richTextBox1.Text, numericUpDown1.Value.ToString());

                dataGridView1.Rows.Clear();

                List<GameItem> publisherGames = SQLManager.GetPublisherGames(currentPublisher);
                foreach (GameItem publisherGame in publisherGames)
                {
                    dataGridView1.Rows.Add(publisherGame.title, publisherGame.price + "$");
                }

                textBox3.Text = null;
                richTextBox1.Text = null;
                numericUpDown1.Value = 12;
            }
        }
    }
}
