using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ApiDocker.Model;

namespace ApiDocker.Business
{
    public interface IStudentBusiness
    {
        ActionResult<Student> Create(Student student);
        ActionResult<Student> FindById(long id);
        ActionResult<List<Student>> FindAll();
        ActionResult<int> Delete(long id);
        ActionResult<Student> Update(Student student);
    }
}
