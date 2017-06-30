using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Comment : ModelId

    {
        public DateTimeOffset CreateDate { get; set; }

        public DateTimeOffset ChangeDate { get; set; }

        public string Text { get; set; }

        public string AuthorName { get; set; }

        public Guid? MessageId { get; set; }

        public Message Message { get; set; }
    }
}

