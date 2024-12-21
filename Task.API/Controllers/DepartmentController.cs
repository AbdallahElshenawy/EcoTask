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
    public class DepartmentController(IDepartmentService departmentService) : ControllerBase
    {
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateDepartment(DepartmentEmployeeDTO departmentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await departmentService.CreateAsync(departmentDTO);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            var department = await departmentService.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await departmentService.GetAllAsync();
            return Ok(departments);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, DepartmentEmployeeDTO departmentDTO)
        {
            if (id != departmentDTO.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedDepartment = await departmentService.UpdateAsync(departmentDTO);
            if (updatedDepartment == null)
            {
                return NotFound();
            }

            return Ok(updatedDepartment);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var result = await departmentService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}