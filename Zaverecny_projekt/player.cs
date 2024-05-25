using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaverecny_projekt
{
    internal class player : IBaseClass
    {
        private int id;
        private string firstName;
        private string lastName;
        private string email;
        private string passw;
        private int money;
        private bool ban;

        public int Id { get => id; set => id = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }
        public string Passw { get => passw; set => passw = value; }
        public int Money { get => money; set => money = value; }
        public bool Ban { get => ban; set => ban = value; }

        public player(){}

        public player(int id, string firstName, string lastName, string email, string passw, int money, bool ban)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.passw = passw;
            this.money = money;
            this.ban = ban;
        }

        public player(string firstName, string lastName, string email, string passw, int money, bool ban)
        {
            this.id = 0;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.passw = passw;
            this.money = money;
            this.ban = ban;
        }
    }
}
