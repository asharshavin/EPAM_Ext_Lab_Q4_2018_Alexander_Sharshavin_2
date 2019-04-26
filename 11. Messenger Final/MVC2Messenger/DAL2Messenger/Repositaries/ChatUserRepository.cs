using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using DAL2Messenger.Interfaces;

namespace DAL2Messenger
{
    public class ChatUserRepository : IChatUserRepository
    {
        private readonly string connectionString;
        private DbProviderFactory factory;

        public ChatUserRepository(ConnectionStringSettings connectionStringItem)
        {
            if (connectionStringItem == null)
            {
                return;
            }

            connectionString = connectionStringItem.ConnectionString;
            var providerName = connectionStringItem.ProviderName;
            factory = DbProviderFactories.GetFactory(providerName);
        }
        public bool Delete(int ChatId, int UserId)
        {
            bool result = false;
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                var command = connection.CreateCommand();

                command.CommandText = "DeleteChatUser";
                command.CommandType = CommandType.StoredProcedure;

                var param = command.CreateParameter();
                param.ParameterName = "@ChatID";
                param.DbType = DbType.Int32;
                param.Value = ChatId;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@UserID";
                param.DbType = DbType.Int32;
                param.Value = UserId;
                command.Parameters.Add(param);

                connection.Open();

                int updatedRows = command.ExecuteNonQuery();

                result = updatedRows != 0;
            }

            return result;
        }

        public List<ChatUser> GetChatUsers(int ChatId)
        {
            var listOfEntity = new List<ChatUser>();
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                var command = connection.CreateCommand();
                command.CommandText = "GetAllChatUsers";
                command.CommandType = CommandType.StoredProcedure;

                var param = command.CreateParameter();
                param.ParameterName = "@ChatID";
                param.DbType = DbType.Int32;
                param.Value = ChatId;
                command.Parameters.Add(param);

                connection.Open();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listOfEntity.Add(new ChatUser()
                        {
                            UserId = (int)reader["UserId"],
                            ChatId = (int)reader["ChatId"],
                        });
                    }
                }
                return listOfEntity;
            }
        }

        public bool Save(ChatUser entity)
        {
            bool result = false;
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                var command = connection.CreateCommand();
                command.CommandText = "SaveChatUser";
                command.CommandType = CommandType.StoredProcedure;

                var param = command.CreateParameter();
                param.ParameterName = "@ChatID";
                param.DbType = DbType.Int32;
                param.Value = entity.ChatId;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@UserID";
                param.DbType = DbType.Int32;
                param.Value = entity.UserId;
                command.Parameters.Add(param);

                connection.Open();

                var id = command.ExecuteScalar();

                result = id != null;
            }

            return result;
        }

        public ChatUser Get(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        ChatUser IBaseRepository<ChatUser>.Get(int id)
        {
            throw new NotImplementedException();
        }

        List<ChatUser> IBaseRepository<ChatUser>.GetAll()
        {
            throw new NotImplementedException();
        }

    }
}
