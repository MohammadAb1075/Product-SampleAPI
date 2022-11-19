using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleAPI.Models
{
    public class Product
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Title { get; set; }

        [EnumDataType(typeof(TypeEnum))]
        [JsonConverter(typeof(StringEnumConverter))]
        public TypeEnum Type { get; set; }
        public double Price { get; set; }

        [EnumDataType(typeof(ColorEnum))]
        [JsonConverter(typeof(StringEnumConverter))]
        public ColorEnum Color { get; set; }
    }

}
