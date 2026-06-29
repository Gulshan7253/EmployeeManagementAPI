using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteDepartmentController : ControllerBase
    {
        private readonly AuthRepository _authRepository;
        public DeleteDepartmentController(AuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(int departmentId)
        {
            try
            {
                var result = await _authRepository.DeleteDepartment(departmentId);
                if (result == 1)
                {
                    return Ok(new ApiResponse
                    {
                        Success = "1",
                        Message = "Department deleted successfully."
                    });
                }
                return BadRequest(new ApiResponse
                {
                    Success = "-2",
                    Message = "Department not found."
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
