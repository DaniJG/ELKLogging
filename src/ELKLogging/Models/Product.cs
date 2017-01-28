using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELKLogging.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        [Text(Name="name")]
        public string Name { get; set; }

        [Text(Name = "description")]
        public string Description { get; set; }

        [Keyword(Name = "tag")]
        public string[] Tags { get; set; }        
    }
}
