using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BindingController : ControllerBase
    {
        [HttpGet("{age:int}/{name:alpha}")]
        public IActionResult TestPrimitive(int age, string? name)
        {
            return Ok();
        }

        [HttpPost("{name}")]
        public IActionResult TestObj(Department dept, string? name)
        {
            return Ok();
        }


        [HttpGet("{Id}/{Name}/{Managername}")]
        public IActionResult TestCustomBinding([FromRoute] Department dept)
        {
            return Ok();
        }
    
    
    }   

}
