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
    public partial class Form3 : Form
    {
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


        SqlConnection connection = Singleton.GetInstance();
        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from player where mail = '" + textBox1.Text + "' and passw = '" + textBox2.Text + "", connection);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows[0][0].ToString()== "1")
            {
                Form5 form5 = new Form5();
                form5.Show();
                this.Hide();
                connection.Close();
            }
            else
            {
                MessageBox.Show("Wrong email or password");

            }
            connection.Close();
        }
    }
}
