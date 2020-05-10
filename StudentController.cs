using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cwiczenia_nr_2.Models;

namespace Cwiczenia_nr_2.Controllers

{
    [ApiController]
    [Route("api/students")]
    public class StudentController : ControllerBase
    {
        string conString = "Data Source=db-mssql;Initial Catalog=s17556;Integrated Security=True";


        [HttpGet]

        public IActionResult GetStudents(string orderBy)
        {
            //return $"Kowalski, Malewski, Andrzejewski sortowanie={orderBy}";

            var list = new List<StudentInfoDTO>();
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select s.FirstName, s.LastName, s.BirthDate, st.Name, e.Semester from Student s join Enrollment e on e.IdEnrollment = s.IdEnrollment join Studies st on st.IdStudy = e.IdStudy";
                con.Open();

                SqlDataReader dr = com.ExecuteReader();

                while (dr.Read())
                {
                    var st = new StudentInfoDTO
                    {
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        Name = dr["Name"].ToString(),
                        BirthDate = dr["BirthDate"].ToString(),
                        Semester = dr["Semester"].ToString(),
                    };

                    list.Add(st);
                }
                }
            return Ok(list);
                }

        [HttpGet("{id}")]
        public IActionResult GetStudent(string id)
        {

            //StudentInfoDTO st = new StudentInfoDTO
            using (SqlConnection connect = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand())
            {

                command.Connection = connect;
                command.CommandText = "select * from Student where IndexNumber=@id";

                command.Parameters.AddWithValue("id", id);

                connect.Open();

                var dr = command.ExecuteReader();

                if(dr.Read())
                {
                    var st = new Student();

                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.IndexNumber = dr["Name"].ToString();
                    return Ok(st);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult CreateStudent(Models.Student student)
        {
            //...add to databaase
            //... generating index number
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }

    }
        
}