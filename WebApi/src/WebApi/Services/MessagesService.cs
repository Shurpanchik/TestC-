using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Api
{
    public class MessagesService
    {
        public ApiDbContext __context;

        public MessagesService(ApiDbContext context)
        {
            this.__context = context;
        }

        public async Task<List<Message>> GetAllMessagesAsync(CancellationToken token)
        {
            return await __context.Messages.ToListAsync(token).ConfigureAwait(false);
        }

        public List<Message> GetAllMessages()
        {
            return __context.Messages.ToList();
        }

        public async Task<Message> GetMessageByIdAsync(Guid id, CancellationToken token)
        {
            return await __context.Messages.FirstOrDefaultAsync(__message => __message.Id == id, token).ConfigureAwait(false);
        }

        public async Task<Message> AddResponseAsync(Guid id, Message response, CancellationToken token)
        {

            response.CreateDate = DateTimeOffset.UtcNow;
            response.Question = await __context.Messages.SingleAsync(__message => __message.Id == id, token).ConfigureAwait(false);

            await AddAsync(response, token);

            return response;

        }

        public async Task<Message> AddQuestionAsync(Message question, CancellationToken token)
        {
            question.CreateDate = DateTimeOffset.UtcNow;
            await AddAsync(question, token);
            return question;
        }

        public async Task<List<Comment>> GetAllCommentsByMessageIdAsync(Guid id, CancellationToken token)
        {
            var message = await __context.Messages.FirstOrDefaultAsync(__message => __message.Id == id, token).ConfigureAwait(false);

            var comments = (from comment in __context.Comments
                            where comment.MessageId == id
                            select comment).ToList();

            return comments;
        }

        public async Task AddAsync(Message response, CancellationToken token)
        {
            if (!TextService.IsBadText(response.Text))
            {
                await __context.AddAsync(response, token).ConfigureAwait(false);
                await SaveChangesAsync(response, token);
            }
        }

        public void Add(Message response)
        {
             
            if (!TextService.IsBadText(response.Text))
            {
                __context.Add(response);
                SaveChanges(response);
            }
        }

        private async Task SaveChangesAsync(Message message, CancellationToken token)
        {
                await __context.SaveChangesAsync(token).ConfigureAwait(false);
        }


        private  void SaveChanges(Message message)
        {
                __context.SaveChanges();
        }

    }
}
