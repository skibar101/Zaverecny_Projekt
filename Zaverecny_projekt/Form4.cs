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
using System.Text.RegularExpressions;

namespace Zaverecny_projekt
{

    /// <summary>
    /// Class for Signup Form
    /// </summary>
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

        /// <summary>
        /// Method that handles regisdter of the user to database
        /// </summary>
        //// <param name="sender"> Source of the event</param>
        /// <param name="e"> This contains data of the event</param>
        private void Register(object sender, EventArgs e)
        {
            //Retrieving users inputs from text boxes
            string email = textBox1.Text;
            string password = textBox2.Text;
            string repeatPassword = textBox3.Text;
            string firstName = textBox4.Text;
            string lastName = textBox5.Text;

            //Checkiing if every input is filled out
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(repeatPassword) || string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("All fields must be filled out.");
                return;
            }

            //Checking if email is in valid format
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Invalid email format.");
                return;
            }

            //Checking if password is in valid format
            if (!IsStrongPassword(password))
            {
                MessageBox.Show("Password is not strong enough. It must be at least 8 characters long and include uppercase letters, lowercase letters, digits, and special characters.");
                return;
            }
            
            //Checking if password is same as password below
            if (password != repeatPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            UserDAO DAO = new(); //Creating instance for acces to database

            User user = new User(firstName, lastName, email, password); //Creating object with these attributes

            try
            {
                DAO.Save(user); //Saving to database
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred. Email likely already in use.");
                return;
            }
            MessageBox.Show("Successfully registered");

            this.Hide();
            Form3 form3 = new Form3();
            form3.Show();
        }

        //Regex method for email validation
        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$"; //Before and after @ must be atleast one symbol, then must be dot and after dot atleast one symbol
            return Regex.IsMatch(email, emailPattern);
        }

        //Regex method for password validation
        private bool IsStrongPassword(string password)
        {
            var passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$"; //Must contain one lowercase, uppercase, one number, one special symbol and must be atleast 8 characters long
            return Regex.IsMatch(password, passwordPattern);
        }






        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
