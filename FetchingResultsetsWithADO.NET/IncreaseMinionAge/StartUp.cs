using InitialSetup;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace IncreaseMinionAge
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int[] minionIds = Console.ReadLine().Split().Select(int.Parse).ToArray();

            using (SqlConnection connection = new SqlConnection(Configuration.connectionString))
            {
                connection.Open();
                connection.ChangeDatabase("MinionsDB");
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    UpdateMinionsAgeAndUpperFirstLetterInName(connection, minionIds, transaction);
                    PrintMinionNameAndAge(connection,transaction);

                }
                catch (SqlException e)
                {

                    Console.WriteLine(e.Message);
                    transaction.Rollback();
                }
                connection.Close();
            }
        }

        private static void PrintMinionNameAndAge(SqlConnection connection,SqlTransaction transaction)
        {
            string printQuery = "SELECT Name,Age FROM Minions";
            using (SqlCommand printCommand = new SqlCommand(printQuery,connection, transaction))
            {
                using (SqlDataReader reader = printCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader[0]} {reader[1]}");
                    }
                }
            }
        }

        private static void UpdateMinionsAgeAndUpperFirstLetterInName(SqlConnection connection, int[] minionIds, SqlTransaction transaction)
        {
            string updateQuery = "UPDATE Minions SET Age += 1, Name = CONCAT(UPPER(LEFT(Name,1)),SUBSTRING(Name,2,LEN(Name))) WHERE Id = @Id";
            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection, transaction))
            {
                updateCommand.Parameters.AddWithValue("@Id", 0);
                for (int i = 0; i < minionIds.Length; i++)
                {
                    updateCommand.Parameters["@Id"].Value = minionIds[i];
                    updateCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
