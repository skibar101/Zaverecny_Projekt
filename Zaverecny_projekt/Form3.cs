using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zaverecny_projekt
{

    /// <summary>
    /// Login form
    /// </summary>
    public partial class Form3 : Form
    {
     
        public Form3()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
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

        // <summary>
        /// Method that logs user into the game by button click
        /// </summary>
        //// <param name="sender"> Source of the event</param>
        /// <param name="e"> This contains data of the event</param>
        private void button1_Click(object sender, EventArgs e)
        {
            UserDAO dao = new(); //Instance of user for acces to database

            //Retrieving values from text boxes from the user inputs
            string email = textBox1.Text;
            string pass = textBox2.Text;
            textBox1.Clear();
            textBox2.Clear();

            User? user = dao.GetByEmailAndPassword(email, pass); //Checks if user exist by email and password

            // If user is not found they cant log in
            if (user == null)
            {
                MessageBox.Show("Failed to login");
                return;
            }
            // If user lost all his money, they are banned and cant play anymore
            if (user.Ban)
            {
                MessageBox.Show("You are banned from betting!");
                return;
            }

           
            MessageBox.Show("Successfully logged in"); // Displays if everything is correct
            Game.loggedInUser = user;
            this.Hide();
            Form5 form5 = new Form5();
            form5.Show();
        }
    }
}
