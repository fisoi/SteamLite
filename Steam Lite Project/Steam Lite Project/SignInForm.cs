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
    public partial class SignInForm : Form
    {
        public SignInForm()
        {
            InitializeComponent();
            SQLManager.InitConnection();

            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length < 5)
            {
                MessageBox.Show("Useranme too short!");
            }
            else
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    if (SQLManager.CheckSignIn(textBox1.Text, textBox2.Text, false))
                    {
                        MainForm form = new MainForm(textBox1.Text, this);
                        form.Show();

                        this.Hide();
                    }
                }
                else if(comboBox1.SelectedIndex == 1)
                {
                    if (SQLManager.CheckSignIn(textBox1.Text, textBox2.Text, true))
                    {
                        PublisherMainForm form = new PublisherMainForm(this, SQLManager.GetPublisherFromUsername(textBox1.Text));
                        form.Show();

                        this.Hide();
                    }
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UserRegisterForm form = new UserRegisterForm(this);
            form.Show();

            this.Hide();
        }

        private void SignInForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
