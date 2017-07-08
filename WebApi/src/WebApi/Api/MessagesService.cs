using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Api
{
    public class MessagesService    {
        private ApiDbContext __context;

        public MessagesService(ApiDbContext context)
        {
            this.__context = context;
        }

        public async Task<List<Message>> GetAllMessages(CancellationToken token)
        {
            return await __context.Messages.ToListAsync(token).ConfigureAwait(false);
        }

        public async Task<Message> GetMessageById(Guid id, CancellationToken token)
        {
            return await __context.Messages.FirstOrDefaultAsync(__message => __message.Id == id, token).ConfigureAwait(false);
        }

        public async Task<Message> AddResponse(Guid id, Message response, CancellationToken token)
        {

            response.CreateDate = DateTimeOffset.UtcNow;
            response.Question = await __context.Messages.SingleAsync(__message => __message.Id == id, token).ConfigureAwait(false);

            await Add(response, token);

            return response;

        }

        public async Task<Message> AddQuestion(Message question, CancellationToken token)
        {
            question.CreateDate = DateTimeOffset.UtcNow;
            await Add(question, token);
            return question;
        }

        public async Task<List<Comment>> GetAllCommentsByMessageId(Guid id, CancellationToken token)
        {
            var message = await __context.Messages.FirstOrDefaultAsync(__message => __message.Id == id, token).ConfigureAwait(false);

            var comments = (from comment in __context.Comments
                           where comment.MessageId == id
                           select comment).ToList();

            return comments;
        }

        public async Task Add(Message response, CancellationToken token)
        {
            await __context.AddAsync(response, token).ConfigureAwait(false);
            await SaveChangesAsync(response, token);
        }


        private async Task SaveChangesAsync(Message message, CancellationToken token)
        {
            if (message.Text.Contains("Bad word"))
            {
                // произойдет тоже, что и если бы мы сохранили id == null
                message.Text = null;
                await __context.AddAsync(message, token).ConfigureAwait(false);
                await __context.SaveChangesAsync(token).ConfigureAwait(false);
            }
            else
            {
                await __context.SaveChangesAsync(token).ConfigureAwait(false);
            }
        }
    }
}
