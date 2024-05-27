using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Zaverecny_projekt
{
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

        public int Id { get => id; set => id = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email
        {
            get => email;
            set
            {
                if (!IsValidEmail(value))
                {
                    throw new ArgumentException("Invalid email format.");
                }
                email = value;
            }
        }
        public string Password
        {
            get => password;
            set
            {
                if (!IsStrongPassword(value))
                {
                    throw new ArgumentException("Password is not strong enough.");
                }
                password = value;
            }
        }
        public int Money { get => money; set => money = value; }
        public bool Ban { get => ban; set => ban = value; }
        public override string ToString()
        {
            return $"{id}. {FirstName} {LastName}, Email: {Email}, Money: {Money}, Banned: {Ban}";
        }

        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        private bool IsStrongPassword(string password)
        {
            if (password.Length < 8)
            {
                return false;
            }

            bool hasUpperCase = false, hasLowerCase = false, hasDigit = false, hasSpecialChar = false;
            foreach (var ch in password)
            {
                if (char.IsUpper(ch)) hasUpperCase = true;
                if (char.IsLower(ch)) hasLowerCase = true;
                if (char.IsDigit(ch)) hasDigit = true;
                if (!char.IsLetterOrDigit(ch)) hasSpecialChar = true;
            }

            return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
        }
    }
}
