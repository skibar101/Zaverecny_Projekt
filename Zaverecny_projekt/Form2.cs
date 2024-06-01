using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Zaverecny_projekt
{
    public partial class Form2 : Form
    {

        /// <summary>
        /// Form for second game, dices
        /// </summary>
        public Form2()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }
        //Initializing variables
        Image[] diceImages;
        int[] dice;
        Random rn;
        int rollCount;
        const int maxRollCount = 20;
        private int balance = Game.loggedInUser.Money;

        private int multiplier = 0;
        private int betAmount;

        private Func<int[], bool> winCondition;

        string conditionTitle = "";

        private void Form2_Load(object sender, EventArgs e)
        {
            label3.Text = "Balance: €" + balance.ToString();

            //Loading images into array
            diceImages = new Image[7];
            diceImages[0] = Properties.Resources.empty;
            diceImages[1] = Properties.Resources.dice1jecna;
            diceImages[2] = Properties.Resources.dice2jecna;
            diceImages[3] = Properties.Resources.dice3jecna;
            diceImages[4] = Properties.Resources.dice4jecna;
            diceImages[5] = Properties.Resources.dice5jecna;
            diceImages[6] = Properties.Resources.dice6jecna;

            dice = new int[5] { 0, 0, 0, 0, 0 };

            rn = new Random();

            //When clicked it rolls dices and checks if bet is OK
            button1.Click += Button_Click;
            button2.Click += Button_Click;
            button3.Click += Button_Click;
            button4.Click += Button_Click;
            button5.Click += Button_Click;
            button6.Click += Button_Click;
            button7.Click += Button_Click;
            button8.Click += Button_Click;
            button9.Click += Button_Click;

            //When clicked user wins what he bets on
            button1.Click += button1_MultiplierModifier;
            button2.Click += button2_MultiplierModifier;
            button3.Click += button3_MultiplierModifier;
            button4.Click += button4_MultiplierModifier;
            button5.Click += button5_MultiplierModifier;
            button6.Click += button6_MultiplierModifier;
            button7.Click += button7_MultiplierModifier;
            button8.Click += button8_MultiplierModifier;
            button9.Click += button9_MultiplierModifier;
        }

        //Method for first button, when clicked and user wins, user wins 4x theirs money
        //User wins if value of dices is smaller or even than 10
        private void button1_MultiplierModifier(object sender, EventArgs e)
        {
            multiplier = 4;
            conditionTitle = "<= 10";
            winCondition = (int[] dice) =>
            {

                return dice.Sum() <= 10; 
            };
        }
        //Method for second button, when clicked and user wins, user wins 3x theirs money
        //User wins if value of dices is greater or even than 20
        private void button2_MultiplierModifier(object sender, EventArgs e)
        {
            multiplier = 3;
            conditionTitle = ">= 20";
            winCondition = (int[] dice) =>
            {

                return dice.Sum() >= 20;
            };

        }

        //Method for third button, when clicked and user wins, user wins 2x theirs money
        //User wins if value of dices is between 11 and 19
        private void button3_MultiplierModifier(object sender, EventArgs e)
        {
            multiplier = 2;
            conditionTitle = "11 - 19";
            winCondition = (int[] dice) =>
            {
                int sum = dice.Sum();

                return sum >= 11 && sum <= 19;
            };
        }

        //Method for fourth button, when clicked and user wins, user wins 8x theirs money
        //User wins if value of dices has two pair
        private void button4_MultiplierModifier(object sender, EventArgs e)
        {
            multiplier = 8;
            conditionTitle = "2 Pair";
            winCondition = (int[] dice) =>
            {
                return dice.GroupBy(x => x) //Group dices by their values and count occurence each value
                     .Select(g => g.Count()) //Select each value
                     .OrderBy(c => c) //Orders the values
                     .SequenceEqual(new[] { 1, 2, 2 }); //Check if two pair is occurs
            };
        }

        //Method for fifth button, when clicked and user wins, user wins 12x theirs money
        //User wins if value of dices has three of a kind
        private void button5_MultiplierModifier(object sender, EventArgs e)
        {
            multiplier = 12;
            conditionTitle = "3 Of a Kind";
            winCondition = (int[] dice) =>
            {
                return dice.GroupBy(x => x) //Group the values
                        .Any(g => g.Count() == 3); //Check if any group of values has 3 dices with same value
            };
        }

        //Method for sixth button, when clicked and user wins, user wins 25x theirs money
        //User wins if value of dices has  full house
        private void button6_MultiplierModifier(object sender, EventArgs e)
        {
            multiplier = 25;
            conditionTitle = "Full House";
            winCondition = (int[] dice) =>
            {
                var groups = dice.GroupBy(x => x) //Group dices by their values and count occurence each value
                                .Select(g => g.Count()) //Select each value
                                .OrderByDescending(c => c) //Orders the values
                                .ToArray(); //Convert to array
                return groups.SequenceEqual(new[] { 3, 2 }); //Check if full house occurs
            };
        }
        //Method for seventh button, when clicked and user wins, user wins 45x theirs money
        //User wins if value of dices has four of a kind
        private void button7_MultiplierModifier(object sender, EventArgs e)
        {
            multiplier = 45;
            conditionTitle = "4 Of a Kind";
            winCondition = (int[] dice) =>
            {
                return dice.GroupBy(x => x) //Group the values
                        .Any(g => g.Count() == 4); //Check if any group of values has 4 dices with same value
            };
        }



        //Method for eigth button, when clicked and user wins, user wins 150x theirs money
        //User wins if value of dices has five of a kind
        private void button8_MultiplierModifier(object sender, EventArgs e)
        {
            multiplier = 150;
            conditionTitle = "5 Of a Kind";
            winCondition = (int[] dice) =>
            {
                return dice.GroupBy(x => x) //Group the values
                        .Any(g => g.Count() == 5); //Check if any group of values has 5 dices with same value
            };
        }

        //Method for ninth button, when clicked and user wins, user wins 500x theirs money
        //User wins if value of dices has straight
        private void button9_MultiplierModifier(object sender, EventArgs e)
        {
            multiplier = 500;
            conditionTitle = "Straight";
            winCondition = (int[] dice) =>
            {
                // Sorts the die from high to low and then adds their index to final value. if all dice have the same value, it means user has straight
                var sortedDice = dice.OrderByDescending(x => x).ToArray(); 

              
                for (int i = 0; i < sortedDice.Length; i++)
                {
                    sortedDice[i] += i;
                }

                return sortedDice.All(x => x == sortedDice[0]); /
            };
        }
        /// <summary>
        /// Method that adds money to users balance if they win
        /// </summary>
        /// <param name="amount"> How much the user will gain</param>
        private void AddMoney(int amount)
        {
            BetDAO betDAO = new(); //Acces to database

            Bet bet = new Bet(DateTime.Now, amount, true, 0, Game.loggedInUser.Id); //Creating object of bet
            Game.loggedInUser.Money += amount; //Updates users money
            balance += amount; //Updates local variable
            label3.Text = "Balance: €" + balance.ToString(); //Updates label

            betDAO.Save(bet); //Saving to database
        }
        // <summary>
        /// Method that removes money from users balance if they lose
        /// </summary>
        /// <param name="amount"> How much the user will loose</param>
        private void RemoveMoney(int amount)
        {
            BetDAO betDAO = new(); //Acces to database

            Bet bet = new Bet(DateTime.Now, amount, false, 0, Game.loggedInUser.Id); //Creating object of bet
            Game.loggedInUser.Money -= amount; //Updates users money
            balance -= amount; //Updates local variable
            label3.Text = "Balance: €" + balance.ToString(); //Updating the label

            betDAO.Save(bet); //Saving to database
        }

        /// <summary>
        /// Method that checks if user has won or not
        /// </summary>
        public void DetermineWin()
        {
            int totalValue = dice.Sum();

            //If user has won
            if (winCondition(dice))
            {
                MessageBox.Show("You won! You won your bet on " + conditionTitle);
                AddMoney(multiplier * betAmount); //Adds money to users balance
            }
            //Else user has lost
            else
            {
                MessageBox.Show("You lost! You bet on " + conditionTitle + " and you lost ");
                RemoveMoney(betAmount); //Removes money from users balance
            }
        }

        // <summary>
        /// Method that is compiling users bet
        /// </summary>
        //// <param name="sender"> Source of the event</param>
        /// <param name="e"> This contains data of the event</param>
        private void Button_Click(object sender, EventArgs e)
        {
            //Checks if the user has placed bet
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please place your bet");
            }
            else
            {
                //Parsing the amount from the text box
                if (int.TryParse(textBox1.Text, out int betAmount))
                {
                    this.betAmount = betAmount;
                    if (betAmount <= 0)
                    {
                        MessageBox.Show("Please enter a valid number");
                    }
                    else
                    {
                        //If users bet is smaller or equal than users balance and bet amount is greater than zero, rolling dices
                        if (betAmount <= balance && betAmount > 0)
                        {
                            label3.Text = "Balance: €" + balance.ToString(); //Updating label
                        
                            RollDice(); //Rolling dices
                        }
                        else
                        {
                            MessageBox.Show("You don't have enough balance");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid number");
                }
            }
        }


        /// <summary>
        /// Method that starts the timer and initializes values for dices
        /// </summary>
        private void RollDice()
        {
            rollCount = 0;
            timer1.Interval = 150;
            timer1.Start();
        }

        // <summary>
        /// Method that is updating images of dices and check if user has won
        /// </summary>
        //// <param name="sender"> Source of the event</param>
        /// <param name="e"> This contains data of the event</param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            rollCount++;
            // Check¨s if the current roll count is less or equal to the maximum roll count
            if (rollCount <= maxRollCount)
            {
                // For loop that assigns random image to a each picture box 
                for (int i = 0; i < dice.Length; i++)
                {
                    dice[i] = rn.Next(1, 6 + 1);
                    pictureBox1.Image = diceImages[rn.Next(1, 6 + 1)];
                    pictureBox2.Image = diceImages[rn.Next(1, 6 + 1)];
                    pictureBox3.Image = diceImages[rn.Next(1, 6 + 1)];
                    pictureBox4.Image = diceImages[rn.Next(1, 6 + 1)];
                    pictureBox5.Image = diceImages[rn.Next(1, 6 + 1)];
                }
            }
            else
            {
                timer1.Stop();

                // Sets final image to a picture boxes
                pictureBox1.Image = diceImages[dice[0]];
                pictureBox2.Image = diceImages[dice[1]];
                pictureBox3.Image = diceImages[dice[2]];
                pictureBox4.Image = diceImages[dice[3]];
                pictureBox5.Image = diceImages[dice[4]];

                CalcValue(); //Calculating values of dices
                DetermineWin(); //Check if user has won


            }
        }

        /// <summary>
        /// Calculates the total value of the dices 
        /// </summary>
        private void CalcValue()
        {
            int totalValue = 0;

            // For each loop that goes through each die value in the dice array
            foreach (int dieValue in dice)
            {
                totalValue += dieValue; // Adds the value of the current die to the total value
            }
            label1.Text = "Total Value: " + totalValue.ToString(); //Updating the label
            
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form5 form5 = new Form5();
            form5.Show();
        }
    }
}
