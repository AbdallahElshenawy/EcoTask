using Task1.API.DTO;
using Task1.API.Models;

namespace Task1.API.Services
{
    public interface IDepartmentService
    {
        Task<Department> CreateAsync(DepartmentEmployeeDTO departmentDTO);
        Task<DepartmentEmployeeDTO> GetByIdAsync(int id);
        Task<IEnumerable<DepartmentEmployeeDTO>> GetAllAsync();
        Task<DepartmentEmployeeDTO> UpdateAsync(DepartmentEmployeeDTO departmentdto);
        Task<bool> DeleteAsync(int id);
    }
}
