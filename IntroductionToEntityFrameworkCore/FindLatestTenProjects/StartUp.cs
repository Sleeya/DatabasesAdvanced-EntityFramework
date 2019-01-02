using System;
using System.Globalization;
using System.IO;
using System.Linq;
using P02_DatabaseFirst.Data;

namespace FindLatestTenProjects
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (SoftUniContext context = new SoftUniContext())
            {
                var projects = context.Projects
                    .Select(p => new
                    {
                        p.Name,
                        p.Description,
                        p.StartDate
                    })
                    .OrderByDescending(p => p.StartDate)
                    .Take(10)
                    .OrderBy(p => p.Name);

                using (StreamWriter writer = new StreamWriter("../ExerciseOutput.txt"))
                {
                    foreach (var p in projects)
                    {
                        Console.WriteLine($"{p.Name} {Environment.NewLine} {p.Description} {Environment.NewLine} {p.StartDate.ToString("M/d/yyyy h:mm:ss tt",CultureInfo.InvariantCulture)}");
                        writer.WriteLine($"{p.Name} {Environment.NewLine} {p.Description} {Environment.NewLine} {p.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)}");
                    }
                }
            }
        }
    }
}
