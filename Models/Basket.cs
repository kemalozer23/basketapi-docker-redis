using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BasketAPI.Models
{
    public class Basket
    {
        [Required]
        public string Id { get; set; } = $"basket:{Guid.NewGuid().ToString()}";

        [Required]
        public string Name { get; set; } = String.Empty;
    }

    public class Product
    {
        public string? Id { get; set; }
        public string? Code { get; set; }
        public string? ImageUrl { get; set; }
        public string? Name { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class BasketItem
    {
        public string Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}