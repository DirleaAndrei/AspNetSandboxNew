using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AspNetSanboxTests
{
    public class FileOperationWithBooksTest
    {
        [Fact]
        public void EumerateFilesTest()
        {
            var path = ".";
            System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(path);
            var files = directoryInfo.EnumerateFiles();
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
        }

        [Fact]
        public void CreateFileTest()
        {
            File.WriteAllText("newSettings.json", @"{
  ""ConnectionStrings"": {
    ""DefaultConnection"": ""HOST=localhost;DB=AspNetSandboxTest;UID=postgres;PWD=55b7b7aeb010e143c4498f30ad357abff6fa73ce8f8bdb775468ed4beeb1169e;PORT=5432;"",
    ""Heroku"": ""Database=da293i0vr25cig; Host=ec2-54-216-48-43.eu-west-1.compute.amazonaws.com; Port=5432; User Id=xsywdatiakmqrk; Password=df7d9d3a2db7a032685a953a51dbea8f339e3511ade04c81aa1f40f3a3fea7c6; SSL Mode=Require;Trust Server Certificate=true""
    //""PostgresLocalConnection"": ""HOST=localhost;DB=postgres;UID=postgres;PWD=C9hL3MyfeyLDEtdWo8g5;PORT=5432;"",
    //""SqlConnection"": ""Server=(localdb)\\mssqllocaldb;Database=aspnet-AspNetSandbox-8EC476EF-59F5-486A-828D-638648E9948B;Trusted_Connection=True;MultipleActiveResultSets=true""
    //
        },
  ""Logging"": {
    ""LogLevel"": {
      ""Default"": ""Information"",
      ""Microsoft"": ""Warning"",
      ""Microsoft.Hosting.Lifetime"": ""Information""
    }
  },
  ""AllowedHosts"": ""*""
}
");
        }

        [Fact]
        public void ReadFilesTest()
        {
            using (var fs = File.OpenRead("newSettings.json"))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);

                while (fs.Read(b, 0, b.Length) > 0)
                {
                    var read = temp.GetString(b);
                    Console.WriteLine(read);
                }
            }
        }
    }
}
