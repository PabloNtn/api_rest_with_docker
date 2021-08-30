using ApiDocker.Model;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ApiDocker.Repository
{
    public interface IStudentRepository
    {
        Student Create(Student student);
        Student FindById(long id);
        List<Student> FindAll();
        ActionResult<int> Delete(long id);
        Student Update(Student student);
    }
}
