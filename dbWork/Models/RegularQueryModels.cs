using System;

namespace dbWork.Models
{

    public class ScWhereOrgName
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateBirdth { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string AcademicDegree { get; set; }
        public string Position { get; set; }
        public string Address { get; set; }
        public string NumberPhone { get; set; }
        public string OrgName { get; set; }
    }
    public class ScInfoOrgName
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string AcademicDegree { get; set; }
        public string Position { get; set; }
        public string OrgName { get; set; }
    }
    
}