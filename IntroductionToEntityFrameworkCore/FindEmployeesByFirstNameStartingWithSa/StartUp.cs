using Microsoft.EntityFrameworkCore;
using P02_DatabaseFirst.Data;
using System;
using System.IO;
using System.Linq;

namespace FindEmployeesByFirstNameStartingWithSa
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (SoftUniContext context = new SoftUniContext())
            {
                var employees = context.Employees
                    .Where(e=> EF.Functions.Like(e.FirstName,"Sa%"))
                    .Select(e => new 
                    {
                        e.FirstName,
                        e.LastName,
                        e.JobTitle,
                        e.Salary
                    })
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName);

                using (StreamWriter writer = new StreamWriter("../ExerciseOutput.txt"))
                {
                    foreach (var e in employees)
                    {
                        Console.WriteLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f2})");
                        writer.WriteLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f2})");
                    }
                }
            }
        }
    }
}
