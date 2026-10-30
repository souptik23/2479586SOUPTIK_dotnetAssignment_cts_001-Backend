using EmployeeManagemantDotnetV1.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagemantDotnetV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class EmployeeController : ControllerBase
    {
        private EmployeeDbContext _employeeDbContext;
        public EmployeeController(EmployeeDbContext employeeDbContext)
        {
            this._employeeDbContext = employeeDbContext;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employees = _employeeDbContext.Employees.ToList();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = _employeeDbContext.Employees.Find(id);
            if (employee != null)
            {
                return Ok(employee);
            }
            else
            {
                var notFoundError = new
                {
                    Stats = "error",
                    Message = "not found"
                };
                return NotFound(notFoundError);
            }
        }

        [HttpPost]
        public IActionResult SaveEmployee(Employee employee)
        {
            _employeeDbContext.Employees.Add(employee);
            _employeeDbContext.SaveChanges();
            return Ok(employee);
        }

        [HttpPut]
        public IActionResult UpdateEmployee(Employee employee)
        {
            var record = _employeeDbContext.Employees.Find(employee.EmployeeId);
            if (record == null)
            {
                var notFoundResponse = new
                {
                    status = "Error",
                    Message = "employee not found in the database"
                };
                return NotFound(notFoundResponse);
            }
            else
            {
                record.FirstName = employee.FirstName;
                record.LastName = employee.LastName;
                record.Email = employee.Email;
                record.PhoneNumber = employee.PhoneNumber;
                record.Gender = employee.Gender;
                record.DateOfBirth = employee.DateOfBirth;
                record.Department = employee.Department;
                record.Designation = employee.Designation;
                record.DateOfJoining = employee.DateOfJoining;
                record.Address = employee.Address;
                record.UpdatedAt = DateTime.Now;

                _employeeDbContext.SaveChanges();

                return Ok(record);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id) 
        {
            var record = _employeeDbContext.Employees.Find(id);
            if(record != null)
            {
                _employeeDbContext.Employees.Remove(record);
                _employeeDbContext.SaveChanges();
                return Ok(record);
            }
            else
            {
                var notFoundResponse = new
                {
                    Status = "error",
                    Message = "record not found in the database"
                };
                return NotFound(notFoundResponse);
            }
        }
    }
}
