using System;

namespace SampleAPI.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public int ProductCategoryId { get; set; }

        public string CategoryDescr { get; set; }

        public bool IsShippable { get; set; }

        public bool IsVisible { get; set; }

        public decimal Price { get; set; }

        public string ProductName { get; set; }

        public string ProductDescr { get; set; }
    }
}