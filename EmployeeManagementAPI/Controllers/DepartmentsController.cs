using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly AuthRepository _authRepository;
        public DepartmentsController(AuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var departments = await _authRepository.GetDepartments();
                return Ok(new ApiResponse
                {
                    Success = "1",
                    Message = "Departments retrieved successfully.",
                    Data = departments
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Success = "-1",
                    Message = ex.Message
                });
            }
        }
       
        
    }
}
