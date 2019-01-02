using P02_DatabaseFirst.Data;
using System;
using System.IO;
using System.Linq;

namespace DeleteProjectById
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (SoftUniContext context = new SoftUniContext())
            {
                var project = context.Projects.FirstOrDefault(p => p.ProjectId == 2);

                var employeesProjects = context.EmployeesProjects.Where(x => x.ProjectId == 2);
                context.EmployeesProjects.RemoveRange(employeesProjects);

                context.Projects.Remove(project);

                context.SaveChanges();

                var projects = context.Projects.Take(10);

                using (StreamWriter writer = new StreamWriter("../ExerciseOutput.txt"))
                {
                    foreach (var p in projects)
                    {
                        Console.WriteLine($"{p.Name}");
                        writer.WriteLine($"{p.Name}");
                    }
                }
            }
        }
    }
}
