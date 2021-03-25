using System;
using System.Collections.Generic;
using dbWork.Models;

namespace dbWork.Repository
{
    public static class RegularRepositoryExtension
    {
        public static List<ScInfoAndOrgName> GetInfoAndOrgName(this RegularRepository rq)
        {
            rq.OpenConnection();
            var rd = rq.ExecReadSqlQuery("" +
                                         "SELECT \"Scientist\".\"Id\", \"Scientist\".\"LastName\"," +
                                         "\"Scientist\".\"FirstName\", \"Scientist\".\"AcademicDegree\", " +
                                         "\"Scientist\".\"Position\", \"Organization\".\"Name\" " +
                                         "FROM \"Scientist\", \"Organization\" " +
                                         "WHERE \"Scientist\".\"Organization_Id\" = \"Organization\".\"Id\";");
            var list = new List<ScInfoAndOrgName>();
            while (rd.Read())
            {
                list.Add(new ScInfoAndOrgName()
                {
                    Id = (int) rd["Id"],
                    LastName = rd["LastName"].ToString(),
                    FirstName = rd["FirstName"].ToString(),
                    AcademicDegree = rd["AcademicDegree"].ToString(),
                    Position = rd["Position"].ToString(),
                    OrgName = rd["Name"].ToString(),
                });
               
            }
            rq.CloseConnection();
            return list;
        }
        public static List<ScInfoByOrgName> GetInfoByOrgName(this RegularRepository rq, string orgname)
        {
            rq.OpenConnection();
            var rd = rq.ExecReadSqlQuery($"" + 
                $"SELECT \"Organization\".\"Name\", \"Scientist\".\"Id\", \"Scientist\".\"LastName\", " +
                $"\"Scientist\".\"FirstName\", \"Scientist\".\"Patronymic\", \"Scientist\".\"Gender\", " +
                $"\"Scientist\".\"DateBirdth\", " +
                $"CASE WHEN date_part('month',current_date) >= date_part('month',\"Scientist\".\"DateBirdth\") " +
                $"AND date_part('day',current_date) >= date_part('day',\"Scientist\".\"DateBirdth\") " +
                $"THEN date_part('year',current_date) - date_part('year',\"Scientist\".\"DateBirdth\") " +
                $"ELSE (date_part('year',current_date) - date_part('year',\"Scientist\".\"DateBirdth\") - 1) " +
                $"END  AS Age, " +
                $"\"Scientist\".\"AcademicDegree\", \"Scientist\".\"Position\", \"Scientist\".\"Address\", " +
                $"\"Scientist\".\"NumberPhone\" " +
                $"FROM \"Scientist\" " +
                $"INNER JOIN \"Organization\" ON \"Organization\".\"Id\" = \"Scientist\".\"Organization_Id\" " +
                $"WHERE \"Organization\".\"Name\" = '{orgname}'");
            
            var list = new List<ScInfoByOrgName>();
            while (rd.Read())
            {
                list.Add(new ScInfoByOrgName()
                {
                    Id = (int) rd["Id"],
                    LastName = rd["LastName"].ToString(),
                    FirstName = rd["FirstName"].ToString(),
                    Patronymic = rd["Patronymic"].ToString(),
                    DateBirdth = (DateTime) rd["DateBirdth"],
                    Age = (int) Math.Floor((double)rd["Age"]),
                    Gender = rd["Gender"].ToString(),
                    AcademicDegree = rd["Academicdegree"].ToString(),
                    Address = rd["Address"].ToString(),
                    NumberPhone = rd["NumberPhone"].ToString(),
                    Position = rd["Position"].ToString(),
                    OrgName = rd["Name"].ToString(),
                });
            }
            rq.CloseConnection();
            return list;
        }       
        
        
    }
}