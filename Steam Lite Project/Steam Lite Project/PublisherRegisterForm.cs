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
    public partial class PublisherRegisterForm : Form
    {
        SignInForm signInForm;

        public PublisherRegisterForm(SignInForm signInForm)
        {
            InitializeComponent();

            this.signInForm = signInForm;
        }

        private void PublisherRegisterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            signInForm.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length >= 5) textBox1.BackColor = Color.LightGreen;
            else textBox1.BackColor = Color.LightPink;
            CheckAll();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length >= 1) richTextBox1.BackColor = Color.LightGreen;
            else richTextBox1.BackColor = Color.LightPink;
            CheckAll();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text.Length >= 1) textBox6.BackColor = Color.LightGreen;
            else textBox6.BackColor = Color.LightPink;
            CheckAll();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text.Length >= 1) textBox5.BackColor = Color.LightGreen;
            else textBox5.BackColor = Color.LightPink;
            CheckAll();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text.Length >= 5 && textBox3.Text == textBox4.Text) textBox4.BackColor = Color.LightGreen;
            else textBox4.BackColor = Color.LightPink;
            CheckAll();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Length >= 5) textBox3.BackColor = Color.LightGreen;
            else textBox3.BackColor = Color.LightPink;
            CheckAll();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length >= 5 && textBox2.Text.Contains("@") && textBox2.Text.Contains(".")) textBox2.BackColor = Color.LightGreen;
            else textBox2.BackColor = Color.LightPink;
            CheckAll();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (textBox7.Text == "arian") textBox7.BackColor = Color.LightGreen;
            else textBox7.BackColor = Color.LightPink;
            CheckAll();
        }
        private void CheckAll()
        {
            if (textBox1.BackColor == Color.LightGreen &&
                textBox2.BackColor == Color.LightGreen &&
                textBox3.BackColor == Color.LightGreen &&
                textBox4.BackColor == Color.LightGreen &&
                textBox5.BackColor == Color.LightGreen &&
                textBox6.BackColor == Color.LightGreen &&
                textBox7.BackColor == Color.LightGreen &&
                richTextBox1.BackColor == Color.LightGreen)
            {
                button1.Enabled = true;
                label9.Visible = false;
            }
            else
            {
                button1.Enabled = false;
                label9.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SQLManager.RegisterPublisher(textBox1.Text, textBox2.Text, textBox3.Text, textBox5.Text, textBox6.Text, richTextBox1.Text))
            {
                this.Close();
            }
            else MessageBox.Show("Register failed!");
        }
    }
}
