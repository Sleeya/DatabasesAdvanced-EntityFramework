using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using FastFood.Data;
using FastFood.DataProcessor.Dto.Import;
using FastFood.Models;
using FastFood.Models.Enums;
using Newtonsoft.Json;

namespace FastFood.DataProcessor
{
    public static class Deserializer
    {
        private const string FailureMessage = "Invalid data format.";
        private const string SuccessMessage = "Record {0} successfully imported.";

        public static string ImportEmployees(FastFoodDbContext context, string jsonString)
        {
            var employeeDtos = JsonConvert.DeserializeObject<EmployeeImportDto[]>(jsonString);

            StringBuilder builder = new StringBuilder();

            List<Employee> employees = new List<Employee>();
            HashSet<Position> positionsToAdd = new HashSet<Position>();

            foreach (var eDto in employeeDtos)
            {
                if (!IsValid(eDto))
                {
                    builder.AppendLine(FailureMessage);
                    continue;
                }

                Position position = context.Positions.FirstOrDefault(p => p.Name == eDto.Position);
                position = positionsToAdd.FirstOrDefault(p => p.Name == eDto.Position);

                if (position == null)
                {
                    position = new Position
                    {
                        Name = eDto.Position
                    };

                    positionsToAdd.Add(position);
                }

                var employee = AutoMapper.Mapper.Map<Employee>(eDto);
                employee.Position = position;

                employees.Add(employee);

                builder.AppendLine(String.Format(SuccessMessage, employee.Name));
            }

            context.Positions.AddRange(positionsToAdd);
            context.Employees.AddRange(employees);
            context.SaveChanges();

            return builder.ToString().TrimEnd();
        }

        public static string ImportItems(FastFoodDbContext context, string jsonString)
        {
            var itemDtos = JsonConvert.DeserializeObject<ItemImportDto[]>(jsonString);

            StringBuilder builder = new StringBuilder();

            List<Category> categoriesToAdd = new List<Category>();
            List<Item> items = new List<Item>();

            foreach (var itemDto in itemDtos)
            {
                bool itemIsValid = IsValid(itemDto);
                if (!itemIsValid)
                {
                    builder.AppendLine(FailureMessage);
                    continue;
                }

                bool itemExistsInDb = context.Items.Any(i => i.Name == itemDto.Name);
                bool itemExistsInCurrentBatch = items.Any(i => i.Name == itemDto.Name);
                if (itemExistsInDb || itemExistsInCurrentBatch)
                {
                    builder.AppendLine(FailureMessage);
                    continue;
                }


                Category category = context.Categories.FirstOrDefault(c => c.Name == itemDto.Category);
                category = categoriesToAdd.FirstOrDefault(c => c.Name == itemDto.Category);

                if (category == null)
                {
                    category = new Category
                    {
                        Name = itemDto.Name
                    };

                    categoriesToAdd.Add(category);
                }

                var item = AutoMapper.Mapper.Map<Item>(itemDto);
                item.Category = category;

                items.Add(item);

                builder.AppendLine(String.Format(SuccessMessage, item.Name));
            }

            context.AddRange(categoriesToAdd);
            context.AddRange(items);
            context.SaveChanges();

            return builder.ToString().TrimEnd();
        }

        public static string ImportOrders(FastFoodDbContext context, string xmlString)
        {
            var xDoc = XDocument.Parse(xmlString);

            var elements = xDoc.Root.Elements();

            List<OrderImportDto> orderDtos = new List<OrderImportDto>();

            foreach (var el in elements)
            {
                orderDtos.Add(new OrderImportDto
                {
                    Customer = el.Element("Customer").Value,
                    Employee = el.Element("Employee").Value,
                    DateTime = DateTime.ParseExact(el.Element("DateTime").Value, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                    Type = Enum.Parse<OrderType>(el.Element("Type").Value),
                    Items = GetItems(el.Element("Items"))
                });
            }

            StringBuilder builder = new StringBuilder();
            List<Order> orders = new List<Order>();
            

            foreach (var orderDto in orderDtos)
            {
                HashSet<OrderItem> orderItems = new HashSet<OrderItem>();

                bool orderItemContainsInvalid = orderDto.Items.Any(oi => !IsValid(oi));
                bool itemDoesntExistsInDatabase = orderDto.Items.Any(oi => !context.Items.Any(i => i.Name == oi.Name));
                bool orderEmployeeExists = context.Employees.Any(e => e.Name == orderDto.Employee);

                if (!IsValid(orderDto) || itemDoesntExistsInDatabase || !orderEmployeeExists || orderItemContainsInvalid)
                {
                    builder.AppendLine(FailureMessage);
                    continue;
                }

                foreach (var orderItemDto in orderDto.Items)
                {
                    OrderItem orderItem = AutoMapper.Mapper.Map<OrderItem>(orderItemDto);
                    orderItem.Item = context.Items.First(i => i.Name == orderItemDto.Name);

                    orderItems.Add(orderItem);
                }

                Order order = AutoMapper.Mapper.Map<Order>(orderDto);
                order.OrderItems = orderItems;
                order.Employee = context.Employees.First(x=>x.Name == orderDto.Employee);

                orders.Add(order);

                builder.AppendLine($"Order for {order.Customer} on {order.DateTime.ToString("dd/MM/yyyy HH:mm")} added");
            }

            context.Orders.AddRange(orders);
            context.SaveChanges();

            return builder.ToString().TrimEnd();
        }

        private static ICollection<OrderItemImportDto> GetItems(XElement element)
        {
            var orderItems = new HashSet<OrderItemImportDto>();

            foreach (var el in element.Elements())
            {

                orderItems.Add(new OrderItemImportDto
                {
                    Name = el.Element("Name").Value,
                    Quantity = int.Parse(el.Element("Quantity").Value)
                });
            }

            return orderItems;
        }

        private static bool IsValid(object obj)
        {
            ValidationContext context = new ValidationContext(obj);

            List<ValidationResult> validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, context, validationResults, true);
        }
    }
}