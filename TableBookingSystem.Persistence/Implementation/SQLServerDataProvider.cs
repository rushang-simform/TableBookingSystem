using TableBookingSystem.Domain.Exceptions;
using TableBookingSystem.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableBookingSystem.Persistence.Implementation
{
    public class SQLServerDataProvider : IDataProvider
    {
        private readonly string _connectionString = string.Empty;
        public SQLServerDataProvider(string connectionString)
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
                return new SqlConnection(_connectionString);
            }
        }
    }
}
