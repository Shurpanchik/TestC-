﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Api;
using WebApi.Models;

namespace WebApi.Services
{
    //https://metanit.com/sharp/entityframeworkcore/3.3.php
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

        public async Task <Topic> AddAsync(Topic response, CancellationToken token)
        {
            if (!TextService.IsBadText(response.Name))
            {
                await __context.AddAsync(response, token).ConfigureAwait(false);
                await SaveChangesAsync(token);
                return response;
            }
            else
            {
                return null;
            }
        }

        public async Task<Topic> UpdateAsync(Topic topic, CancellationToken token)
        {
            if (!TextService.IsBadText(topic.Name))
            {
                __context.Topics.Update(topic);
                await SaveChangesAsync(token);
                return topic;
            }
            else
            {
                return null;
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
