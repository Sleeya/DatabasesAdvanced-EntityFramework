using P02_DatabaseFirst.Data;
using System;
using System.IO;
using System.Linq;

namespace AddressesByTown
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (SoftUniContext context = new SoftUniContext())
            {
                var addresses = context.Addresses
                    .OrderByDescending(x => x.Employees.Count)
                    .ThenBy(x => x.Town.Name)
                    .ThenBy(x => x.AddressText)
                    .Take(10)
                    .Select(x => new
                    {
                        x.AddressText,
                        TownName = x.Town.Name,
                        EmployeeCount = x.Employees.Count
                    });

                using (StreamWriter writer = new StreamWriter("../ExerciseOutput.txt"))
                {
                    foreach (var a in addresses)
                    {
                        Console.WriteLine($"{a.AddressText}, {a.TownName} - {a.EmployeeCount} employees");
                        writer.WriteLine($"{a.AddressText}, {a.TownName} - {a.EmployeeCount} employees");
                    }
                }
            }
        }
    }
}
