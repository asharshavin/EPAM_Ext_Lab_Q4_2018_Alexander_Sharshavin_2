using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Configuration;
using DAL2Messenger.Interfaces;

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

                command.CommandText = "DeleteUser";
                command.CommandType = CommandType.StoredProcedure;

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
                command.CommandText = "GetUser";
                command.CommandType = CommandType.StoredProcedure;

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
                        user.password = (string)reader["Password"];
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
                command.CommandText = "GetAll";
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listOfUser.Add(new User() { id = (int)reader["UserID"], name = (string)reader["Username"],
                            dateOfCreation = (DateTime)reader["DateOfCreation"], role = (int)reader["Role"],
                            password = (string)reader["Password"]
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
                            role = (int)reader["Role"],
                            password = (string)reader["Password"]
                    });
                    }
                }
            }

            return listOfUser;
        }

        public bool Save(User entity)
        {
            bool result = false;

            if (entity.name == null || entity.name == "") throw new Exception("Не заполнено имя пользователя!");

            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                var command = connection.CreateCommand();
                command.CommandText = "SaveUser";
                command.CommandType = CommandType.StoredProcedure;

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
                dateOfCreation.DbType = DbType.DateTime;
                dateOfCreation.Value = DateTime.Now;
                command.Parameters.Add(dateOfCreation);

                var role = command.CreateParameter();
                role.ParameterName = "@Role";
                role.DbType = DbType.Int32;
                role.Value = entity.role;
                command.Parameters.Add(role);

                var pass = command.CreateParameter();
                pass.ParameterName = "@Password";
                pass.DbType = DbType.Int32;
                pass.Value = entity.password;
                command.Parameters.Add(pass);

                connection.Open();

                var id = command.ExecuteScalar();

                entity.id = Convert.ToInt32(id);

                result = id != null;
            }

            return result;
        }
    }
}
