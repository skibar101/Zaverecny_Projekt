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
        /// <summary>
        /// atributes from the table bet, getters, setters, constructors, toString method and methods for validation
        /// </summary>

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
        public string Email
        {
            get => email;
            set
            {
                //if statement that checks format of the email that user has wrote
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
                //if statement that checks strength of the password that user has wrote
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

        /// <summary>
        /// Function that validates an email format.
        /// </summary>
        /// <param name="email">The email that has to be validated.</param>
        /// <returns>valid email</returns>
        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$"; // Regex for the email validation
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
