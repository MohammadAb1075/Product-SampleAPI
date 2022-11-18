using System;

namespace SampleAPI.Models
{
    public class Product
    {

        public Guid Id { get; set; }
        public string Title { get; set; }
        public TypeEnum Type { get; set; }
        public double Price { get; set; }
        public ColorEnum Color { get; set; }
    }
}
