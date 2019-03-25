using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using FastFood.Data;
using FastFood.DataProcessor.Dto.Export;
using FastFood.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FastFood.DataProcessor
{
    public class Serializer
    {
        public static string ExportOrdersByEmployee(FastFoodDbContext context, string employeeName, string orderType)
        {
            var orderDtos = context.Employees
                .Where(e => e.Name == employeeName)
                .Select(e => new EmployeeOrdersDto
                {
                    Name = e.Name,
                    Orders = e.Orders
                                .Where(o => o.Type == Enum.Parse<OrderType>(orderType))
                                .Select(o => new OrderExportDto
                                {
                                    Customer = o.Customer,
                                    Items = o.OrderItems
                                                .Select(oi => new ItemExportDto
                                                {
                                                    Name = oi.Item.Name,
                                                    Price = oi.Item.Price,
                                                    Quantity = oi.Quantity
                                                }).ToArray(),
                                    TotalPrice = o.OrderItems.Sum(oi => oi.Item.Price * oi.Quantity)
                                })
                                .OrderByDescending(o => o.TotalPrice)
                                .OrderByDescending(o => o.Items.Length)
                                .ToArray()

                })
                .ToArray();


            string jsonString = JsonConvert.SerializeObject(orderDtos, Newtonsoft.Json.Formatting.Indented);

            return jsonString;
        }

        public static string ExportCategoryStatistics(FastFoodDbContext context, string categoriesString)
        {
            var categories = categoriesString.Split(',');

            var categoryStatistics = context.Items
                .Where(i => categories.Any(c => c == i.Category.Name))
                .GroupBy(i => i.Category.Name)
                .Select(g => new CategoryExportDto
                {
                    Name = g.Key,
                    MostPopularItem = g.Select(i => new CategoryItemExportDto
                    {
                        Name = i.Name,
                        TotalMade = i.OrderItems.Sum(oi => oi.Quantity * oi.Item.Price),
                        TimesSold = i.OrderItems.Sum(oi => oi.Quantity)
                    })
                        .OrderByDescending(i => i.TotalMade)
                        .ThenByDescending(i => i.TimesSold)
                        .First()
                })
                .OrderByDescending(dto => dto.MostPopularItem.TotalMade)
                .ThenByDescending(dto => dto.MostPopularItem.TimesSold)
                .ToArray();

            var serializer = new XmlSerializer(typeof(CategoryExportDto[]), new XmlRootAttribute("Categories"));

            var sb = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });
            serializer.Serialize(new StringWriter(sb), categoryStatistics, namespaces);

            var result = sb.ToString();

            return result;
        }
    }
}