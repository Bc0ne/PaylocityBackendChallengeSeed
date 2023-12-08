namespace Api.Dtos.Employee
{
    public class CalculateEmployeePaycheckDto
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public decimal SalaryAfterDeductions { get; set; }

        public decimal TotalDeductions { get; set; }
    }
}
