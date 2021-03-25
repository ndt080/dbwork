using System.Data.Linq.Mapping;

namespace dbWork.Models
{
    public class Organization
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        
        [Column(Name = "Name")]
        public string Name { get; set; }
    }
}