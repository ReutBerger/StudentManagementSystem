using System;
using System.Collections.Concurrent;

namespace StudentManagementSystem.Models
{
    public class StudentManager
    {
        private static ConcurrentDictionary<string, Student> dict_student;

        public StudentManager()
        {
            dict_student = new ConcurrentDictionary<string, Student>();
        }

        /** Try to add a new student **/
        public void AddStudent(Student new_student)
        {
            string id = new_student.Student_Id;
            // Create new student.
            Student std = new Student()
            {
                Student_Id = id,
                First_Name = new_student.First_Name,
                Last_Name = new_student.Last_Name,
                Age = new_student.Age,
                Grades_Average = new_student.Grades_Average,
                School_Name = new_student.School_Name,
                School_Address = new_student.School_Address
            };

            // Add new student to dict_student.
            if (!dict_student.ContainsKey(id))
            {
                // This student not exist - add it to dictionary.
                dict_student.TryAdd(id, std);
            }
            else
            {
                // This student is already exist.
                throw new Exception("This student is already exist");
            }
        }

        /** Try to remove the student with this id **/
        public void DeleteStudent(string id)
        {
            // Remove student from dict_student.
            if (dict_student.ContainsKey(id))
            {
                // This student exist
                Student std = dict_student[id];
                dict_student.TryRemove(id, out std);
            }
            else
            {
                // This student id NOT exist.
                throw new Exception("This student id not exist");
            }
        }

        /** Try to edit the student with this id **/
        public void EditStudent(string id, Student student)
        {
            // Edit this "id" student.
            if (dict_student.ContainsKey(id))
            {
                // This student exist
                Student std = dict_student[id];
                std.First_Name = student.First_Name;
                std.Last_Name = student.Last_Name;
                std.Age = student.Age;
                std.Grades_Average = student.Grades_Average;
                std.School_Name = student.School_Name;
                std.School_Address = student.School_Address;
            }
            else
            {
                // This student not exist - create new student.
                Student std = new Student()
                {
                    Student_Id = id,
                    First_Name = student.First_Name,
                    Last_Name = student.Last_Name,
                    Age = student.Age,
                    Grades_Average = student.Grades_Average,
                    School_Name = student.School_Name,
                    School_Address = student.School_Address
                };
                dict_student.TryAdd(id, std);
            }
        }

        /** Try to get student detail with this id **/
        public string GetStudent(string id, string field)
        {
            if (dict_student.ContainsKey(id))
            {
                // This student exist
                Student std = dict_student[id];
                switch (field)
                {
                    case "first_name":
                        return std.First_Name;
                    case "last_name":
                        return std.Last_Name;
                    case "age":
                        return std.Age.ToString();
                    case "grades_average":
                        return std.Grades_Average.ToString();
                    case "school_name":
                        return std.School_Name;
                    case "school_address":
                        return std.School_Address;
                    default:
                        Console.WriteLine("Nothing");
                        break;
                }
            }
            else
            {
                // This student NOT exist.
                throw new Exception("This student not exist");
            }
            return "";
        }
    }
}
