using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Dapper;

namespace Speakr.TalksApi.DataAccess.DbAccess
{
    public class Dapper : IDapper
    {
        private IConfiguration _configuration;
        private string _connectionString;

        public Dapper(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration["DbConnectionString"];
        }

        private MySqlConnection CreateOpenDbConnection()
        {
            var dbConnection = new MySqlConnection(_connectionString);
            dbConnection.Open();

            return dbConnection;
        }

        public IEnumerable<T> Query<T>(string sql)
        {
            try
            {
                using (var conn = CreateOpenDbConnection())
                {
                    return conn.Query<T>(sql);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<T> Query<T>(string sql, dynamic parameters)
        {
            try
            {
                using (var conn = CreateOpenDbConnection())
                {
                    return conn.Query<T>(sql, parameters as object);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
