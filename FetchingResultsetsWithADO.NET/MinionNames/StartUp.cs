using InitialSetup;
using System;
using System.Data.SqlClient;

namespace MinionNames
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int villainId = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(Configuration.connectionString))
            {
                connection.Open();
                connection.ChangeDatabase("MinionsDB");

                string villainName = GetVillainName(connection, villainId);

                if (villainName == null)
                {
                    Console.WriteLine($"No villain with ID {villainId} exists in the database.");
                }
                else
                {
                    PrintVillainAndMinionsIfAny(villainId, villainName, connection);
                }

                connection.Close();
            }
        }

        private static void PrintVillainAndMinionsIfAny(int villainId, string villainName,SqlConnection connection)
        {
            string query = "SELECT m.Name,m.Age FROM Minions AS m " +
                                "JOIN MinionsVillains AS mv ON m.Id = mv.MinionId WHERE mv.VillainId = @VillainId " +
                                "ORDER BY m.Name";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@VillainId", villainId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine($"Villain: {villainName}");
                    if (reader.HasRows)
                    {
                        int counter = 1;
                        while (reader.Read())
                        {
                            Console.WriteLine($"{counter}. {reader[0]} {reader[1]}");
                            counter++;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"(no minions)");
                    }

                }
            }
        }

        private static string GetVillainName(SqlConnection connection, int villainId)
        {
            string query = "SELECT Name FROM Villains WHERE Id = @Id";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", villainId);

                return (string)command.ExecuteScalar();
            }
        }
    }
}
