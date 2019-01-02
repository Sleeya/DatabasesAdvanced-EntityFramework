using P02_DatabaseFirst.Data;
using System;
using System.IO;
using System.Linq;

namespace DepartmentsWithMoreThanFiveEmployees
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (SoftUniContext context = new SoftUniContext())
            {
                var departments = context.Departments.Where(d => d.Employees.Count > 5)
                    .OrderBy(d => d.Employees.Count)
                    .ThenBy(d => d.Name)
                    .Select(d => new
                    {
                        d.Name,
                        ManagerFirstName = d.Manager.FirstName,
                        ManagerLastName = d.Manager.LastName,
                        Employees = d.Employees.Select(e=> new
                        {
                            e.FirstName,
                            e.LastName,
                            e.JobTitle
                        }).OrderBy(e=>e.FirstName).ThenBy(e=>e.LastName)
                    });

                using (StreamWriter writer = new StreamWriter("../ExerciseOutput.txt"))
                {
                    foreach (var d in departments)
                    {
                        Console.WriteLine($"{d.Name} - {d.ManagerFirstName} {d.ManagerLastName}");
                        writer.WriteLine($"{d.Name} - {d.ManagerFirstName} {d.ManagerLastName}");
                        foreach (var e in d.Employees)
                        {
                            Console.WriteLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");
                            writer.WriteLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");
                        }

                        Console.WriteLine(new string('-',10));
                        writer.WriteLine(new string('-', 10));
                    }
                }
            }
        }
    }
}
