using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
	public class Message : ModelId
	{
		public DateTimeOffset CreateDate { get; set; }

        public string Text { get; set; }

		public Guid? QuestionId { get; set; }

		public Message Question { get; set; }
	}
}