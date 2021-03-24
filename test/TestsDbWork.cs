using System;
using System.Collections.Generic;
using System.Linq;
using dbWork;
using dbWork.Context;
using dbWork.Models;
using dbWork.Services;
using NUnit.Framework;

namespace test
{
    public class TestsDbWork
    {
        private static readonly string Pass = Environment
            .GetEnvironmentVariable("DBPOSTGRESQLPASSWORD", EnvironmentVariableTarget.Machine);
        private static readonly string Login = Environment
            .GetEnvironmentVariable("DBPOSTGRESQLLOGIN", EnvironmentVariableTarget.Machine);
        
        private static readonly string DbConnectString = $"User ID={Login};Password={Pass};Host=localhost;Port=5432;Database=postgres;";
        
        [Test]
        public void SelectScInfoOrgNameTestBySql()
        {
            var query = new RegularQuerySql(DbConnectString);
            var list1 = query.SelectScInfoOrgName();
            
            var check = list1 != null;
            Assert.True(check);
        }
        
        [Test]
        public void SelectScWhereOrgNameTestBySql()
        {
            var query = new RegularQuerySql(DbConnectString);
            var list1 = query.SelectScWhereOrgName("Белорусский государственный университет");
            
            var check = list1 != null;
            Assert.True(check);
        }      
        
        [Test]
        public void SelectScInfoOrgNameTestByLinq()
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
        [Test]
        public void SelectScWhereOrgNameTestByLinq()
        {
            var db = new ScientistContext();

            var query = db.Scientist.Join(db.Organization,
                s => s.Organization_Id,
                o => o.Id,
                (s, o) => new
                {
                    OrganizationName = o.Name,
                    Id = s.Id,
                    LastName = s.LastName,
                    FirstName = s.FirstName,
                    Patronymic = s.Patronymic,
                    Gender = s.Gender,
                    DateBirdth = s.DateBirdth,
                    Age = (DateTime.Now.Month >= s.DateBirdth.Month && DateTime.Now.Day >= s.DateBirdth.Day)
                        ? (DateTime.Now.Year - s.DateBirdth.Year)
                        : (DateTime.Now.Year - s.DateBirdth.Year - 1),
                    AcademicDegree = s.AcademicDegree,
                    Position = s.Position,
                    Address = s.Address,
                    NumberPhone = s.NumberPhone,
                }).Where(o => o.OrganizationName == "Белорусский государственный университет");
            
            var check = query.Any();
            Assert.True(check);
        }
    }
}