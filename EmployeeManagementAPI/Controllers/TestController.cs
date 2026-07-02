using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("Api is working");
        }

        private enum TestEnum
        {
            Value1,
            Value2,
            Value3
        }

        public class Value1 {

            public int testId { get; set; }
            public int TestName { get; set; }
        }
    }
}
