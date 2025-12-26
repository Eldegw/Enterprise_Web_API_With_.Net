using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO;
using WebApi.Models;
using WebApi.Repository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository Empo;

        public EmployeeController(IEmployeeRepository _Empo)
        {
            Empo = _Empo;
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
          Employee emp =  Empo.GetById(id);
          GeneralResponse respone = new GeneralResponse();

            if (emp != null)
            {
                respone.IsSuccess = true;
                respone.Data = emp;
            }
            else
            {
                respone.IsSuccess = false;
                respone.Data = "ID Is Not Valid";
            }

            return Ok(respone);
        }
    }
}
