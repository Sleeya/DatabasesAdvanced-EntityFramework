using System.Linq;

namespace FastFood.DataProcessor.Dto.Export
{
    public class EmployeeOrdersDto
    {
        public string Name { get; set; }
        public OrderExportDto[] Orders { get; set; }

        public decimal TotalMade => Orders
                                    .Sum(o => o.Items.Sum(i => i.Price * i.Quantity));
    }
}
