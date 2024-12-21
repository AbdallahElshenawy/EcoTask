namespace Task1.API.DTO
{
    public class EmployeeDeptDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public decimal Salary { get; set; }

        public string? DepartmentName { get; set; }
    }
}
