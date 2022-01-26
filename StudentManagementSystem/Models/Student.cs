using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class Student
    {
        [Key]
        [JsonProperty("student_id")]
        public string Student_Id { get; set; }

        [JsonProperty("first_name")]
        public string First_Name { get; set; }

        [JsonProperty("last_name")]
        public string Last_Name { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("grades_average")]
        public double Grades_Average { get; set; }

        [JsonProperty("school_name")]
        public string School_Name { get; set; }

        [JsonProperty("school_address")]
        public string School_Address { get; set; }
    }
}
