using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Configuration;

namespace DAL2Messenger
{
    public class UserRepository : IUserRepository
    {
        private readonly string connectionString;
        private DbProviderFactory factory;

        public UserRepository(ConnectionStringSettings connectionStringItem)
        {
            if (connectionStringItem == null)
            {
                return;
            }

            connectionString = connectionStringItem.ConnectionString;
            var providerName = connectionStringItem.ProviderName;
            factory = DbProviderFactories.GetFactory(providerName);
        }

        public bool Delete(int id)
        {
            bool result = false;
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                var command = connection.CreateCommand();
                command.CommandText =
                    @"DELETE FROM dbo.[Users] WHERE UserID =  @UserID ";

                var userID = command.CreateParameter();
                userID.ParameterName = "@UserID";
                userID.DbType = DbType.Int32;
                userID.Value = id;
                command.Parameters.Add(userID);

                connection.Open();

                int updatedRows = command.ExecuteNonQuery();

                result = updatedRows != 0;
            }

            return result;
        }

        public User Get(int id)
        {
            var user = new User();

            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                var command = connection.CreateCommand();
                command.CommandText = "SELECT TOP 1 [UserID], [Username], [DateOfCreation], Role FROM [Users] WHERE UserID = @UserID";

                var userID = command.CreateParameter();
                userID.ParameterName = "@UserID";
                userID.DbType = DbType.Int32;
                userID.Value = id;
                command.Parameters.Add(userID);

                connection.Open();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user.id = (int)reader["UserID"];
                        user.name = (string)reader["Username"];
                        user.dateOfCreation = (DateTime)reader["DateOfCreation"];
                        user.role = (int)reader["Role"];
                    }
                }
            }

            return user;
        }

        public List<User> GetAll()
        {
            var listOfUser = new List<User>();
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                var command = connection.CreateCommand();
                command.CommandText = "SELECT [UserID], [Username], [DateOfCreation], [Role] FROM [Users]";

                connection.Open();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listOfUser.Add(new User() { id = (int)reader["UserID"], name = (string)reader["Username"],
                            dateOfCreation = (DateTime)reader["DateOfCreation"], role = (int)reader["Role"]
                        });
                    }
                }
            }

            return listOfUser;
        }

        public List<User> GetAllUsers(int top)
        {
            /*
            хранимка создана в sms
            CREATE PROCEDURE dbo.GetAllUsers
	            @Top int
            AS
            BEGIN
	            SET NOCOUNT ON;
	            SELECT TOP(@Top) [UserID], [Username], [DateOfCreation], [Role] FROM [Users]
            END
            GO
            */

            var listOfUser = new List<User>();
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                var command = connection.CreateCommand();
                command.CommandText = "GetAllUsers";
                command.CommandType = CommandType.StoredProcedure;

                var param = command.CreateParameter();
                param.ParameterName = "@Top";
                param.DbType = DbType.Int32;
                param.Value = top;
                command.Parameters.Add(param);

                connection.Open();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listOfUser.Add(new User()
                        {
                            id = (int)reader["UserID"],
                            name = (string)reader["Username"],
                            dateOfCreation = (DateTime)reader["DateOfCreation"],
                            role = (int)reader["Role"]
                        });
                    }
                }
            }

            return listOfUser;
        }

        public bool Save(User entity)
        {
            bool result = false;
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                var command = connection.CreateCommand();
                command.CommandText =
                    @"BEGIN TRAN
	                    UPDATE Users
	                    SET [Username] = @Username, Role = @Role  
	                    WHERE UserID =  @UserID 

                       IF @@rowcount = 0
                       BEGIN
                          INSERT [Users] ([UserID], [Username], [DateOfCreation], [Role]) VALUES (@UserID, @Username, @DateOfCreation, @Role)
                       END
                    COMMIT TRAN";

                var userID = command.CreateParameter();
                userID.ParameterName = "@UserID";
                userID.DbType = DbType.Int32;
                userID.Value = entity.id;
                command.Parameters.Add(userID);

                var username = command.CreateParameter();
                username.ParameterName = "@Username";
                username.DbType = DbType.String;
                username.Value = entity.name;
                command.Parameters.Add(username);

                var dateOfCreation = command.CreateParameter();
                dateOfCreation.ParameterName = "@DateOfCreation";
                dateOfCreation.DbType = DbType.String;
                dateOfCreation.Value = DateTime.Now;
                command.Parameters.Add(dateOfCreation);

                var role = command.CreateParameter();
                role.ParameterName = "@Role";
                role.DbType = DbType.Int32;
                role.Value = 1;
                command.Parameters.Add(role);

                connection.Open();

                int updatedRows = command.ExecuteNonQuery();

                result = updatedRows != 0;
            }

            return result;
        }
    }
}
