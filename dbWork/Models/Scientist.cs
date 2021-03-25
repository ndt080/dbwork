using System;
using System.Data.Linq.Mapping;

namespace dbWork.Models
{
    [Table(Name = "Scientist")]
    public class Scientist
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        
        [Column(Name = "LastName")]
        public string LastName { get; set; }
                
        [Column(Name = "FirstName")]
        public string FirstName { get; set; }
                
        [Column(Name = "Patronymic")]
        public string Patronymic { get; set; }
                
        [Column(Name = "DateBirdth")]
        public DateTime DateBirdth { get; set; }
                
        [Column(Name = "Gender")]
        public string Gender { get; set; }
                
        [Column(Name = "Nationality")]
        public string Nationality { get; set; }
                
        [Column(Name = "AcademicDegree")]
        public string AcademicDegree { get; set; }
                
        [Column(Name = "Position")]
        public string Position { get; set; }
                
        [Column(Name = "Address")]
        public string Address { get; set; }
                
        [Column(Name = "NumberPhone")]
        public string NumberPhone { get; set; }
                
        [Column(Name = "Organization_Id")]
        public int Organization_Id { get; set; }
    }
}