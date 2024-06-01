using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zaverecny_projekt
{

    /// <summary>
    /// Login form
    /// </summary>
    public partial class Form3 : Form
    {
        SqlConnection connection = Singleton.GetInstance();
        public Form3()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 form4 = new Form4();
            form4.Show();
        }

        /// <summary>
        /// Method that handles login of the user to database
        /// </summary>
        //// <param name="sender"> Source of the event</param>
        /// <param name="e"> This contains data of the event</param>
        private void Login(object sender, EventArgs e)
        {
            UserDAO dao = new(); //Creating instance for acces to database

            //Retrieving users inputs from text boxes
            string email = textBox1.Text;
            string pass = textBox2.Text;

            //Checking if the email is valid
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Invalid email format.");
                return;
            }

            textBox1.Clear();
            textBox2.Clear();

            //Retrieving user from database by email and password
            User? user = dao.GetByEmailAndPassword(email, pass);

            //Checking if user was found
            if (user == null)
            {
                MessageBox.Show("Failed to login");
                return;
            }

            //Checking if user is banned
            if (user.Ban)
            {
                MessageBox.Show("You are BANNED!");
                return;
            }

            //Mesage that pops up if succesfully logged in
            MessageBox.Show("Successfully logged in");
            Game.loggedInUser = user;
            this.Hide();
            Form5 form5 = new Form5();
            form5.Show();
        }

        //Regex method that checks email validity
        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";  //Before and after @ must be atleast one symbol, then must be dot and after dot atleast one symbol
            return Regex.IsMatch(email, emailPattern);
        }
    }
}
