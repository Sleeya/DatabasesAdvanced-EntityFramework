using System;
using AutoMapper;

using Employees.Data;
using Employees.DtoModels;
using Employees.Models;

namespace Employees.Services
{
    public class EmployeeService
    {
        private readonly EmployeesContext context;

        public EmployeeService(EmployeesContext context)
        {
            this.context = context;
        }

        public EmployeeDto ById(int id)
        {
            var employee = context.Employees.Find(id);

            var employeeDto = Mapper.Map<EmployeeDto>(employee);

            return employeeDto;
        }

        public EmployeePersonalInfoDto PersonalById(int employeeId)
        {
            var employee = context.Employees.Find(employeeId);

            var employeeDto = Mapper.Map<EmployeePersonalInfoDto>(employee);

            return employeeDto;
        }


        public void AddEmployee(EmployeeDto employeeDto)
        {
            var employee = Mapper.Map<Employee>(employeeDto);

            this.context.Employees.Add(employee);

            this.context.SaveChanges();
        }


        public string SetBirthday(int employeeId, DateTime birthdayDate)
        {
            var employee = this.context.Employees.Find(employeeId);

            employee.Birthday = birthdayDate;

            this.context.SaveChanges();

            return $"{employee.FirstName}  {employee.LastName}";

        }

        public object SetAddress(int employeeId, string address)
        {
            var employee = this.context.Employees.Find(employeeId);

            employee.Address = address;

            this.context.SaveChanges();

            return $"{employee.FirstName}  {employee.LastName}";
        }
    }
}
