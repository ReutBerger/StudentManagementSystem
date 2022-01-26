using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentManagementSystem;
using StackExchange.Redis;
using StudentManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace StudentManagementSystem.Controllers
{
    [Route("api/Student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentContext _context;
        //private StudentManager _studentManager;

        private IDatabase _redisCache;

        private static ConnectionMultiplexer GetConnection()
        {
            string connectionString = "cacheDemo2.redis.cache.windows.net:6380,password=KlnsCt1ufYM4SoMk4v0pYACCMEc1dFC0mAzCaLULb9Y=,ssl=True,abortConnect=False";
            return ConnectionMultiplexer.Connect(connectionString);
        }

        public StudentController(StudentContext context)
        {
            //_studentManager = student_manager;
            _context = context;
            _redisCache = GetConnection().GetDatabase();
        }

       
        // Add student-
        // POST: api/Student
        [HttpPost]
        public async Task<ActionResult<string>> PostStudentAsync([FromBody] Student new_student)
        {
            // Check if request exist in cache.
            string keyRequest = "POSTrequest" + new_student.Student_Id;
            string responseValue = _redisCache.StringGet(keyRequest);
            if (responseValue == null)
            {
                Console.WriteLine("ADD " + responseValue); // DELETE
                // This is a new POST request - Not in the cache!

                // Check if the is age is valid.
                if (new_student.Age > 18)
                {
                    responseValue = "This is not a valid student's age";
                    _redisCache.StringSet(keyRequest, responseValue, expiry: new TimeSpan(0, 0, 10));
                    return BadRequest(responseValue);
                }
                
                try
                {
                    _context.Students.Add(new_student);
                    await _context.SaveChangesAsync();
                    //_studentManager.AddStudent(new_student);
                }
                catch(Exception e)
                {
                    //responseValue = "Failed add Student"; // DELETE
                    responseValue = e.Message;
                    _redisCache.StringSet(keyRequest, responseValue, expiry: new TimeSpan(0, 0, 10));
                    return BadRequest(responseValue);
                }
                responseValue = "Success add student";
                _redisCache.StringSet(keyRequest, responseValue, expiry: new TimeSpan(0, 0, 10));
                return Ok(responseValue);
            }
            else
            {
                // This POST request exist in the cache!
                return ResponseVal(responseValue);
            }
        }


        // Delete student-
        // DELETE: api/Student/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteStudentAsync(string id)
        {
            // Check if request exist in cache.
            string keyRequest = "DELETErequest" + id;
            string responseValue = _redisCache.StringGet(keyRequest);
            if (responseValue == null)
            {
                Console.WriteLine("DELETE " + responseValue); // DELETE
                // This is a new DELETE request - Not in the cache!
                
                var std = await _context.Students.FindAsync(id);
                try
                {
                    _context.Students.Remove(std);
                    await _context.SaveChangesAsync();
                    //_studentManager.DeleteStudent(id);
                }
                catch
                {
                    // THIS STUDENT ID NOT EXIST!
                    responseValue = "This student id not exist";
                    _redisCache.StringSet(keyRequest, responseValue, expiry: new TimeSpan(0, 0, 10));
                    return BadRequest(responseValue);
                }
                responseValue = "Success delete student";
                _redisCache.StringSet(keyRequest, responseValue, expiry: new TimeSpan(0, 0, 10));
                return Ok(responseValue);
            }
            else
            {
                // This DELETE request exist in the cache!
                return ResponseVal(responseValue);
            }
        }


        // Edit student-
        // PUT: api/Student/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> PutStudentAsync(string id, [FromBody] Student student)
        {
            // Check if exist in cache.
            string keyRequest = "PUTrequest" + id;
            string responseValue = _redisCache.StringGet(keyRequest);
            if (responseValue == null)
            {
                Console.WriteLine("EDIT " + responseValue); // DELETE
                // This is a new PUT request - Not in the cache!

                // Check if the request is valid.
                if (student.Student_Id != id)
                {
                    responseValue = "This is not a valid request";
                    _redisCache.StringSet(keyRequest, responseValue, expiry: new TimeSpan(0, 0, 10));
                    return BadRequest(responseValue);
                }

                _context.Entry(student).State = EntityState.Modified;

                try
                {
                    
                    await _context.SaveChangesAsync();
                    //_studentManager.EditStudent(id, student);
                }
                catch (Exception e)
                {
                    // THE STUDENT EXIST??
                    // responseValue = "Failed edit Student";
                    responseValue = e.Message; // DELETE
                    _redisCache.StringSet(keyRequest, responseValue, expiry: new TimeSpan(0, 0, 10));
                    return BadRequest(responseValue);
                }
                responseValue = "Success edit student";
                _redisCache.StringSet(keyRequest, responseValue, expiry: new TimeSpan(0, 0, 10));
                return Ok(responseValue);
            }
            else
            {
                // This PUT request exist in the cache!
                return ResponseVal(responseValue);
            }
        }


        // Get student-
        // GET: api/Student/{id}/{field}
        [HttpGet("{id}/{field}")]
        public async Task<ActionResult<string>> GetStudentAsync(string id, string field)
        {
            // Check if request exist in cache.
            string keyRequest = "GETrequest" + id + field;
            string responseValue = _redisCache.StringGet(keyRequest);
            if (responseValue == null)
            {
                Console.WriteLine("GET " + responseValue); // DELETE
                // This is a new GET request - Not in the cache!

                // Check if the is field is valid.
                if (field != "first_name" && field != "last_name" && field != "age" &&
                field != "grades_average" && field != "school_name" && field != "school_address")
                {
                    responseValue = "This is not a valid field name";
                    _redisCache.StringSet(keyRequest, responseValue, expiry: new TimeSpan(0, 0, 10));
                    return BadRequest(responseValue);
                }

                Student stud = await _context.Students.FindAsync(id);
                if (stud == null)
                {
                    responseValue = "This student id not exist";
                    _redisCache.StringSet(keyRequest, responseValue, expiry: new TimeSpan(0, 0, 10));
                    return NotFound(responseValue);
                }

                responseValue = GetStudent(stud, field);
                _redisCache.StringSet(keyRequest, responseValue, expiry: new TimeSpan(0, 0, 10));
                return Ok(responseValue);
            }
            else
            {
                // This GET request exist in the cache!
                Console.WriteLine(responseValue); // DELLETE
                if (responseValue.Contains("Failed"))
                {
                    return BadRequest(responseValue);
                }else if(responseValue.Contains("This")) {
                    return NotFound(responseValue);
                }
                else
                {
                    return Ok(responseValue);
                }
            }
        }


        private ActionResult<string> ResponseVal(string responseValue)
        {
            Console.WriteLine(responseValue); // DELLETE
            if (responseValue.Contains("Success"))
            {
                return Ok(responseValue);
            }
            return BadRequest(responseValue);
        }

        private string GetStudent(Student std, string field)
        {
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
            return "";
        }
    }
}