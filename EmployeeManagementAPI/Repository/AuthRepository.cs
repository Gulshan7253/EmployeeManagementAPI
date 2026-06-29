using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace EmployeeManagementAPI.Repository
{
    
    public class AuthRepository
    {
        private readonly DbHelper _dbHelper;
        public AuthRepository(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<int> Register(string username,string email,string password)
        {
            using var connection = _dbHelper.GetConnection();
            {
                await connection.OpenAsync();
                using SqlCommand cmd = new SqlCommand("ManagementSp.Sp_RegisterUser", connection);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FullName", username);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password); 
                var result= await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(result);

                
            }
        }

        public async Task<int> Login(string email, string password)
        {
            using var connection = _dbHelper.GetConnection();
            {
                await connection.OpenAsync();
                using SqlCommand cmd = new SqlCommand("[ManagementSp].[LoginUser]", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                // return await cmd.ExecuteNonQueryAsync();
                var result = await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }
        public async Task<int> CreateDepartment(string departmentName)
        {
            using var connection = _dbHelper.GetConnection();
            {
                await connection.OpenAsync();
                using SqlCommand cmd = new SqlCommand("ManagementSp.Department", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure; 
                cmd.Parameters.AddWithValue("@departmentName", departmentName); 
                var result = await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }

        public async Task<List<DepartmentModel>> GetDepartments()
        {
            var list = new List<DepartmentModel>();
            using var connection = _dbHelper.GetConnection();
            {
                await connection.OpenAsync();
                using SqlCommand cmd = new SqlCommand("ManagementSp.departmentList", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    list.Add(new DepartmentModel
                    {
                        DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                        DepartmentName = reader.GetString(reader.GetOrdinal("DepartmentName"))
                    });
                }
            }

            return list;
        }

        public async Task<int> DeleteDepartment(int departmentId)
        {
            using var connection = _dbHelper.GetConnection();
            {
                await connection.OpenAsync();
                using SqlCommand cmd = new SqlCommand("ManagementSp.DeleteDepartment", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DepartmentId", departmentId);
                var result = await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }
    }
}
