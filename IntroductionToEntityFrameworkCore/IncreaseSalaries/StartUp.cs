using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;
using System;
using System.IO;
using System.Linq;

namespace IncreaseSalaries
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (SoftUniContext context = new SoftUniContext())
            {
                var employees = context.Employees
                    .Where(e => new[] { "Engineering", "Tool Design", "Marketing", "Information Services" }.Contains(e.Department.Name))
                    .OrderBy(e=>e.FirstName)
                    .ThenBy(e=>e.LastName);

                using (StreamWriter writer = new StreamWriter("../ExerciseOutput.txt"))
                {
                    foreach (var e in employees)
                    {
                        e.Salary *= 1.12m;
                        Console.WriteLine($"{e.FirstName} {e.LastName} (${e.Salary:f2})");
                        writer.WriteLine($"{e.FirstName} {e.LastName} (${e.Salary:f2})");
                    }
                }

                context.SaveChanges();
            }
        }
    }
}
