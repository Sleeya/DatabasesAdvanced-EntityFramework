using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;
using System;
using System.IO;
using System.Linq;

namespace AddingANewAddressAndUpdatingEmployee
{
    class StartUp
    {
        static void Main(string[] args)
        {
            Address newAddress = new Address { AddressText = "Vitoshka 15", TownId = 4 };

            using (SoftUniContext context = new SoftUniContext())
            {
                Employee employee = context.Employees.FirstOrDefault(x => x.LastName == "Nakov");
                employee.Address = newAddress;

                context.SaveChanges();

                var employees = context.Employees.OrderByDescending(x => x.Address.AddressId).Take(10).Select(x => new { AdressText = x.Address.AddressText });

                using (StreamWriter writer = new StreamWriter("../ExerciseOutput.txt"))
                {
                    foreach (var e in employees)
                    {
                        Console.WriteLine($"{e.AdressText}");
                        writer.WriteLine($"{e.AdressText}");
                    }
                }
            }
        }
    }
}
