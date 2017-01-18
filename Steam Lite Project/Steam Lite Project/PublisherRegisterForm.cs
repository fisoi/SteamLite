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
    }
}
