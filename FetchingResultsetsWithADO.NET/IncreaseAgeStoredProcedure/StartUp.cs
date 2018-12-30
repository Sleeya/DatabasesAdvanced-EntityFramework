using InitialSetup;
using System;
using System.Data.SqlClient;

namespace IncreaseAgeStoredProcedure
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int minionId = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(Configuration.connectionString))
            {
                connection.Open();
                connection.ChangeDatabase("MinionsDB");

                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    ExecuteAgingProcedure(connection, transaction, minionId);
                    PrintAgedMinion(connection, transaction, minionId);
                }
                catch (SqlException e)
                {

                    Console.WriteLine(e.Message);
                }

                transaction.Commit();
                connection.Close();
            }
        }

        private static void PrintAgedMinion(SqlConnection connection, SqlTransaction transaction, int minionId)
        {
            string findMinionQuery = "SELECT Name,Age FROM Minions WHERE Id = @Id";

            using (SqlCommand findMinionCommand = new SqlCommand(findMinionQuery, connection, transaction))
            {
                findMinionCommand.Parameters.AddWithValue("@Id", minionId);

                using (SqlDataReader reader = findMinionCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader[0]} - {reader[1]} years old");
                    }
                }
            }
        }

        private static void ExecuteAgingProcedure(SqlConnection connection, SqlTransaction transaction, int minionId)
        {
            string agingQuery = "EXEC usp_GetOlder @MinionId";

            using (SqlCommand agingCommand = new SqlCommand(agingQuery,connection,transaction))
            {
                agingCommand.Parameters.AddWithValue("@MinionId", minionId);
                agingCommand.ExecuteNonQuery();
            }
        }
    }
}
