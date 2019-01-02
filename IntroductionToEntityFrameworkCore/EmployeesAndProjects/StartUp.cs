using P02_DatabaseFirst.Data;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace EmployeesAndProjects
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (SoftUniContext context = new SoftUniContext())
            {
                var employees = context.Employees
                    .Where(x => x.EmployeesProjects.Any(y => y.Project.StartDate.Year >= 2001 && y.Project.StartDate.Year <= 2003))
                    .Take(30)
                    .Select(x => new
                    {
                        x.FirstName,
                        x.LastName,
                        ManagerFirstName = x.Manager.FirstName,
                        ManagerLastName = x.Manager.LastName,
                        Projects = x.EmployeesProjects.Select(ep => new
                        {
                            ep.Project.Name,
                            ep.Project.StartDate,
                            ep.Project.EndDate
                        })
                    });
                using (StreamWriter writer = new StreamWriter("../ExerciseOutput.txt"))
                {


                    foreach (var e in employees)
                    {
                        Console.WriteLine($"{e.FirstName} {e.LastName} - Manager: {e.ManagerFirstName} {e.ManagerLastName}");
                        writer.WriteLine($"{e.FirstName} {e.LastName} - Manager: {e.ManagerFirstName} {e.ManagerLastName}");

                        foreach (var p in e.Projects)
                        {
                            Console.WriteLine($"--{p.Name} - {p.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)}" +
                                $" - {p.EndDate?.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture) ?? "not finished"}");
                            writer.WriteLine($"--{p.Name} - {p.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)}" +
                                $" - {p.EndDate?.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture) ?? "not finished"}");
                        }
                    }
                }
            }
        }
    }
}
