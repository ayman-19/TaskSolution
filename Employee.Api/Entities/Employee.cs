using System.ComponentModel.DataAnnotations;

namespace Employee.Api.Entities
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }

        public static Employee Create(string Name, string Department, decimal Salary) =>
            new Employee
            {
                Name = Name,
                Department = Department,
                Salary = Salary
            };
    }
}
