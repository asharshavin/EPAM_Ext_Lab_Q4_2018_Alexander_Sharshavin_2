using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using DAL2Messenger.Interfaces;

namespace DAL2Messenger
{
    public class RoleRepository : IRoleRepository
    {
        private readonly string connectionString;
        private DbProviderFactory factory;

        public RoleRepository(ConnectionStringSettings connectionStringItem)
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

                command.CommandText = "DeleteRole";
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

        public Role Get(int id)
        {
            var entity = new Role();

            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                var command = connection.CreateCommand();
                command.CommandText = "GetRole";
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
                        entity.id = (int)reader["RoleId"];
                        entity.name = (string)reader["Name"];
                    }
                }
            }

            return entity;
        }

        public List<Role> GetAll()
        {
            var listOfEntity = new List<Role>();
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                var command = connection.CreateCommand();
                command.CommandText = "GetAllRoles";
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listOfEntity.Add(new Role() { id = (int)reader["RoleId"], name = (string)reader["Name"] });
                    }
                }
            }
            return listOfEntity;
        }

        public bool Save(Role entity)
        {
            bool result = false;
            using (IDbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                var command = connection.CreateCommand();
                command.CommandText = "SaveRole";
                command.CommandType = CommandType.StoredProcedure;

                var param = command.CreateParameter();
                param.ParameterName = "@ID";
                param.DbType = DbType.Int32;
                param.Value = entity.id;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@Name";
                param.DbType = DbType.String;
                param.Value = entity.name;
                command.Parameters.Add(param);

                connection.Open();

                var id = command.ExecuteScalar();

                entity.id = Convert.ToInt32(id);

                result = id != null;
            }

            return result;
        }
    }
}
