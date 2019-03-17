using AutoMapper;

using Employees.DtoModels;
using Employees.Models;

namespace Employees.App
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
            CreateMap<Employee, EmployeePersonalInfoDto>();
        }
    }
}
