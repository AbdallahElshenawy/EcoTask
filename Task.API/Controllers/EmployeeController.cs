using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task1.API.DTO;
using Task1.API.Models;
using Task1.API.Services;

namespace Task.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController(IEmployeeService employeeService) : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeDeptDTO employeedto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await employeeService.CreateAsync(employeedto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await employeeService.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await employeeService.GetAllAsync();
            return Ok(employees);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedEmployee = await employeeService.UpdateAsync(employee);
            if (updatedEmployee == null)
            {
                return NotFound();
            }

            return Ok(updatedEmployee);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await employeeService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

