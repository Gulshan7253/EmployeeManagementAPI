using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateDepartmentController : ControllerBase
    {
        private readonly AuthRepository _authRepository;
        public CreateDepartmentController(AuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(CreateDepartmentRequestModel request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.DepartmentName))
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = "-2",
                        Message = "Department name is required."
                    });
                }

                var result = await _authRepository.CreateDepartment(request.DepartmentName);

                if (result == 2)
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = "-2",
                        Message = "Department already exists."
                    });
                }

                if (result == 1)
                {
                    return Ok(new ApiResponse
                    {
                        Success = "1",
                        Message = "Department created successfully."
                    });
                }

                return StatusCode(500, new ApiResponse
                {
                    Success = "-1",
                    Message = "Something went wrong."
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
