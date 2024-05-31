using Microsoft.Data.SqlClient;
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
    internal class BetDAO : IDAO<Bet>
    {
        /// <summary>
        /// Deletes entity from database
        /// </summary>
        /// <param name="bet">Entoty that has to be deleted</param>
        public void Delete(Bet bet)
        {
            SqlConnection conn = Singleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM bet WHERE id = @id", conn))
            {
                command.Parameters.Add(new SqlParameter("@id", bet.Id));
                command.ExecuteNonQuery();
                bet.Id = 0;
            }
        }

        // <summary>
        /// Loads all entities from database
        /// </summary>
        /// <returns> All entities</returns>
        public IEnumerable<Bet> GetAll()
        {
            SqlConnection conn = Singleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM bet", conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Bet bet = new Bet(
                            Convert.ToInt32(reader["id"]),
                            Convert.ToDateTime(reader["date_of_bet"]),
                            Convert.ToInt32(reader["amount"]),
                            Convert.ToBoolean(reader["win"]),
                            Convert.ToInt32(reader["result"]),
                            Convert.ToInt32(reader["player_id"])
                        );
                        yield return bet;
                    }
                }
            }
        }
        /// <summary>
        /// Loads entity by id
        /// </summary>
        /// <param name="id"> id of entity</param>
        /// <returns> Entity or null if not found</returns>
        public Bet? GetByID(int id)
        {
            Bet? bet = null;
            SqlConnection connection = Singleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM bet WHERE id = @id", connection))
            {
                command.Parameters.Add(new SqlParameter("@id", id));

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        bet = new Bet(
                            Convert.ToInt32(reader["id"]),
                            Convert.ToDateTime(reader["date_of_bet"]),
                            Convert.ToInt32(reader["amount"]),
                            Convert.ToBoolean(reader["win"]),
                            Convert.ToInt32(reader["result"]),
                            Convert.ToInt32(reader["player_id"])
                        );
                    }
                }
            }
            return bet;
        }
        /// <summary>
        /// Saves entity to database
        /// </summary>
        /// <param name="bet"> entity that has to be saved</param>
        public void Save(Bet bet)
        {
            SqlConnection conn = Singleton.GetInstance();
            SqlCommand command = null;

            if (bet.Id < 1)
            {
                using (command = new SqlCommand("INSERT INTO bet (date_of_bet, amount, win, result, player_id) " +
                    "VALUES (@dateOfBet, @amount, @win, @result, @playerId)", conn))
                {
                    command.Parameters.Add(new SqlParameter("@dateOfBet", bet.DateOfBet));
                    command.Parameters.Add(new SqlParameter("@amount", bet.Amount));
                    command.Parameters.Add(new SqlParameter("@win", bet.Win));
                    command.Parameters.Add(new SqlParameter("@result", bet.Result));
                    command.Parameters.Add(new SqlParameter("@playerId", bet.PlayerId));
                    command.ExecuteNonQuery();

                    command.CommandText = "SELECT @@IDENTITY";
                    bet.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            else
            {
                using (command = new SqlCommand("UPDATE bet SET date_of_bet = @dateOfBet, amount = @amount, " +
                    "win = @win, result = @result, player_id = @playerId WHERE id = @id", conn))
                {
                    command.Parameters.Add(new SqlParameter("@id", bet.Id));
                    command.Parameters.Add(new SqlParameter("@dateOfBet", bet.DateOfBet));
                    command.Parameters.Add(new SqlParameter("@amount", bet.Amount));
                    command.Parameters.Add(new SqlParameter("@win", bet.Win));
                    command.Parameters.Add(new SqlParameter("@result", bet.Result));
                    command.Parameters.Add(new SqlParameter("@playerId", bet.PlayerId));
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

            using (SqlCommand command = new SqlCommand("DELETE FROM bet", conn))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
