using InitialSetup;
using System;
using System.Data.SqlClient;

namespace RemoveVillain
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
                SqlTransaction transaction = connection.BeginTransaction();

                string villainName = GetVillainNameIfExists(connection, villainId);

                if (villainName == String.Empty)
                {
                    Console.WriteLine("No such villain was found.");
                }
                else
                {
                    try
                    {
                        int numberOfMinionsReleased = ReleaseMinionsOffTheVillain(connection, villainId, transaction);
                        DeleteVillain(connection, villainId, transaction);
                        Console.WriteLine($"{villainName} was deleted.");
                        Console.WriteLine($"{numberOfMinionsReleased} were released.");
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine(e.Message);
                        transaction.Rollback();
                    }
                }
                connection.Close();
            }
        }

        private static void DeleteVillain(SqlConnection connection, int villainId, SqlTransaction transaction)
        {
            string deleteVillainQuery = "DELETE FROM Villains WHERE Id = @VillainId";
            using (SqlCommand deleteCommand = new SqlCommand(deleteVillainQuery,connection, transaction))
            {
                deleteCommand.Parameters.AddWithValue("@VillainId", villainId);
                deleteCommand.ExecuteNonQuery();
            }
        }

        private static int ReleaseMinionsOffTheVillain(SqlConnection connection, int villainId, SqlTransaction transaction)
        {
            string deleteMatchingMinionsQuery = "DELETE FROM MinionsVillains WHERE VillainId = @VillainId";
            using (SqlCommand deleteCommand = new SqlCommand(deleteMatchingMinionsQuery, connection, transaction))
            {
                deleteCommand.Parameters.AddWithValue("@VillainId", villainId);

                return deleteCommand.ExecuteNonQuery();
            }
        }

        private static string GetVillainNameIfExists(SqlConnection connection, int villainId)
        {
            string getVillain = "SELECT Name FROM Villains WHERE Id = @VillainId";
            using (SqlCommand getVillainCommand = new SqlCommand(getVillain, connection))
            {
                getVillainCommand.Parameters.AddWithValue("@VillainId", villainId);

                if (getVillainCommand.ExecuteScalar() == null)
                {
                    return String.Empty;
                }
                else
                {
                    return (string)getVillainCommand.ExecuteScalar();
                }
            }
        }
    }
}
