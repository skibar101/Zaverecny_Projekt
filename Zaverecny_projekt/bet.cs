using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaverecny_projekt
{
    internal class bet: IBaseClass
    {
        private int id;
        private DateTime date_of_bet;
        private int amount;
        private bool win;
        private int result;
        private int player_id;


        public bet()
        {
        }

        public bet(int id, DateTime date_of_bet, int amount, bool win, int result, int player_id)
        {
            this.id = id;
            this.date_of_bet = date_of_bet;
            this.amount = amount;
            this.win = win;
            this.result = result;
            this.player_id = player_id;
        }

        public bet(DateTime date_of_bet, int amount, bool win, int result, int player_id)
        {
            this.id = 0;
            this.date_of_bet = date_of_bet;
            this.amount = amount;
            this.win = win;
            this.result = result;
            this.player_id = player_id;
        }

        public int Id { get => id; set => id = value; }
        public DateTime Date_of_bet { get => date_of_bet; set => date_of_bet = value; }
        public int Amount { get => amount; set => amount = value; }
        public bool Win { get => win; set => win = value; }
        public int Result { get => result; set => result = value; }
        public int Player_id { get => player_id; set => player_id = value; }
    }
}
