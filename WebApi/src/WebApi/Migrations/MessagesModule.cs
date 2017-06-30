using System;
using Microsoft.EntityFrameworkCore;
using Nancy.ModelBinding;
using WebApi.Models;
using System.Linq;

namespace WebApi.Api
{
	public sealed class MessagesModule : ApiModuleBase
	{
		public MessagesModule(ApiDbContext context) : base("/messages")
		{
			Get("/", name: "GetAllMessages", action: async (__, __token) =>
			{
				var messages = await context.Messages.ToListAsync(__token).ConfigureAwait(false);

				return messages;
			});

			Get("/{id}", name: "GetMessageById", action: async (__params, __token) =>
			{
				Guid id = __params.Id;

				var message = await context.Messages.FirstOrDefaultAsync(__message => __message.Id == id, __token).ConfigureAwait(false);

				return message;
			});

			Post("/{questionId}/", name: "AddResponse", action: async (__params, __token) =>
			{
				Guid id = __params.questionId;
				Message response = this.Bind();

				response.CreateDate = DateTimeOffset.UtcNow;
				response.Question = await context.Messages.SingleAsync(__message => __message.Id == id, __token).ConfigureAwait(false);

				await context.AddAsync(response, __token).ConfigureAwait(false);

				await context.SaveChangesAsync(__token).ConfigureAwait(false);

				return response;
			});

			Post("/", name: "AddQuestion", action: async (__, __token) =>
			{
				Message question = this.Bind();

				question.CreateDate = DateTimeOffset.UtcNow;

				await context.AddAsync(question, __token).ConfigureAwait(false);

				await context.SaveChangesAsync(__token).ConfigureAwait(false);

				return question;
			});


            // метод получения всех коментариев к сообщению
            Get("/{id}/comments", name: "GetAllCommentsByMessageId", action: async (__params, __token) =>
            {
                Guid id = __params.id;

                var message = await context.Messages.FirstOrDefaultAsync(__message => __message.Id == id, __token).ConfigureAwait(false);

                var comments = from comment in context.Comments
                               where comment.MessageId == id
                               select comment;

                return comments;
            });
        }
    }
}