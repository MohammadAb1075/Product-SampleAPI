using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;

namespace SampleAPI.Models
{
    public class Product
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Title { get; set; }

        [EnumDataType(typeof(TypeEnum))]
        [JsonConverter(typeof(StringEnumConverter))]
        public TypeEnum Type { get; set; }
        public double Price { get; set; }
        public ColorEnum Color { get; set; }
    }
}
