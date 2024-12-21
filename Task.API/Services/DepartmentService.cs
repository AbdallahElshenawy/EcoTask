using Microsoft.EntityFrameworkCore;
using Task1.API.DTO;
using Task1.API.Models;

namespace Task1.API.Services
{
    public class DepartmentService(ApplicationDbContext context) : IDepartmentService
    {
        public async Task<Department> CreateAsync(DepartmentEmployeeDTO departmentDTO)
        {
            var department = new Department
            {
                Name = departmentDTO.Name,
                Employees = departmentDTO.EmployeesNames?.Select(name => new Employee
                {
                    Name = name,
                }).ToList() ?? new List<Employee>() 
            };
            context.Departments.Add(department);
            await context.SaveChangesAsync();

            return department;
        }

        public async Task<DepartmentEmployeeDTO> GetByIdAsync(int id)
        {
            Department dept = await context.Departments.Include(d=>d.Employees).FirstOrDefaultAsync(d=>d.Id == id);
            DepartmentEmployeeDTO deptDTO = new DepartmentEmployeeDTO()
            {
                Id = dept.Id,
                Name = dept.Name,

            };
            foreach(Employee e in dept.Employees)
                deptDTO.EmployeesNames.Add(e.Name);
            return deptDTO;     
            
        }

        public async Task<IEnumerable<DepartmentEmployeeDTO>> GetAllAsync()
        {
            var departments = await context.Departments.Include(d => d.Employees).ToListAsync();

            var departmentDTOs = departments.Select(dept => new DepartmentEmployeeDTO
            {
                Id = dept.Id,
                Name = dept.Name,
                EmployeesNames = dept.Employees.Select(e => e.Name).ToList()
            }).ToList();

            return departmentDTOs;
        }

        public async Task<DepartmentEmployeeDTO> UpdateAsync(DepartmentEmployeeDTO departmentDTO)
        {
            var existingDepartment = await context.Departments.Include(d => d.Employees).FirstOrDefaultAsync(d => d.Id == departmentDTO.Id);
            if (existingDepartment == null)
                throw new Exception("Department not found.");

            existingDepartment.Name = departmentDTO.Name;

            if (departmentDTO.EmployeesNames != null && departmentDTO.EmployeesNames.Any())
            {
                existingDepartment.Employees.Clear();

                foreach (var employeeName in departmentDTO.EmployeesNames)
                {
                    var employee = new Employee { Name = employeeName };
                    existingDepartment.Employees.Add(employee);
                }
            }
            await context.SaveChangesAsync();

            return new DepartmentEmployeeDTO
            {
                Id = existingDepartment.Id,
                Name = existingDepartment.Name,
                EmployeesNames = existingDepartment.Employees.Select(e => e.Name).ToList()
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var department = await context.Departments.FindAsync(id);
            if (department == null)
                throw new Exception("Department not found.");

            context.Departments.Remove(department);
            await context.SaveChangesAsync();
            return true;
        }
    }

}
