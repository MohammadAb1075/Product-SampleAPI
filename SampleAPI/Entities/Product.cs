using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleAPI.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
