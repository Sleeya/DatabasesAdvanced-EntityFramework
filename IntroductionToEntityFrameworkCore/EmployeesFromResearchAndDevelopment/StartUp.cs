using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;
using System;
using System.IO;
using System.Linq;

namespace EmployeesFromResearchAndDevelopment
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            using (SoftUniContext context = new SoftUniContext())
            {
                var employees = context.Employees
                    .Where(x => x.Department.Name == "Research and Development")
                    .Select(x => new
                    {
                        x.FirstName,
                        x.LastName,
                        DepartmentName = x.Department.Name,
                        x.Salary
                    })
                    .OrderBy(x => x.Salary)
                    .ThenByDescending(x => x.FirstName);

                using (StreamWriter writer = new StreamWriter("../ExerciseOutput.txt"))
                {
                    foreach (var e in employees)
                    {
                        Console.WriteLine($"{e.FirstName} {e.LastName} from {e.DepartmentName} - ${e.Salary:f2}");
                        writer.WriteLine($"{e.FirstName} {e.LastName} from {e.DepartmentName} - ${e.Salary:f2}");
                    }
                }
            }
        }
    }
}
