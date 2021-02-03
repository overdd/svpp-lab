using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace SVPP_Lab_8
{
    class Railway
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TrainQuantity { get; set; }
        public int RailwayLength { get; set; }
        public int StaffNumber { get; set; }

        static SqlConnection connection;
        public Railway()
        {
            var connString = ConfigurationManager.ConnectionStrings["DemoConnection"].ConnectionString;
            connection = new SqlConnection(connString);
        }

        public override string ToString()
        {
            return String.Format("id: {0}, name: {1}, trains: {2}, railways length: {3}, employees: {4}",
           Id, Name, TrainQuantity, RailwayLength, StaffNumber);
        }
        public static IEnumerable<Railway> GetAllRailways()
        {
            var commandString = "SELECT Id, Name, TrainQuantity, RailwayLength, StaffNumber FROM [Table]";
            SqlCommand getAllCommand = new SqlCommand(commandString, connection);
            connection.Open();
            var reader = getAllCommand.ExecuteReader(); if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var name = reader.GetSqlString(1);
                    var trainQuantity = reader.GetInt32(2);
                    var railwayLength = reader.GetInt32(3);
                    var staffNumber = reader.GetInt32(4);
                    var railway = new Railway
                    {
                        Id = id,
                        Name = name.IsNull == true ? "No name" : name.Value,
                        TrainQuantity = trainQuantity,
                        RailwayLength = railwayLength,
                        StaffNumber = staffNumber
                    };
                    yield return railway;
                }
            };
            connection.Close();
        }

        public void Insert()
        {
            var commandString = "INSERT INTO [Table] (Name, TrainQuantity,  RailwayLength, StaffNumber)" + "VALUES (@name, @trainQuantity, @railwayLength, @staffNumber)"; SqlCommand insertCommand = new SqlCommand(commandString, connection);
            insertCommand.Parameters.AddRange(new SqlParameter[]
            {
                    new SqlParameter("name", Name),
                    new SqlParameter("trainQuantity", TrainQuantity),
                    new SqlParameter("railwayLength", RailwayLength),
                    new SqlParameter("staffNumber", StaffNumber)
            });
            connection.Open();
            insertCommand.ExecuteNonQuery();
            connection.Close();
        }

        public static Railway GetRailway(int id)
        {
            foreach (var rw in GetAllRailways())
            {
                if (rw.Id == id)
                    return rw;
            }
            return null;
        }

        public void Update()
        {
            var commandString = "UPDATE [Table] SET Name=@name,  TrainQuantity=@trainQuantity, RailwayLength=@railwayLength, StaffNumber=@staffNumber WHERE(Id=@id)"; SqlCommand updateCommand = new SqlCommand(commandString, connection);
            updateCommand.Parameters.AddRange(new SqlParameter[] {
                new SqlParameter("name", Name),
                new SqlParameter("trainQuantity", TrainQuantity),
                new SqlParameter("railwayLength", RailwayLength),
                new SqlParameter("staffNumber", StaffNumber),
                new SqlParameter("Id", Id),  });
            connection.Open();
            updateCommand.ExecuteNonQuery();
            connection.Close();
        }

        public static void Delete(int id)
        {
            var commandString = "DELETE FROM [Table]  WHERE(Id=@id)";
            SqlCommand deleteCommand = new SqlCommand(commandString, connection);
            deleteCommand.Parameters.AddWithValue("id", id);
            connection.Open();
            deleteCommand.ExecuteNonQuery();
            connection.Close();
        }
    }
}
