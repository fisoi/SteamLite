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
    public partial class UserRegisterForm : Form
    {
        SignInForm signInForm;

        public UserRegisterForm(SignInForm signInForm)
        {
            InitializeComponent();

            this.signInForm = signInForm;
        }

        private void UserRegisterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(signInForm != null)
                signInForm.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length >= 5) textBox1.BackColor = Color.LightGreen;
            else textBox1.BackColor = Color.LightPink;
            CheckAll();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length >= 5 && textBox2.Text.Contains("@") && textBox2.Text.Contains(".")) textBox2.BackColor = Color.LightGreen;
            else textBox2.BackColor = Color.LightPink;
            CheckAll();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Length >= 5) textBox3.BackColor = Color.LightGreen;
            else textBox3.BackColor = Color.LightPink;
            CheckAll();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text.Length >= 5 && textBox3.Text == textBox4.Text) textBox4.BackColor = Color.LightGreen;
            else textBox4.BackColor = Color.LightPink;
            CheckAll();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text.Length >= 1) textBox5.BackColor = Color.LightGreen;
            else textBox5.BackColor = Color.LightPink;
            CheckAll();
        }

        private void CheckAll()
        {
            if(textBox1.BackColor == Color.LightGreen && 
                textBox2.BackColor == Color.LightGreen && 
                textBox3.BackColor == Color.LightGreen &&
                textBox4.BackColor == Color.LightGreen &&
                textBox5.BackColor == Color.LightGreen)
            {
                button1.Enabled = true;
                label7.Visible = false;
            }
            else
            {
                button1.Enabled = false;
                label7.Visible = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PublisherRegisterForm form = new PublisherRegisterForm(signInForm);
            form.Show();

            signInForm = null;

            this.Close();
        }
    }
}
