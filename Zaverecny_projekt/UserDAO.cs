using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaverecny_projekt
{
    internal class UserDAO : IDAO<User>
    {
        public void Delete(User user)
        {
            SqlConnection conn = Singleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM player WHERE id = @id", conn))
            {
                command.Parameters.Add(new SqlParameter("@id", user.Id));
                command.ExecuteNonQuery();
                user.Id = 0;
            }
        }

        public IEnumerable<User> GetAll()
        {
            SqlConnection conn = Singleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM player", conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = new User(
                            Convert.ToInt32(reader["id"]),
                            reader["firstName"].ToString(),
                            reader["lastName"].ToString(),
                            reader["email"].ToString(),
                            reader["passw"].ToString(),
                            Convert.ToInt32(reader["money"]),
                            Convert.ToBoolean(reader["ban"])
                        );
                        yield return user;
                    }
                }
            }
        }

        public User? GetByID(int id)
        {
            User? user = null;
            SqlConnection connection = Singleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM player WHERE id = @id", connection))
            {
                command.Parameters.Add(new SqlParameter("@id", id));

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

        public void Save(User user)
        {
            SqlConnection conn = Singleton.GetInstance();
            SqlCommand command = null;

            if (user.Id < 1)
            {
                using (command = new SqlCommand("INSERT INTO player (firstName, lastName, email, passw, money, ban) " +
                    "VALUES (@firstName, @lastName, @Email, @passw, @money, @ban)", conn))
                {
                    command.Parameters.Add(new SqlParameter("@firstName", user.FirstName));
                    command.Parameters.Add(new SqlParameter("@lastName", user.LastName));
                    command.Parameters.Add(new SqlParameter("@Email", user.Email));
                    command.Parameters.Add(new SqlParameter("@passw", user.Password));
                    command.Parameters.Add(new SqlParameter("@money", user.Money));
                    command.Parameters.Add(new SqlParameter("@ban", user.Ban));
                    command.ExecuteNonQuery();

                    command.CommandText = "SELECT @@IDENTITY";
                    user.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            else
            {
                using (command = new SqlCommand("UPDATE player SET firstName = @firstName, lastName = @lastName, " +
                    "email = @Email, passw = @passw, money = @money, ban = @ban WHERE id = @id", conn))
                {
                    command.Parameters.Add(new SqlParameter("@id", user.Id));
                    command.Parameters.Add(new SqlParameter("@firstName", user.FirstName));
                    command.Parameters.Add(new SqlParameter("@lastName", user.LastName));
                    command.Parameters.Add(new SqlParameter("@Email", user.Email));
                    command.Parameters.Add(new SqlParameter("@passw", user.Password));
                    command.Parameters.Add(new SqlParameter("@money", user.Money));
                    command.Parameters.Add(new SqlParameter("@ban", user.Ban));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveAll()
        {
            SqlConnection conn = Singleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM player", conn))
            {
                command.ExecuteNonQuery();
            }
        }

        public User? GetByEmailAndPassword(string email, string password)
        {
            User? user = null;
            SqlConnection connection = Singleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM player WHERE email = @Email AND passw = @Password", connection))
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
    }
}
