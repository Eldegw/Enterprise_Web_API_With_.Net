using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Address { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        [JsonIgnore]
        public Department? Department { get; set; }
    }
}
