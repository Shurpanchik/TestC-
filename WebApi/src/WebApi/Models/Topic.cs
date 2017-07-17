using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Topic : ModelId
    {
        public string Name { get; set; }
        public Forum Forum { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
