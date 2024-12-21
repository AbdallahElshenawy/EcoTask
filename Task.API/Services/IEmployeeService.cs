using Task1.API.DTO;
using Task1.API.Models;

namespace Task1.API.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeDeptDTO> CreateAsync(EmployeeDeptDTO employeedto);
        Task<EmployeeDeptDTO> GetByIdAsync(int id);
        Task<IEnumerable<EmployeeDeptDTO>> GetAllAsync();
        Task<Employee> UpdateAsync(Employee employee);
        Task<bool> DeleteAsync(int id); 
    }
}
