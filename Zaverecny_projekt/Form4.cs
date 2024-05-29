using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Zaverecny_projekt
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
        }


        private void Register(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string password = textBox2.Text;
            string repeatPassword = textBox3.Text;
            string firstName = textBox4.Text;
            string lastName = textBox5.Text;
            //  string birthCertificateNumber = textBox6.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(repeatPassword) || string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) /*|| string.IsNullOrWhiteSpace(birthCertificateNumber)*/)
            {
                MessageBox.Show("All fields must be filled out.");
                return;
            }

            if (password != repeatPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            UserDAO DAO = new();

            User user = new User(firstName, lastName, email, password);

            try
            {
                DAO.Save(user);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured. Email likely already in use");
                return;
            }
            MessageBox.Show("Sucessfully registered");

            this.Hide();
            Form3 form3 = new Form3();
            form3.Show();
        }






        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}

