using Dapper;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DapperAdvanceDemoLibrary
{
    public class DapperBasics
    {
        private static void BasicRead()
        {
            using (IDbConnection cnn = new SqlConnection(Tools.GetConnectionString()))
            {
                string sql = "select * from dbo.Person";

                var people = cnn.Query<PersonModel>(sql);

                //foreach (var person in people)
                //{
                //    Console.WriteLine($"{ person.FirstName } { person.LastName }");
                //}
            }
        }

        private static void BasicWithoutModel()
        {
            using (IDbConnection cnn = new SqlConnection(Tools.GetConnectionString()))
            {
                string sql = "select * from dbo.Person";

                var people = cnn.Query(sql);

                //foreach (var person in people)
                //{
                //    Console.WriteLine($"{ person.FirstName } { person.LastName }");
                //}
            }
        }

        private static void ReadOnlyPart()
        {
            using (IDbConnection cnn = new SqlConnection(Tools.GetConnectionString()))
            {
                string sql = "select FirstName from dbo.Person";

                var people = cnn.Query<PersonModel>(sql);

                foreach (var person in people)
                {
                    Console.WriteLine($"{ person.FirstName } { person.LastName }");
                }
            }
        }

        private static void ReadWithParameters(string lastName)
        {
            using (IDbConnection cnn = new SqlConnection(Tools.GetConnectionString()))
            {
                var p = new DynamicParameters();
                p.Add("@LastName", lastName);

                string sql = "select * from dbo.Person where LastName = @LastName";

                var people = cnn.Query<PersonModel>(sql, p);

                foreach (var person in people)
                {
                    Console.WriteLine($"{ person.FirstName } { person.LastName }");
                }
            }
        }

        private static void ReadWithAnonymousParameters(string lastName)
        {
            using (IDbConnection cnn = new SqlConnection(Tools.GetConnectionString()))
            {
                var p = new
                {
                    LastName = lastName
                };

                string sql = "select * from dbo.Person where LastName = @LastName";

                var people = cnn.Query<PersonModel>(sql, p);

                foreach (var person in people)
                {
                    Console.WriteLine($"{ person.FirstName } { person.LastName }");
                }
            }
        }

        private static void ReadWithStoredProcedure(string searchTerm)
        {
            using (IDbConnection cnn = new SqlConnection(Tools.GetConnectionString()))
            {
                var p = new DynamicParameters();
                p.Add("@SearchTerm", searchTerm);

                string sql = "dbo.spPerson_Search";

                var people = cnn.Query<PersonModel>(sql, p,
                    commandType: CommandType.StoredProcedure);

                foreach (var person in people)
                {
                    Console.WriteLine($"{ person.FirstName } { person.LastName }");
                }
            }
        }

        private static void BasicWrite(string firstName, string lastName)
        {
            using (IDbConnection cnn = new SqlConnection(Tools.GetConnectionString()))
            {
                var p = new DynamicParameters();
                p.Add("@FirstName", firstName);
                p.Add("@LastName", lastName);

                string sql = $@"insert into dbo.Person (FirstName, LastName)
                                values (@FirstName, @LastName)";

                cnn.Execute(sql, p);

                ReadWithParameters(lastName);
            }
        }

        private static void GetWriteCount()
        {
            using (IDbConnection cnn = new SqlConnection(Tools.GetConnectionString()))
            {
                string sql = @"update dbo.Person
                               set LastName = UPPER(LastName);";

                var rowsAffected = cnn.Execute(sql);

                Console.WriteLine($"Rows Affected: { rowsAffected }");
            }
        }

        private static void WriteSet(List<PersonModel> people)
        {
            using (IDbConnection cnn = new SqlConnection(Tools.GetConnectionString()))
            {
                string sql = $@"insert into dbo.Person (FirstName, LastName)
                                values (@FirstName, @LastName)";

                cnn.Execute(sql, people);

                BasicRead();
            }
        }

        private static List<PersonModel> GetPeople()
        {
            var output = new List<PersonModel>();

            output.Add(new PersonModel { FirstName = "Mary", LastName = "Kilborn" });
            output.Add(new PersonModel { FirstName = "Wayne", LastName = "Decker" });
            output.Add(new PersonModel { FirstName = "Beth", LastName = "Tasker" });
            output.Add(new PersonModel { FirstName = "Luke", LastName = "Riker" });
            output.Add(new PersonModel { FirstName = "Owen", LastName = "Parker" });

            return output;
        }
    }
}