using System;
using System.Collections.Generic;
using System.Linq;
using dbWork.Context;
using dbWork.Models;
using Microsoft.EntityFrameworkCore;

namespace dbWork.Repository
{
    public static class OrmRepository
    {
        public static IEnumerable<ScInfoAndOrgName> GetInfoAndOrgName()
        {
            var db = new BaseContext();
            return db.Scientist.Join(db.Organization,
                s => s.Organization_Id,
                o => o.Id,
                (s, o) => new ScInfoAndOrgName
                {
                    Id = s.Id,
                    LastName = s.LastName,
                    FirstName = s.FirstName,
                    AcademicDegree = s.AcademicDegree,
                    Position = s.Position,
                    OrgName = o.Name,
                }).ToList();
        }
        
        public static IEnumerable<ScInfoByOrgName> GetInfoByOrgName( string orgName)
        {
            var db = new BaseContext();
            return db.Scientist.Join(db.Organization,
                s => s.Organization_Id,
                o => o.Id,
                (s, o) => new ScInfoByOrgName
                {
                    OrgName = o.Name,
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
                }).Where(o => o.OrgName == orgName);
        }
    }
}