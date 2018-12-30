using InitialSetup;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PrintAllMinionsNames
{
    class StartUp
    {
        static void Main(string[] args)
        {
            List<string> minionNames = new List<string>();

            using (SqlConnection connection = new SqlConnection(Configuration.connectionString))
            {
                connection.Open();
                connection.ChangeDatabase("MinionsDB");

                minionNames = GetMinionNames(connection);
                connection.Close();
            }

            for (int i = 0; i < minionNames.Count / 2; i++)
            {
                Console.WriteLine(minionNames[i]);
                Console.WriteLine(minionNames[minionNames.Count - 1 - i]);
            }
        }

        private static List<string> GetMinionNames(SqlConnection connection)
        {
            List<string> minions = new List<string>();

            string getNamesQuery = "SELECT Name FROM Minions";
            using (SqlCommand getNamesCommand = new SqlCommand(getNamesQuery, connection))
            {
                using (SqlDataReader reader = getNamesCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        minions.Add(reader[0].ToString());
                    }
                }
            }

            return minions;
        }
    }
}
