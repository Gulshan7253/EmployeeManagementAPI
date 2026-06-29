using EmployeeManagementAPI.Helpers;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AuthRepository _authRepository;
        private readonly JwtService _jwtService;
        public LoginController(AuthRepository authRepository, JwtService jwtService)
        {
            _authRepository = authRepository;
            _jwtService = jwtService;
        }


        [HttpPost]
        public async Task<IActionResult> Post(LoginModel request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Email))
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = "-4",
                        Message = "Invalid email or empty."

                    });
                }
                if (string.IsNullOrEmpty(request.Password))
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = "-4",
                        Message = "Invalid password or empty."

                    });
                }
                var result = await _authRepository.Login(request.Email, request.Password);

                if (result == 1)
                {
                    var token = _jwtService.GenerateToken(request.Email);
                    return Ok(new
                    {
                        Success = "1",
                        Message = "User login successfully.",
                        Token = token
                    });
                }
                if (result == 4)
                {
                    return Ok(new ApiResponse
                    {
                        Success = "4",
                        Message = "Invalid email."
                    });
                }
                if (result == 5)
                {
                    return Ok(new ApiResponse
                    {
                        Success = "5",
                        Message = "Invalid password or emailId"
                    });
                }
                else
                {
                    return StatusCode(500, new ApiResponse
                    {
                        Success = "-1",
                        Message = "Server error."
                    });
                }
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
