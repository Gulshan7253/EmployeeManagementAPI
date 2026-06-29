using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly AuthRepository _authRepository;
        public RegisterController(AuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post(RegisterModel request)
        {

            try
            {
                if (string.IsNullOrEmpty(request.FullName))
                {
                    return NotFound(new ApiResponse
                    {
                        Success = "-4",
                        Message = "Full name invalid or empty."
                    });
                }
                if (string.IsNullOrEmpty(request.Email))
                {
                    return NotFound(new ApiResponse
                    {
                        Success = "-4",
                        Message = "Email not found or empty"
                    });
                }
                if (string.IsNullOrEmpty(request.Password))
                {
                    return NotFound(new ApiResponse
                    {
                        Success = "-4",
                        Message = "Password not found or empty"
                    });
                }
                var result = await _authRepository.Register(request.FullName, request.Email, request.Password);
                if (result == -2)
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = "-2",
                        Message = "Email already exists"
                    });
                }
                if (result == 1)
                {
                    return Ok(new
                    {
                        Success = "1",
                        Message = "User registered successfully"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Success = "-1",
                        Message = "Server error"
                    });
                }
            }
            catch (Exception ex)
            {


                return BadRequest(new
                {
                    Success = "-1",
                    Message = ex.Message
                });
            }

        }
    }
}
