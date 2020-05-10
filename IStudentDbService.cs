using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cwiczenia_nr_3.DTOs.Requests;

namespace Cwiczenia_nr_3.Services
{
    public interface IStudentDbService
    {
        void EnrollStudent(EnrollStudentRequest request);
        void PromoteStudents(int semester, string studies);
    }
}