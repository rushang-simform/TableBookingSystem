using TableBookingSystem.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableBookingSystem.Domain.Exceptions;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace TableBookingSystem.Persistence.Implementation
{
    public class MySQLDataProvider : IDataProvider
    {
        private readonly string _connectionString = string.Empty;
        public MySQLDataProvider(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IDbConnection Connection
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    throw new InvalidConnectionStringException();
                }
                return new MySqlConnection(_connectionString);
            }
        }
    }
}
