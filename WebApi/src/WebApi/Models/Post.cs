using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Post : ModelId
    {
        public string Text { get; set; }

        public Topic Topic { get; set; }
        public Guid TopicId { get; set; }

    }
}
