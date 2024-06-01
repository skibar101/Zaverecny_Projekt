using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Zaverecny_projekt
{

    /// <summary>
    /// Class for user, getters setters, constructors and toString method
    /// </summary>
    internal class User : IBaseClass
    {
        private int id;
        private string firstName;
        private string lastName;
        private string email;
        private string password;
        private int money;
        private bool ban;

        public User(int id, string firstName, string lastName, string email, string password, int money, bool ban)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.password = password;
            this.money = money;
            this.ban = ban;
        }

        public User(string firstName, string lastName, string email, string password)
        {
            this.id = 0;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.password = password;
            this.money = 500;
            this.ban = false;
        }

        public int Id { get => id; set => id = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public int Money { get => money; set => money = value; }
        public bool Ban { get => ban; set => ban = value; }

        public override string ToString()
        {
            return $"{id}. {FirstName} {LastName}, Email: {Email}, Money: {Money}, Banned: {Ban}";
        }
    }
}
