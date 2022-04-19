using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using TableBookingSystem.Application.Repository;
using TableBookingSystem.Persistence.Repository;
using TableBookingSystem.Persistence.Interfaces;
using TableBookingSystem.Domain.Entities.User;
using System.Data;
using Dapper;
using Moq.Dapper;
using System.Data.SqlClient;
using TableBookingSystem.Persistence.Implementation;

namespace TableBookingSystem.Tests.Repository
{
    public class UserRepositoryTests
    {
        [Theory]
        [InlineData("System@TableBookingSystem.com", true)]
        [InlineData("panchalrushang8866@gmail.com", false)]
        public void CheckUserExists_Test(string emailId, bool shouldBeAvailable)
        {
            IUserRepository userRepository = new UserRepository(GetDataProvider());

            var retVal = userRepository.CheckUserExists(emailId);

            Assert.True(retVal == shouldBeAvailable);
        }
        public IDataProvider GetDataProvider()
        {
            IDataProvider dataProvider = new SQLServerDataProvider(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Live Work\TableBookingSystem\TableBookingSystem.Tests\Database\TestDatabase.mdf;Integrated Security=True");

            return dataProvider;
        }
    }
}
