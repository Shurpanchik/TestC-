using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Comment : ModelId

    {
        [Required]
        public DateTimeOffset CreateDate { get; set; }

        public DateTimeOffset ChangeDate { get; set; }

        [NotBadWords]
        public string Text { get; set; }

        public string AuthorName { get; set; }

        public Guid? MessageId { get; set; }

        public Message Message { get; set; }
    }
}

