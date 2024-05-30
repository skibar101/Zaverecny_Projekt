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
        public Form2()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

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

            button1.Click += Button_Click;
            button2.Click += Button_Click;
            button3.Click += Button_Click;
            button4.Click += Button_Click;
            button5.Click += Button_Click;
            button6.Click += Button_Click;
            button7.Click += Button_Click;
            button8.Click += Button_Click;
            button9.Click += Button_Click;

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


        private void button1_MultiplierModifier(object sender, EventArgs e)
        {
            multiplier = 4;
            conditionTitle = "<= 10";
            winCondition = (int[] dice) =>
            {

                return dice.Sum() <= 10;
            };
        }
        private void button2_MultiplierModifier(object sender, EventArgs e)
        {
            multiplier = 3;
            conditionTitle = ">= 20";
            winCondition = (int[] dice) =>
            {

                return dice.Sum() >= 20;
            };

        }


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

        private void button4_MultiplierModifier(object sender, EventArgs e)
        {
            multiplier = 8;
            conditionTitle = "2 Pair";
            winCondition = (int[] dice) =>
            {
                return dice.GroupBy(x => x)
                     .Select(g => g.Count())
                     .OrderBy(c => c)
                     .SequenceEqual(new[] { 1, 2, 2 });
            };
        }

        private void button5_MultiplierModifier(object sender, EventArgs e)
        {
            multiplier = 12;
            conditionTitle = "3 Of a Kind";
            winCondition = (int[] dice) =>
            {
                return dice.GroupBy(x => x)
                        .Any(g => g.Count() == 3);
            };
        }

        private void button6_MultiplierModifier(object sender, EventArgs e)
        {
            multiplier = 25;
            conditionTitle = "Full House";
            winCondition = (int[] dice) =>
            {
                var groups = dice.GroupBy(x => x)
                                .Select(g => g.Count())
                                .OrderByDescending(c => c)
                                .ToArray();
                return groups.SequenceEqual(new[] { 3, 2 });
            };
        }

        private void button7_MultiplierModifier(object sender, EventArgs e)
        {
            multiplier = 45;
            conditionTitle = "4 Of a Kind";
            winCondition = (int[] dice) =>
            {
                return dice.GroupBy(x => x)
                        .Any(g => g.Count() == 4);
            };
        }




        private void button8_MultiplierModifier(object sender, EventArgs e)
        {
            multiplier = 150;
            conditionTitle = "5 Of a Kind";
            winCondition = (int[] dice) =>
            {
                return dice.GroupBy(x => x)
                        .Any(g => g.Count() == 5);
            };
        }


        private void button9_MultiplierModifier(object sender, EventArgs e)
        {
            multiplier = 500;
            conditionTitle = "Straight";
            winCondition = (int[] dice) =>
            {
                var sortedDice = dice.OrderByDescending(x => x).ToArray();
                for (int i = 0; i < sortedDice.Length; i++)
                {
                    sortedDice[i] += i;
                }

                return sortedDice.All(x => x == sortedDice[0]);
            };
        }

        private void AddMoney(int amount)
        {
            BetDAO betDAO = new();

            Bet bet = new Bet(DateTime.Now, amount, true, 0, Game.loggedInUser.Id);
            Game.loggedInUser.Money += amount;
            balance += amount;
            label3.Text = "Balance: €" + balance.ToString();

            betDAO.Save(bet);
        }

        private void RemoveMoney(int amount)
        {
            BetDAO betDAO = new();

            Game.loggedInUser.Money -= amount;
            Bet bet = new Bet(DateTime.Now, amount, false, 0, Game.loggedInUser.Id);

            betDAO.Save(bet);
        }

        public void DetermineWin()
        {
            int totalValue = dice.Sum();


            if (winCondition(dice))
            {
                MessageBox.Show("You won! You won your bet on " + conditionTitle);
                AddMoney(multiplier * betAmount);
            }
            else
            {
                MessageBox.Show("You lost! You bet on " + conditionTitle + " and you lost ");
                RemoveMoney(betAmount);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please place your bet");
            }
            else
            {
                if (int.TryParse(textBox1.Text, out int betAmount))
                {
                    this.betAmount = betAmount;
                    if (betAmount <= 0)
                    {
                        MessageBox.Show("Please enter a valid number");
                    }
                    else
                    {
                        if (betAmount <= balance && betAmount > 0)
                        {
                            label3.Text = "Balance: €" + balance.ToString();
                        
                            RollDice();
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



        private void RollDice()
        {
            rollCount = 0;
            timer1.Interval = 150;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            rollCount++;
            if (rollCount <= maxRollCount)
            {
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


                pictureBox1.Image = diceImages[dice[0]];
                pictureBox2.Image = diceImages[dice[1]];
                pictureBox3.Image = diceImages[dice[2]];
                pictureBox4.Image = diceImages[dice[3]];
                pictureBox5.Image = diceImages[dice[4]];

                CalcValue();
                DetermineWin();


            }
        }
        private void CalcValue()
        {
            int totalValue = 0;
            foreach (int dieValue in dice)
            {
                totalValue += dieValue;
            }
            label1.Text = "Total Value: " + totalValue.ToString();
            // MessageBox.Show(conditionTitle + "\n" + "Won? " + winCondition(dice));
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
