using InitialSetup;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ChangeTownNamesCasing
{
    class StartUp
    {
        static void Main(string[] args)
        {
            string countryName = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(Configuration.connectionString))
            {
                connection.Open();
                connection.ChangeDatabase("MinionsDB");

                int countryCode = GetCountryCodeByName(connection,countryName);
                if (countryCode != 0)
                {
                    UpdateTownNamesToUpperCasingAndPrint(connection, countryCode);
                }
               
                connection.Close();
            }
        }

        private static int GetCountryCodeByName(SqlConnection connection, string countryName)
        {
            string findIdQuery = "SELECT Id FROM Countries WHERE Name = @Name";
            using (SqlCommand findIdCommand = new SqlCommand(findIdQuery,connection))
            {
                findIdCommand.Parameters.AddWithValue("@Name", countryName);

                if (findIdCommand.ExecuteScalar() == null)
                {
                    Console.WriteLine("No town names were affected.");
                    return 0;
                }
                else
                { 
                    return (int)findIdCommand.ExecuteScalar();
                }
            }
        }

        private static void UpdateTownNamesToUpperCasingAndPrint(SqlConnection connection, int countryCode)
        { 
            string updateQuery = "UPDATE Towns SET Name = UPPER(Name) WHERE CountryCode = @CountryCode";
            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
            {
                updateCommand.Parameters.AddWithValue("@CountryCode", countryCode);

                if (updateCommand.ExecuteNonQuery() == 0)
                {
                    Console.WriteLine("No town names were affected.");
                }
                else
                {
                    int numberOfRowsAffected = updateCommand.ExecuteNonQuery();
                    List<string> townsAffected = new List<string>();
                    string findUpdatedTownsQuery = "SELECT Name FROM Towns WHERE CountryCode = @CountryCode";
                    using (SqlCommand findUpdatedTownsCommand = new SqlCommand(findUpdatedTownsQuery, connection))
                    {
                        findUpdatedTownsCommand.Parameters.AddWithValue("@CountryCode", countryCode);

                        using (SqlDataReader reader = findUpdatedTownsCommand.ExecuteReader())
                        { 
                            while (reader.Read())
                            {
                                townsAffected.Add((string)reader[0]);
                            }
                        }
                    }
                    Console.WriteLine($"{numberOfRowsAffected} town names were affected.");
                    Console.WriteLine($"[{string.Join(", ",townsAffected)}]");
                }
            }
        }
    }
}
