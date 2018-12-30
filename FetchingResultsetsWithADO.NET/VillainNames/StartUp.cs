using InitialSetup;
using System;
using System.Data.SqlClient;

namespace VillainNames
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.connectionString))
            {
                connection.Open();
                connection.ChangeDatabase("MinionsDB");

                string query = "SELECT v.Name,COUNT(mv.MinionId) AS Count FROM Villains AS v JOIN MinionsVillains AS mv" +
                    " ON v.Id = mv.VillainId GROUP BY v.Name HAVING COUNT(mv.MinionId) >= 3 ORDER BY Count DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0]} - {reader[1]}");

                        }
                    }
                }

                connection.Close();
            }
        }
    }
}
