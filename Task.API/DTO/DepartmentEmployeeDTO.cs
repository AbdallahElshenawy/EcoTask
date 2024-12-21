namespace Task1.API.DTO
{
    public class DepartmentEmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string>? EmployeesNames { get; set; }= new List<string>();
    }
}
