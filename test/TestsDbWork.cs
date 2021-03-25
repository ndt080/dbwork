using System;
using System.Collections.Generic;
using System.Linq;
using dbWork;
using dbWork.Context;
using dbWork.Models;
using dbWork.Repository;
using Microsoft.EntityFrameworkCore;
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
        public void GetInfoAndOrgNameTest1()
        {
            var query = new RegularRepository(DbConnectString);
            var list1 = query.GetInfoAndOrgName();
            
            var check = list1 != null;
            Assert.True(check);
        }
        
        [Test]
        public void GetInfoByOrgNameTest1()
        {
            var query = new RegularRepository(DbConnectString);
            var list1 = query.GetInfoByOrgName("Белорусский государственный университет");
            
            var check = list1 != null;
            Assert.True(check);
        }      
        
        [Test]
        public void GetInfoAndOrgNameTest2()
        {
            var query = OrmRepository.GetInfoAndOrgName();
            var check = query.Any();
            Assert.True(check);
        }
        [Test]
        public void GetInfoByOrgNameTest2()
        {
            var query = OrmRepository.GetInfoByOrgName("Белорусский государственный университет");
            var check = query.Any();
            Assert.True(check);
        }
    }
}