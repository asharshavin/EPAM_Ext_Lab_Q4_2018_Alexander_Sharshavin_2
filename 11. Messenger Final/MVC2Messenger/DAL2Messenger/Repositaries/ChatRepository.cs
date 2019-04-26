using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using DAL2Messenger.Interfaces;

namespace DAL2Messenger
{
    public class ChatRepository : IChatRepository
    {
        private readonly string connectionString;
        private DbProviderFactory factory;

        public ChatRepository(ConnectionStringSettings connectionStringItem)
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

                command.CommandText = "DeleteChat";
                command.CommandType = CommandType.StoredProcedure;

                var param = command.CreateParameter();
                param.ParameterName = "@ID";
                param.DbType = DbType.Int32;
                param.Value = id;
                command.Parameters.Add(param);

                connection.Open();

                int updatedRows = command.ExecuteNonQuery();

                result = updatedRows != 0;
            }

            return result;
        }

        public Chat Get(int id)
        {
            var entity = new Chat();

            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                var command = connection.CreateCommand();
                command.CommandText = "GetChat";
                command.CommandType = CommandType.StoredProcedure;

                var param = command.CreateParameter();
                param.ParameterName = "@ID";
                param.DbType = DbType.Int32;
                param.Value = id;
                command.Parameters.Add(param);

                connection.Open();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        entity.ChatId = (int)reader["ChatId"];
                        entity.Name = (string)reader["Name"];
                    }
                }
            }

            return entity;
        }

        public List<Chat> GetAll()
        {
            var listOfEntity = new List<Chat>();
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                var command = connection.CreateCommand();
                command.CommandText = "GetAllChats";
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listOfEntity.Add(new Chat() { ChatId = (int)reader["ChatId"], Name = (string)reader["Name"] });
                    }
                }
            }
            return listOfEntity;
        }

        public List<Chat> GetAllChatUser(int UserId)
        {
            var listOfEntity = new List<Chat>();
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                var command = connection.CreateCommand();
                command.CommandText = "GetAllChatUserChats";
                command.CommandType = CommandType.StoredProcedure;

                var param = command.CreateParameter();
                param.ParameterName = "@UserID";
                param.DbType = DbType.Int32;
                param.Value = UserId;
                command.Parameters.Add(param);

                connection.Open();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listOfEntity.Add(new Chat() { ChatId = (int)reader["ChatId"], Name = (string)reader["Name"] });
                    }
                }
            }
            return listOfEntity;
        }

        public bool Save(Chat entity)
        {
            bool result = false;
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                var command = connection.CreateCommand();
                command.CommandText = "SaveChat";
                command.CommandType = CommandType.StoredProcedure;

                var param = command.CreateParameter();
                param.ParameterName = "@ID";
                param.DbType = DbType.Int32;
                param.Value = entity.ChatId;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@Name";
                param.DbType = DbType.String;
                param.Value = entity.Name;
                command.Parameters.Add(param);

                var dateOfCreation = command.CreateParameter();
                dateOfCreation.ParameterName = "@DateOfCreation";
                dateOfCreation.DbType = DbType.DateTime;
                dateOfCreation.Value = DateTime.Now;
                command.Parameters.Add(dateOfCreation);

                connection.Open();

                var id = command.ExecuteScalar();

                entity.ChatId = Convert.ToInt32(id);

                result = id != null;
            }

            return result;
        }
    }
}
