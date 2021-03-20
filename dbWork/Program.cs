using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using Microsoft.Extensions.Configuration;
using dbWork.Context;
using dbWork.Models;
using Npgsql;

namespace dbWork
{
    internal static class Program
    {
        private const string DbConnectString = "User ID=postgres;Password=Andrei080;Host=localhost;Port=5432;Database=postgres;";

        private static void SqlWorking(string dbConnectString)
        {
            var query = new QuerySQL(dbConnectString);
            try
            {
                query.OpenConnection();
                if (!query.CheckTableExists("scientist"))
                {
                    query.ExecuteSqlFile( "..\\..\\..\\resources/script.sql");
                }
                var reader = query.ExecuteReadSqlQuery("SELECT scientist.id, scientist.lastname, scientist.firstname, " +
                                                       "scientist.academicdegree, scientist.position, organization.org_name " +
                                                       "FROM scientist, organization WHERE scientist.organization_id = " +
                                                       "organization.id;");
                var id = new List<int>();
                var lastname = new List<string>();
                var firstname = new List<string>();
                var academicdegree = new List<string>();
                var position = new List<string>();
                var orgname = new List<string>();
                
                while (reader.Read())
                {
                    id.Add((int) reader["Id"]);
                    lastname.Add(reader["lastname"].ToString());
                    firstname.Add(reader["firstname"].ToString());    
                    academicdegree.Add(reader["academicdegree"].ToString());   
                    position.Add(reader["position"].ToString());
                    orgname.Add(reader["org_name"].ToString());    
                    
                }

                for (var i = 0; i < id.Count; i++)
                {
                    Console.WriteLine("{0} {1} {2} {3} {4} {5}", 
                        id[i], lastname[i],firstname[i],academicdegree[i],orgname[i], position[i]);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                query.CloseConnection();
            }
        }

        private static void LinqWorking()
        {
            //Before starting, check the sent id
            var db = new ScientistContext();
            var scientis1 = new Scientist()
            {
                Id = 1,
                LastName = "Исаев",
                FirstName = "Aндрей",  
                Patronymic = "Александрович",
                DateBirdth = new DateTime(08/07/1999),  
                Gender = "man",
                Nationality = "белорус",  
                AcademicDegree = "доктор наук",
                Position = "директор отдела научных исследований",  
                Address = "220000, г.Минск, ул.Грибоедова, 25-21",
                NumberPhone = "+375257705629",  
                Organization_Id = 1
            };
            var scientis2 = new Scientist()
            {
                Id = 2,
                LastName = "Исаев",
                FirstName = "Aндрей",  
                Patronymic = "Александрович",
                DateBirdth = new DateTime(08/07/1999),  
                Gender = "man",
                Nationality = "белорус",  
                AcademicDegree = "доктор наук",
                Position = "директор отдела научных исследований",  
                Address = "220000, г.Минск, ул.Грибоедова, 25-21",
                NumberPhone = "+375257705629",
                Organization_Id = 2
            };
            db.Scientist.AddRange(scientis1, scientis2);
            db.SaveChanges();
            
            var query = new QuerySQL(DbConnectString);
            query.OpenConnection();
            query.ExecuteSqlNonQuery("alter table \"Scientist\" add constraint scientist_organization__fk_2 " + 
            "foreign key (\"Organization_Id\") references \"Organization\" (\"Id\")");
            query.CloseConnection();
                
            var scientists = db.Scientist.ToList();
            Console.WriteLine("Users list:");
            foreach (var s in scientists)
            {
                Console.WriteLine($"{s.Id}.{s.LastName} - {s.FirstName}");
            }
        }
        private static void Main()
        { 
            LinqWorking();
            SqlWorking(DbConnectString);
        }
    }
}