using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaverecny_projekt
{
    internal class Bet : IBaseClass
    {
        /// <summary>
        /// atributes from the table bet, getters, setters, constructors and toString method
        /// </summary>
        private int id;
        private DateTime dateOfBet;
        private int amount;
        private bool win;
        private int result;
        private int playerId;

        public int Id { get => id; set => id = value; }
        public DateTime DateOfBet { get => dateOfBet; set => dateOfBet = value; }
        public int Amount { get => amount; set => amount = value; }
        public bool Win { get => win; set => win = value; }
        public int Result { get => result; set => result = value; }
        public int PlayerId { get => playerId; set => playerId = value; }

        public Bet(int id, DateTime dateOfBet, int amount, bool win, int result, int playerId)
        {
            this.id = id;
            this.DateOfBet = dateOfBet;
            this.Amount = amount;
            this.Win = win;
            this.Result = result;
            this.PlayerId = playerId;
        }

        public Bet(DateTime dateOfBet, int amount, bool win, int result, int playerId)
        {
            this.id = 0;
            this.DateOfBet = dateOfBet;
            this.Amount = amount;
            this.Win = win;
            this.Result = result;
            this.PlayerId = playerId;
        }

        public override string ToString()
        {
            return $"{id}. Date: {DateOfBet}, Amount: {Amount}, Win: {Win}, Result: {Result}, Player ID: {PlayerId}";
        }
    }
}
