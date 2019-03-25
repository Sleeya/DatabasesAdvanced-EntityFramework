using FastFood.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastFood.DataProcessor.Dto.Import
{
    //<Customer>Garry</Customer>
    //<Employee>Maxwell Shanahan</Employee>
    //<DateTime>21/08/2017 13:22</DateTime>
    //<Type>ForHere</Type>
    //<Items></Items>

    public class OrderImportDto
    {
        [Required]
        public string Customer { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Employee { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public OrderType Type { get; set; }

        [Required]
        public ICollection<OrderItemImportDto> Items { get; set; }
    }
}
