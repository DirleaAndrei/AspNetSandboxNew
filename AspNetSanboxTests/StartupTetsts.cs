using AspNetSandbox;
using Xunit;

namespace AspNetSanboxTests
{
    public class StartupTetsts
    {
        [Fact]
        public void CheckConversionToEFConnectionString()
        {
            // Assume
            string databaseUrl = "postgres://xsywdatiakmqrk:df7d9d3a2db7a032685a953a51dbea8f339e3511ade04c81aa1f40f3a3fea7c6@ec2-54-216-48-43.eu-west-1.compute.amazonaws.com:5432/da293i0vr25cig";

            // Act
            string convertedConnectionString = Startup.ConvertConnectionString(databaseUrl);

            // Assert
            Assert.Equal("Database=da293i0vr25cig; Host=ec2-54-216-48-43.eu-west-1.compute.amazonaws.com; Port=5432; User Id=xsywdatiakmqrk; Password=df7d9d3a2db7a032685a953a51dbea8f339e3511ade04c81aa1f40f3a3fea7c6; SSL Mode=Require;Trust Server Certificate=true", convertedConnectionString);
        }
    }
}
