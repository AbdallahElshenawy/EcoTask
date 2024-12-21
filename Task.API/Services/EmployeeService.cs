using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Task1.API.DTO;
using Task1.API.Models;
namespace Task1.API.Services
{
    public class EmployeeService(ApplicationDbContext context) : IEmployeeService
    {
        public async Task<EmployeeDeptDTO> CreateAsync(EmployeeDeptDTO employeeDTO)
        {
            var employee = new Employee
            {
                Name = employeeDTO.Name,
                Position = employeeDTO.Position,
                Salary = employeeDTO.Salary,
                DepartmentId = context.Departments
                    .Where(d => d.Name == employeeDTO.DepartmentName)
                    .Select(d => d.Id)
                    .FirstOrDefault() 
            };

            context.Employees.Add(employee);
            await context.SaveChangesAsync();

            employeeDTO.Id = employee.Id;
            return employeeDTO;
        }

        public async Task<EmployeeDeptDTO> GetByIdAsync(int id)
        {
            Employee emp = context.Employees.Include(e=>e.Department).FirstOrDefault(e=>e.Id==id);
            if (emp == null)
                throw new Exception("Employee not found.");

            EmployeeDeptDTO empDTO = new EmployeeDeptDTO()
            {
                Id = emp.Id,
                Name = emp.Name,
                Position = emp.Position,
                Salary = emp.Salary,
                DepartmentName = emp.Department.Name,
            };
            return empDTO;
        }

        public async Task<IEnumerable<EmployeeDeptDTO>> GetAllAsync()
        {
            var employees = await context.Employees
                .Include(e => e.Department)
                .ToListAsync();

            var employeeDTOs = employees.Select(emp => new EmployeeDeptDTO
            {
                Id = emp.Id,
                Name = emp.Name,
                Position = emp.Position,
                Salary = emp.Salary,
                DepartmentName = emp.Department?.Name 
            });

            return employeeDTOs;
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            var existingEmployee = await context.Employees.FindAsync(employee.Id);
            if (existingEmployee == null)
                throw new Exception("Employee not found.");

            existingEmployee.Name = employee.Name;
            existingEmployee.Position = employee.Position;
            existingEmployee.Salary = employee.Salary;
            existingEmployee.DepartmentId = employee.DepartmentId;

            await context.SaveChangesAsync();
            return existingEmployee;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await context.Employees.FindAsync(id);
            if (employee == null)
                throw new Exception("Employee not found.");

            context.Employees.Remove(employee);
            await context.SaveChangesAsync();
            return true;
        }
    }

}
