using AutoMapper;
using FastFood.DataProcessor.Dto.Import;
using FastFood.Models;

namespace FastFood.App
{
	public class FastFoodProfile : Profile
	{
		// Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
		public FastFoodProfile()
		{
            CreateMap<EmployeeImportDto, Employee>()
                .ForMember(e => e.Position, p => p.UseValue<Position>(null));

            CreateMap<ItemImportDto,Item>()
                .ForMember(i => i.Category, c => c.UseValue<Category>(null));

            CreateMap<OrderItemImportDto, OrderItem>()
                .ForMember(oi => oi.Item, x => x.UseValue<Item>(null))
                .ForMember(o => o.Order, x => x.UseValue<Order>(null));

            CreateMap<OrderImportDto, Order>()
                .ForMember(o => o.Employee, x => x.UseValue<Employee>(null));
        }
	}
}
