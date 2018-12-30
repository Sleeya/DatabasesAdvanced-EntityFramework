using InitialSetup;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace AddMinion
{
    class StartUp
    {
        static void Main(string[] args)
        {
            string[] minionInformation = Console.ReadLine().Split().ToArray();
            string[] villainInformation = Console.ReadLine().Split().ToArray();

            string minionName = minionInformation[1];
            int minionAge = int.Parse(minionInformation[2]);
            string minionTown = minionInformation[3];

            string villainName = villainInformation[1];

            using (SqlConnection connection = new SqlConnection(Configuration.connectionString))
            {
                connection.Open();
                connection.ChangeDatabase("MinionsDB");

                int townId = AddTownIfNotFoundAndReturnId(connection, minionTown);
                int villainId = AddVillainIfNotFoundAndReturnId(connection, villainName);
                int minionId = AddMinionAndReturnId(connection, minionName, minionAge, townId);

                SetMinionAsServantOfVillain(connection, minionId, villainId,minionName,villainName);

                connection.Close();
            }
        }

        private static int AddTownIfNotFoundAndReturnId(SqlConnection connection, string minionTown)
        {
            string checkTownExists = "SELECT Id FROM Towns WHERE Name = @MinionTown";

            using (SqlCommand checkCommand = new SqlCommand(checkTownExists, connection))
            {
                checkCommand.Parameters.AddWithValue("@MinionTown", minionTown);

                if (checkCommand.ExecuteScalar() == null)
                {
                    string insertTownQuery = "INSERT INTO Towns(Name) VALUES(@Name)";
                    using (SqlCommand insertCommand = new SqlCommand(insertTownQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@Name", minionTown);
                        insertCommand.ExecuteNonQuery();
                        Console.WriteLine($"Town {minionTown} was added to the database.");
                    }
                }
                return (int)checkCommand.ExecuteScalar();
            }
        }

        private static int AddVillainIfNotFoundAndReturnId(SqlConnection connection, string villainName)
        {
            string getVillainId = "SELECT Id FROM Villains WHERE Name = @Name";

            using (SqlCommand getIdCommand = new SqlCommand(getVillainId, connection))
            {
                getIdCommand.Parameters.AddWithValue("@Name", villainName);

                if (getIdCommand.ExecuteScalar() == null)
                {
                    int evilnessFactorId;
                    string getEvilEvilnessFactorId = "SELECT Id FROM EvilnessFactors WHERE Name = @Name";
                    using (SqlCommand getEvilIdComamnd = new SqlCommand(getEvilEvilnessFactorId, connection))
                    {
                        getEvilIdComamnd.Parameters.AddWithValue("@Name", "Evil");
                        evilnessFactorId = (int)getEvilIdComamnd.ExecuteScalar();
                    }


                    string insertVillain = "INSERT INTO Villains(Name,EvilnessFactorId) VALUES(@Name,@EvilnessFactorId)";
                    using (SqlCommand insertVillainCommand = new SqlCommand(insertVillain, connection))
                    {
                        insertVillainCommand.Parameters.AddWithValue("@Name", villainName);
                        insertVillainCommand.Parameters.AddWithValue(@"EvilnessFactorId", evilnessFactorId);

                        insertVillainCommand.ExecuteNonQuery();
                        Console.WriteLine($"Villain {villainName} was added to the database");
                    }
                }

                return (int)getIdCommand.ExecuteScalar();
            }
        }

        private static int AddMinionAndReturnId(SqlConnection connection, string minionName, int minionAge, int minionTownId)
        {
            string insertMinionQuery = "INSERT INTO Minions(Name,Age,TownId) VALUES(@Name,@Age,@TownId)";
            using (SqlCommand insertMinionCommand = new SqlCommand(insertMinionQuery, connection))
            {
                insertMinionCommand.Parameters.AddWithValue("@Name", minionName);
                insertMinionCommand.Parameters.AddWithValue("@Age", minionAge);
                insertMinionCommand.Parameters.AddWithValue("@TownId", minionTownId);

                insertMinionCommand.ExecuteNonQuery();
            }

            string getMinionIdQuery = "Select Id FROM Minions WHERE Name = @Name";
            using (SqlCommand getMinionIdCommand = new SqlCommand(getMinionIdQuery, connection))
            {
                getMinionIdCommand.Parameters.AddWithValue("@Name", minionName);
                
                return (int)getMinionIdCommand.ExecuteScalar();
            }
        }

        private static void SetMinionAsServantOfVillain(SqlConnection connection, int minionId, int villainId, string minionName, string villainName)
        {
            string insertMinionsVillainQuery = "INSERT INTO MinionsVillains(MinionId,VillainId) VALUES(@MinionId,@VillainId)";

            using (SqlCommand insertCommand = new SqlCommand(insertMinionsVillainQuery, connection))
            {
                insertCommand.Parameters.AddWithValue("@MinionId", minionId);
                insertCommand.Parameters.AddWithValue("@VillainId", villainId);

                insertCommand.ExecuteNonQuery();
                Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
            }
        }
    }
}
