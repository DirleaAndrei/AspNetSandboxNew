using AspNetSandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AspNetSanboxTests
{
    public class StartupTetsts
    {
        [Fact]
        public void CheckConversionToEFConnectionString()
        {
            //Assume
            string databaseUrl = "";

            //Act

            string convertedConnectionString = Startup.ConvertConnectionString(databaseUrl);

            //Assert
            Assert.Equal("Database=dcdcalp5i2127v; Host=ec2-54-217-15-9.eu-west-1.compute.amazonaws.com; Port=5432; User Id=afymdxklelzjvw; Password=ca39a0e122e8f66d9a3d76388e803213e7630e9fa7a7ba16b376c285860ed627; SSL Mode=Require;Trust Server Certificate=true", convertedConnectionString);
        }
    }
}
