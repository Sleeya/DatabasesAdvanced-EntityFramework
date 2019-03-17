using Employees.DtoModels;
using Employees.Services;
using System;

namespace Employees.App.Commands
{
    class EmployeePersonalInfoCommand : ICommand
    {
        private EmployeeService employeeService;

        public EmployeePersonalInfoCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        // <employeeId>
        public string Execute(params string[] args)
        {
            int employeeId = int.Parse(args[0]);

            EmployeePersonalInfoDto employee = this.employeeService.PersonalById(employeeId);

            return $"ID: {employee.Id} - {employee.FirstName} {employee.LastName} - {employee.Salary:f2} {Environment.NewLine}" +
                $"Birthday: {employee.Birthday?.ToString("dd-MM-yyyy")} {Environment.NewLine}" +
                $"Address: { employee.Address}";
        }
    }
}
