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

        private void Login(object sender, EventArgs e)
        {
            UserDAO dao = new();

            string email = textBox1.Text;
            string pass = textBox2.Text;

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Invalid email format.");
                return;
            }

            textBox1.Clear();
            textBox2.Clear();

            User? user = dao.GetByEmailAndPassword(email, pass);

            if (user == null)
            {
                MessageBox.Show("Failed to login");
                return;
            }

            if (user.Ban)
            {
                MessageBox.Show("You are BANNED!");
                return;
            }

            MessageBox.Show("Successfully logged in");
            Game.loggedInUser = user;
            this.Hide();
            Form5 form5 = new Form5();
            form5.Show();
        }

        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}
