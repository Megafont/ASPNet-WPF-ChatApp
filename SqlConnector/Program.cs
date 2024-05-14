using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SqlConnector
{
    /// <summary>
    /// This project is a simple test app that acccesses the database called Test, which we created in video 2 of the MVC playlist:
    /// https://www.youtube.com/watch?v=EI30Zl40IhQ&list=PLrW43fNmjaQUBZv0OiliNY4fStb4Vj1u4&index=3
    /// 
    /// This project demonstrates how to access and manipulate a database in C# with an <see cref="SqlConnection"/>.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var sqlConnection = new SqlConnection("Server=.; Database=Test; User Id=TestUser; Password=TestUser"))
            {
                sqlConnection.Open();


                using (var command = new SqlCommand($"INSERT INTO dbo.Users (Id, Username, FirstName, LastName, IsEnabled, CreatedDateUtc) VALUES ('{Guid.NewGuid().ToString("N")}', 'Username1', 'FirstName1', 'LastName1', 1, '2024-05-14 14:42:00.0000000 -06:00')", sqlConnection))
                {
                    var result = command.ExecuteNonQuery();

                }


                using (var command = new SqlCommand("SELECT * FROM Users", sqlConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Username {reader["Username"]}, First Name: {reader["FirstName"]}, LastName: {reader["LastName"]}, IsEnabled? {reader["IsEnabled"]}");
                        }
                    }

                }

                sqlConnection.Close();
            }

            Console.WriteLine();
            Console.WriteLine("Press Enter to close...");
            Console.Read();
        }
    }
}
