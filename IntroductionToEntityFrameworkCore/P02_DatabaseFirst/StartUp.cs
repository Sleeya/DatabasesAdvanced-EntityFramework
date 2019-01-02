using Microsoft.EntityFrameworkCore;
using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;
using System;
using System.Linq;

namespace P02_DatabaseFirst
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            using (SoftUniContext context = new SoftUniContext())
            {
                var employees = context.Employees.Select(x => new { x.EmployeeId,x.FirstName,x.MiddleName,x.LastName,x.JobTitle,x.Salary }).OrderBy(x=>x.EmployeeId);

                foreach (var e in employees)
                {
                    Console.WriteLine($"{e.FirstName} {e.MiddleName} {e.LastName} {e.JobTitle} {e.Salary:F2}");
                }
            }
        }
    }
}
