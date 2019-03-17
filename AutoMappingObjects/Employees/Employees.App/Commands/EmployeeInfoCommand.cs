using Employees.DtoModels;
using Employees.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.App.Commands
{
    class EmployeeInfoCommand : ICommand
    {
        private EmployeeService employeeService;

        public EmployeeInfoCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        // <employeeId>
        public string Execute(params string[] args)
        {
            int employeeId = int.Parse(args[0]);

            EmployeeDto employee = this.employeeService.ById(employeeId);

            return $"{employee.Id} - {employee.FirstName} {employee.LastName} - ${employee.Salary:f2}";
        }
    }
}
