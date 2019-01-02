using P02_DatabaseFirst.Data;
using System;
using System.Linq;

namespace EmployeesWithSalaryOver50000
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            using (SoftUniContext context = new SoftUniContext())
            {
                var employees = context.Employees.Where(x => x.Salary > 50000).Select(x => new { x.FirstName}).OrderBy(x=>x.FirstName);
                
                foreach (var e in employees)
                {
                    Console.WriteLine($"{e.FirstName}");
                }
            }
        }
    }
}
