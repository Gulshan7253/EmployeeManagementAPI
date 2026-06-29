namespace EmployeeManagementAPI.Models
{
    public class ApiResponse
    {
        public string? Success { get; set; }
        public string? Message { get; set; } 
        public object? Data { get; set; }
    }
}
