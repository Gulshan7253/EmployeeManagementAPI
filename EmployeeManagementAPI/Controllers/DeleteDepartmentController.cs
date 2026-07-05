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
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteDepartmentRequestModel request)
        {
            try
            {
                var departmentid = request.departmentId;
                var result = await _authRepository.DeleteDepartment(departmentid);
                if (result == 1)
                {
                    return Ok(new ApiResponse
                    {
                        Success = "1",
                        Message = "Department deleted successfully."
                    });
                }
                if (result == 4)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = "4",
                        Message = "Department Not Found."
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
