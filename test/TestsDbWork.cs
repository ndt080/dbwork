using System;
using System.Collections.Generic;
using System.Linq;
using dbWork;
using dbWork.Context;
using dbWork.Models;
using NUnit.Framework;

namespace test
{
    public class TestsDbWork
    {
        private const string DbConnectString = "User ID=postgres;Password=Andrei080;Host=localhost;Port=5432;Database=postgres;";
        
        [Test]
        public void CreateAndInsertTableTestBySql()
        {
            var query = new QuerySQL(DbConnectString);
            query.OpenConnection();
            var check = query.ExecuteSqlFile("..\\..\\..\\..\\dbWork/resources/script.sql");
            if (!check)
            {
                check = query.CheckTableExists("scientist");
            }
            Assert.True(check);

            query.CloseConnection();
        }
        
        [Test]
        public void SelectDataTestBySql()
        {
            var query = new QuerySQL(DbConnectString);
            query.OpenConnection();
            
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

            var check = id.Count != 0;
            Assert.True(check);
            query.CloseConnection();
        }
        
        [Test]
        public void CreateAndInsertTableTestByLinq()
        {
            //Before starting, check the sent id
            var db = new ScientistContext();
            try
            {
                var org1 = new Organization()
                {
                    Id = 1,
                    Name = "Академия наук Республики Беларусь"
                };
                var org2 = new Organization()
                {
                    Id = 2,
                    Name = "Белорусский государственный университет"
                };
                db.Organization.AddRange(org1, org2);
                db.SaveChanges();

                var scientis1 = new Scientist()
                {
                    Id = 1,
                    LastName = "Исаев",
                    FirstName = "Aндрей",
                    Patronymic = "Александрович",
                    DateBirdth = new DateTime(08 / 07 / 1999),
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
                    DateBirdth = new DateTime(08 / 07 / 1999),
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
                query.ExecuteSqlNonQuery("alter table \"Scientist\" add constraint scientist_organization__fk_2 foreign key (\"Organization_Id\") references \"Organization\" (\"Id\")");
                query.CloseConnection();

                Assert.True(true);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Test]
        public void SelectDataTestByLinq()
        {
            var db = new ScientistContext();
            
            var query = db.Scientist.Join(db.Organization,
                s => s.Organization_Id,
                o => o.Id,
                (s, o) => new
                {
                    Id = s.Id,
                    LastName = s.LastName,
                    FirstName = s.FirstName,
                    AcademicDegree = s.AcademicDegree,
                    Position = s.Position,
                    OrganizationName = o.Name,
                });
            
            var check = query.Any();
            Assert.True(check);
        }
    }
}