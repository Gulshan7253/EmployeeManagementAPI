using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Web.Helpers;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        public OrderController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        [HttpGet]
        public async  Task<IActionResult> GetOrders()
        {
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJQcm9kdWN0QGdtYWlsLmNvbSIsImV4cCI6MTc4MjQxNDMxMiwiaXNzIjoiRW1wbG95ZWVNYW5hZ2VtZW50QVBJIiwiYXVkIjoiRW1wbG95ZWVNYW5hZ2VtZW50QVBJIn0.cOGDtaRyV8aXOeEIcR698fBXK5kUMtBwpEAvqz8JPTI";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var department = await _httpClient.GetFromJsonAsync<object>("https://localhost:44395/api/Departments"); 
            return Ok(new
            {
                Message = "Order created successfully.",
                DepartmentData = department
            });
        }
    }
}
