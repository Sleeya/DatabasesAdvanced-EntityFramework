using P02_DatabaseFirst.Data;
using System;
using System.IO;
using System.Linq;

namespace Employee147
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (SoftUniContext context = new SoftUniContext())
            {
                var employee = context.Employees.Where(x=> x.EmployeeId == 147)
                    .Select(e=> new
                    {
                        e.FirstName,
                        e.LastName,
                        e.JobTitle,
                        Projects = e.EmployeesProjects.Select(p=> new { ProjectName = p.Project.Name }).OrderBy(p=>p.ProjectName)
                    })
                    .FirstOrDefault();

                using (StreamWriter writer = new StreamWriter("../ExerciseOutput.txt"))
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
                    writer.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
                    foreach (var p in employee.Projects)
                    {
                        Console.WriteLine($"{p.ProjectName}");
                        writer.WriteLine($"{p.ProjectName}");
                    }
                }
            }
        }
    }
}
