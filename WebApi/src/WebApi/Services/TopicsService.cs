using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Api;
using WebApi.Models;

namespace WebApi.Services
{
    public class TopicsService
    {
        public ApiDbContext __context;

        public TopicsService(ApiDbContext context)
        {
            this.__context = context;
        }


        public async Task<Topic> GetTopicByIdAsync(Guid id, CancellationToken token)
        {
            var Topic = await __context.Topics
                .Include(__Topic => __Topic.Posts)
                .FirstOrDefaultAsync(__Topic => __Topic.Id == id, token).ConfigureAwait(false);

            return Topic;
        }

        public async Task AddAsync(Topic response, CancellationToken token)
        {
            if (!TextService.IsBadText(response.Name))
            {
                await __context.AddAsync(response, token).ConfigureAwait(false);
                await SaveChangesAsync(token);
            }
        }

        public async Task UpdateAsync(Topic Topic, CancellationToken token)
        {
            if (!TextService.IsBadText(Topic.Name))
            {
                __context.Topics.Update(Topic);
                await SaveChangesAsync(token);
            }
        }

        public async Task<Topic> RemoveAsync(Guid id, CancellationToken token)
        {

            var Topic = await __context.Topics.FirstOrDefaultAsync(__comment => __comment.Id == id, token).ConfigureAwait(false);

            __context.Remove(Topic);

            await SaveChangesAsync(token);

            return Topic;

        }

        private async Task SaveChangesAsync(CancellationToken token)
        {
            await __context.SaveChangesAsync(token).ConfigureAwait(false);
        }

    }
}
