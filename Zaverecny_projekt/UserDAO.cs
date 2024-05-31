using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaverecny_projekt
{
    // <summary>
    /// Class that acces data using DAO
    /// </summary>

    internal class UserDAO : IDAO<User>
    {
        /// <summary>
        /// Deletes entity from database
        /// </summary>
        /// <param name="user">Entity that has to be deleted</param>

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

        // <summary>
        /// Loads all entities from database
        /// </summary>
        /// <returns> All entities</returns>

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
        /// <summary>
        /// Loads entity by id
        /// </summary>
        /// <param name="id"> id of entity</param>
        /// <returns> Entity or null if not found</returns>
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
        /// <summary>
        /// Saves entity to database
        /// </summary>
        /// <param name="user"> entity that has to be saved</param>

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
        // <summary>
        /// Removes all data from table
        /// </summary>

        public void RemoveAll()
        {
            SqlConnection conn = Singleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM player", conn))
            {
                command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Retrieves a user by email and password from the table
        /// </summary>
        /// <param name="email"> The email of user</param>
        /// <param name="password"> The password of user</param>
        /// /// <returns>
        /// User if everything is matching otherwise null</c>.
        /// </returns>
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
    }
}
