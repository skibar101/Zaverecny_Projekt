# Application in C# that contains casino games in WinFroms with users database

Intelligible and easy to understand interface, allows the user to create an account, log in and then play casino games

## Structure of the database

Database has two tables *player* and *bet*. Table *player* stores data about users and table *bet* stores data about bets each user has places.  
Database contains trigger for changing money on users account.


![image](https://github.com/skibar101/Zaverecny_projekt/assets/94451333/f2846dc5-5c08-431d-86f1-b6da4f273e74)



player    
       
    id int primary key identity(1,1),
    
    firstName varchar(20),
    
    lastName varchar(20),
    
    email varchar(20) unique,

    passw varchar(20),

    money int default 500,

    ban bit
    

bet
  
    id int primary key identity(1,1),
    
    date_of_bet date,

    amount int,

    win bit,

    result int,

    player_id int foreign key references player(id),



# Structure of the application

App contains 5 WinForms and 9 other classes that are mostly for interacting with database.

Form1 and Form2 are the casino games.  
Form3 is login form.  
Form4 is sign up form.  
Form 5 is starting screen.


![image](https://github.com/skibar101/Zaverecny_projekt/assets/94451333/88f68667-5deb-4c0d-bdf2-23adfd6908d8)
**All of the classes and forms**

## Casino rules
Every user after succesfull signup and login is given 500 USD after entering casino.  
User can spend theirs money on two games **Slot machine** or **Dices**.  
If user spend all theirs money and is on 0 USD, they wont be able to bet anymore and will be blocked from playing ever again.


## Game 1- Slot machine

First game of the casino is slot machine with 11 possible combinations of winning.  
Game is simple user has to put a bet and then waits if they got any winning combination.

**Winning combinations**

1. 4 Diamonds: 30x bet
2. 4 Golds: 25x bet
3. 4 Silvers: 20x bet
4. 4 Bronzes: 15x bet
5. 3 Diamonds: 15x bet
6. 3 Golds: 10x bet
7. 3 Silvers: 7x bet
8. 3 Bronzes: 4x bet
9. 2 Diamonds and 2 Golds: 12x bet
10. 2 Silvers and 2 Bronzes: 6x bet
11. 2 Diamonds: 4x bet



![image](https://github.com/skibar101/Zaverecny_projekt/assets/94451333/453e1402-b29a-477c-a5e7-6857ae612d77)


## Game 2- Dices

Second game of the casino are dices. Where user can bet each time on one of 9 occurences.  
Game has 5 dices that spin each time user has placed a bet. User wins money if he bets on 1 of 9 occurences and the occurence will happen. 



![image](https://github.com/skibar101/Zaverecny_projekt/assets/94451333/3ba29b53-7d98-4f4d-b3a2-4e937d33db7d)



**Winning occurences**

1. Smaller or equal than 10: 4x bet
2. Greater or equal than 20: 3x bet
3. Between 11-19: 2x bet
4. Two pair: 8x bet
5. Three of a Kind: 12x bet
6. Full House: 25x bet
7. Four of a Kind: 45x bet
8. Five of a Kind: 150x bet
9. Straight: 500x bet



# Readability of the code

Code is properly commented by normal comments and even by comments in XML format.

**Code snippet- 1 (Form4)**

         //Regex method for email validation
        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$"; //Before and after @ must be atleast one symbol, then must be dot and after dot atleast one symbol
            return Regex.IsMatch(email, emailPattern);
        }

        //Regex method for password validation
        private bool IsStrongPassword(string password)
        {
            var passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$"; //Must contain one lowercase, uppercase, one number, one special symbol and must be atleast 8 characters long
            return Regex.IsMatch(password, passwordPattern);
        }

**Code snippet- 2 (UserDAO)**


         /// <summary>
        /// Retrieves a user by email and password from the table
        /// </summary>
        /// <param name="email"> The email of user</param>
        /// <param name="password"> The password of user</param>
        ///  <returns> User if everything is matching otherwise null</returns>
        public User? GetByEmailAndPassword(string email, string password)
        {
            User? user = null;
            SqlConnection connection = Singleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM player WHERE email = @Email AND passw = @Password COLLATE Latin1_General_CS_AS", connection))
            {
                command.Parameters.Add(new SqlParameter("@Email", email));
                command.Parameters.Add(new SqlParameter("@Password", password));

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new User(
                            Convert.ToInt32(reader["id"]),
                            reader["firstName"].ToString(),
                            reader["lastName"].ToString(),
                            reader["email"].ToString(),
                            reader["passw"].ToString(),
                            Convert.ToInt32(reader["money"]),
                            Convert.ToBoolean(reader["ban"])
                        );
                    }
                }
            }
            return user;
        }

       










    
   


