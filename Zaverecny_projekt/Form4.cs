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
    /// <summary>
    /// Signup form
    /// </summary>
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
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

        /// <summary>
        /// Does the register of the user
        /// </summary>
        /// <param name="sender"> Source of the event</param>
        /// <param name="e"> Tjis contains data of the event</param>
        private void Register(object sender, EventArgs e)
        {

            //Retrieves values from text boxes from user input
            string email = textBox1.Text;
            string password = textBox2.Text;
            string repeatPassword = textBox3.Text;
            string firstName = textBox4.Text;
            string lastName = textBox5.Text;
           
            //Controls if all fields are filled out
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(repeatPassword) || string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("All fields must be filled out.");
                return;
            }

            //Check if passwords matches
            if (password != repeatPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            UserDAO DAO = new();//Instance of user for acces to database

            User user = new User(firstName, lastName, email, password);//Creates new user with these attributes

            try
            {
                DAO.Save(user); //Saving the user to database
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured. Email likely already in use"); //Error message when email is in use
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

