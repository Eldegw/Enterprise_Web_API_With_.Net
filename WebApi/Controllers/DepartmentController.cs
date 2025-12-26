using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO;
using WebApi.Models;
using WebApi.Repository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository Repo;

        public DepartmentController(IDepartmentRepository _Repo)
        {
            Repo = _Repo;
        }

        [HttpGet("p")]
        [Authorize]
        public ActionResult<List<DeptWithEmpCountDto>> GetDeptDetails()
        {
            List<Department> deptlist = Repo.GetDeptDetails();
            List<DeptWithEmpCountDto> deptDtoList = new List<DeptWithEmpCountDto>();

            foreach (Department dept in deptlist)
            {
                DeptWithEmpCountDto deptDto = new DeptWithEmpCountDto();
               deptDto.Name = dept.Name;
                deptDto.Id = dept.Id;
                deptDto.EmpCount = dept.Emps.Count();

                deptDtoList.Add(deptDto);
                 
            }

            return deptDtoList;

        }





        [HttpGet]
        public IActionResult DisplayAllDept()
        {
            List<Department> deptList = Repo.GetAll();
            return Ok(deptList);
        }



        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            Department dept = Repo.GetById(id);
            return Ok(dept);
        }

        [HttpGet("{name:alpha}")]
        public IActionResult GetByName(string name)
        {
            Department dept = Repo.GetByName(name);
            return Ok(dept);

        }




        [HttpPost]
        public IActionResult AddDept(Department dept)
        {
          Repo.Add(dept);
          Repo.SaveChanges();
            return CreatedAtAction("GetById", new { id = dept.Id }, dept);
        }



        [HttpPut("{id:int}")]
        public IActionResult UpdateDept(Department deptFromRequest , int id )
        {
            
           Department deptFromDB =  Repo.GetById(id);

            if (deptFromDB != null)
            {
                deptFromDB.Name = deptFromRequest.Name;
                deptFromDB.ManagerName = deptFromRequest.ManagerName;
                Repo.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound("Department Not Valid");
            }

        }

       
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            Department dept = Repo.GetById(id);
            if (dept != null)
            {
                Repo.Delete(id);
                Repo.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound("Department Not Found");
            }
        }



    }
}
