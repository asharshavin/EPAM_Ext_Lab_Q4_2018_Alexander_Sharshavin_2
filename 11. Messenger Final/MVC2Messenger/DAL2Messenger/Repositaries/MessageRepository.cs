using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using DAL2Messenger.Interfaces;

namespace DAL2Messenger
{
    public class MessageRepository : IMessageRepository
    {
        private readonly string connectionString;
        private DbProviderFactory factory;

        public MessageRepository(ConnectionStringSettings connectionStringItem)
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

                command.CommandText = "DeleteMessage";
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

        public Message Get(int id)
        {
            var entity = new Message();

            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                var command = connection.CreateCommand();
                command.CommandText = "GetMessage";
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
                        entity.MsgId = (int)reader["MessageId"];
                        entity.Text = (string)reader["Text"];
                        entity.UserId = (int)reader["UserId"];
                        entity.ChatId = (int)reader["ChatId"];
                        entity.Date = (DateTime)reader["Date"];
                    }
                }
            }

            return entity;
        }

        public List<Message> GetChatMessages(int ChatId, int top)
        {
            var listOfEntity = new List<Message>();
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                var command = connection.CreateCommand();
                command.CommandText = "GetChatMessages";
                command.CommandType = CommandType.StoredProcedure;

                var param = command.CreateParameter();
                param.ParameterName = "@ChatID";
                param.DbType = DbType.Int32;
                param.Value = ChatId;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@top";
                param.DbType = DbType.Int32;
                param.Value = top;
                command.Parameters.Add(param);

                connection.Open();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listOfEntity.Add(new Message()
                        {
                            MsgId = (int)reader["MessageId"],
                            Text = (string)reader["Text"],
                            UserId = (int)reader["UserId"],
                            ChatId = (int)reader["ChatId"],
                            Date = (DateTime)reader["Date"],
                        });
                    }
                }
            }
            return listOfEntity;
        }


        public List<Message> GetAll()
        {
            var listOfEntity = new List<Message>();
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                var command = connection.CreateCommand();
                command.CommandText = "GetAllMessages";
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listOfEntity.Add(new Message()
                        {
                            MsgId = (int)reader["MessageId"],
                            Text = (string)reader["Text"],
                            UserId = (int)reader["UserId"],
                            ChatId = (int)reader["ChatId"],
                        });
                    }
                }
            }
            return listOfEntity;
        }

        public bool Save(Message entity)
        {
            bool result = false;
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                var command = connection.CreateCommand();
                command.CommandText = "SaveMessage";
                command.CommandType = CommandType.StoredProcedure;

                var param = command.CreateParameter();
                param.ParameterName = "@ID";
                param.DbType = DbType.Int32;
                param.Value = entity.MsgId;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@Text";
                param.DbType = DbType.String;
                param.Value = entity.Text;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@ChatID";
                param.DbType = DbType.Int32;
                param.Value = entity.ChatId;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@UserID";
                param.DbType = DbType.Int32;
                param.Value = entity.UserId;
                command.Parameters.Add(param);

                var dateOfCreation = command.CreateParameter();
                dateOfCreation.ParameterName = "@Date";
                dateOfCreation.DbType = DbType.DateTime;
                dateOfCreation.Value = DateTime.Now;
                command.Parameters.Add(dateOfCreation);

                connection.Open();

                var id = command.ExecuteScalar();

                entity.MsgId = Convert.ToInt32(id);

                result = id != null;
            }

            return result;
        }
    }
}
