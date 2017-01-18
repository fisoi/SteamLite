﻿using System;
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
                SQLManager.CheckSignIn();

                MainForm form = new MainForm();
                form.Show();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UserRegisterForm form = new UserRegisterForm(this);
            form.Show();

            this.Hide();
        }
    }
}
