using System.Collections;
using System.ComponentModel;
using System.Drawing.Text;
using System.Net.Http.Headers;

namespace Zaverecny_projekt
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// First game, slot machine.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        Random rn = new Random();

        int a, b, c, d, move;


        private int balance = Game.loggedInUser.Money;

        /// <summary>
        /// Method that adds money to users balance if they win
        /// </summary>
        /// <param name="amount"> How much the user will gain</param>
        private void AddMoney(int amount)
        {
            BetDAO betDAO = new(); //Acces to database

            Bet bet = new Bet(DateTime.Now, amount, true, 0, Game.loggedInUser.Id); //Creating object
            Game.loggedInUser.Money += amount; //Updates users monay
            balance += amount; // Updates local variable
            label1.Text = "Balance: €" + balance.ToString(); // Updating the label

            betDAO.Save(bet); //Saving to database
        }

        /// <summary>
        /// Method that removes money from users balance if they lose
        /// </summary>
        /// <param name="amount"> How much the user will loose</param>
        private void RemoveMoney(int amount)
        {
            BetDAO betDAO = new(); //Acces to database

            Bet bet = new Bet(DateTime.Now, amount, false, 0, Game.loggedInUser.Id); //Creating object of bet
            Game.loggedInUser.Money -= amount; //Updates users money
            balance -= amount; //Updates local variable
            label1.Text = "Balance: €" + balance.ToString(); //Updating the label

            betDAO.Save(bet); //Saving to database
        }

        private void ChangeBet()
        {
            textBox1.Enabled = true;

        }

        /// <summary>
        /// Method to check the results of the game
        /// </summary>
        private void CheckWin()
        {
           
            int[] results = new int[] { a, b, c, d };  // Array for the results 

            // Counts each symbol
            int diamondCount = results.Count(x => x == 0);
            int bronzeCount = results.Count(x => x == 1);
            int silverCount = results.Count(x => x == 2);
            int goldCount = results.Count(x => x == 3);

            // If statement for various winning combinations and update the user's balance accordingly to what they won
            // 4 logos- best possible outcome
            if (diamondCount == 4)
            {

                MessageBox.Show("You won 30 times your bet!");
                AddMoney(Convert.ToInt32(textBox1.Text) * 30);

            }
            else if (goldCount == 4)
            {

                MessageBox.Show("You won 25 times your bet!");
                AddMoney(Convert.ToInt32(textBox1.Text) * 25);

            }
            else if (silverCount == 4)
            {

                MessageBox.Show("You won 20 times your bet!");
                AddMoney(Convert.ToInt32(textBox1.Text) * 20);

            }
            else if (bronzeCount == 4)
            {

                MessageBox.Show("You won 15 times your bet!");
                AddMoney(Convert.ToInt32(textBox1.Text) * 15);

            }



            // 3 logos
            else if (diamondCount == 3)
            {

                MessageBox.Show("You won 15 times your bet!");
                AddMoney(Convert.ToInt32(textBox1.Text) * 15);
            }
            else if (goldCount == 3)
            {

                MessageBox.Show("You won 10 times your bet!");
                AddMoney(Convert.ToInt32(textBox1.Text) * 10);

            }
            else if (silverCount == 3)
            {

                MessageBox.Show("You won 7 times your bet!");
                AddMoney(Convert.ToInt32(textBox1.Text) * 7);

            }
            else if (bronzeCount == 3)
            {

                MessageBox.Show("You won 4 times your bet!");
                AddMoney(Convert.ToInt32(textBox1.Text) * 4);



                //combinations

            }
            else if ((diamondCount == 2))
            {
                if (goldCount == 2)
                {
                    AddMoney(Convert.ToInt32(textBox1.Text) * 12);
                    MessageBox.Show("You won 12 times your bet!");

                }
                else
                {
                    MessageBox.Show("You won 4 times your bet!");
                    AddMoney(Convert.ToInt32(textBox1.Text) * 4);

                }


            }
            else if (silverCount == 2 && bronzeCount == 2)
            {

                MessageBox.Show("You won 6 times your bet!");
                AddMoney(Convert.ToInt32(textBox1.Text) * 6);
            }



            else
            {
                RemoveMoney(Convert.ToInt32(textBox1.Text));
                MessageBox.Show("Sorry, you didn't win this time");

            }
        }
        /// <summary>
        /// Initializes the users balance and updates the label
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            balance = Game.loggedInUser.Money;
            label1.Text = "Balance: €" + balance.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // <summary>
        /// Method that is compiling users bet
        /// </summary>
        //// <param name="sender"> Source of the event</param>
        /// <param name="e"> This contains data of the event</param>
        private void button1_Click(object sender, EventArgs e)
        {
            //Checks if text box is empty
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please place your bet");
            }
            else
            {
                //Parsing the amount from the text box
                if (int.TryParse(textBox1.Text, out int betAmount))
                {
                    //Checks if the bet amount is smaller or equal to users balance and bet amount is greater than 0
                    if (betAmount <= balance && betAmount > 0)
                    {
                        label1.Text = "Balance: €" + balance.ToString();
                        timer1.Enabled = true;
                        textBox1.Enabled = false;

                    }
                    else
                    {
                        MessageBox.Show("You don't have enough balance");//Displays if users bet is greater than your balance
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid number");//Displays if users bet isnt valid number
                }
            }
        }
        // <summary>
        /// Method that is updating images of logos and check if user has won
        /// </summary>
        //// <param name="sender"> Source of the event</param>
        /// <param name="e"> This contains data of the event</param>

        private void timer1_Tick(object sender, EventArgs e)
        {
            //If move is smaller than 30 it keeps on updating images
            move++;
            if (move < 30)
            {
                // Generate random number for each picture box
                a = rn.Next(4);
                b = rn.Next(4);
                c = rn.Next(4);
                d = rn.Next(4);

                //Generates 4 random images into 4 picture boxes
                switch (a)
                {
                    case 1:
                        pictureBox1.Image = Properties.Resources.bronze;
                        break;
                    case 2:
                        pictureBox1.Image = Properties.Resources.silver;
                        break;
                    case 3:
                        pictureBox1.Image = Properties.Resources.gold;
                        break;
                    case 0:
                        pictureBox1.Image = Properties.Resources.diamond;
                        break;
                }

                switch (b)
                {
                    case 1:
                        pictureBox2.Image = Properties.Resources.bronze;
                        break;
                    case 2:
                        pictureBox2.Image = Properties.Resources.silver;
                        break;
                    case 3:
                        pictureBox2.Image = Properties.Resources.gold;
                        break;
                    case 0:
                        pictureBox2.Image = Properties.Resources.diamond;
                        break;
                }

                switch (c)
                {
                    case 1:
                        pictureBox3.Image = Properties.Resources.bronze;
                        break;
                    case 2:
                        pictureBox3.Image = Properties.Resources.silver;
                        break;
                    case 3:
                        pictureBox3.Image = Properties.Resources.gold;
                        break;
                    case 0:
                        pictureBox3.Image = Properties.Resources.diamond;
                        break;
                }

                switch (d)
                {
                    case 1:
                        pictureBox4.Image = Properties.Resources.bronze;
                        break;
                    case 2:
                        pictureBox4.Image = Properties.Resources.silver;
                        break;
                    case 3:
                        pictureBox4.Image = Properties.Resources.gold;
                        break;
                    case 0:
                        pictureBox4.Image = Properties.Resources.diamond;
                        break;
                }
            }
            else
            {
                timer1.Enabled = false;
                move = 0;
                CheckWin();
                ChangeBet();
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form5 form5 = new Form5();
            form5.Show();
        }
    }
}