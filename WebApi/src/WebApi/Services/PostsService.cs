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
    public class PostsService
    {
        public ApiDbContext __context;

        public PostsService(ApiDbContext context)
        {
            this.__context = context;
        }


        public async Task<Post> GetPostByIdAsync(Guid id, CancellationToken token)
        {
            var Post = await __context.Posts
                .FirstOrDefaultAsync(__Post => __Post.Id == id, token).ConfigureAwait(false);

            return Post;
        }

        public async Task AddAsync(Post response, CancellationToken token)
        {
            if (!TextService.IsBadText(response.Text))
            {
                await __context.AddAsync(response, token).ConfigureAwait(false);
                await SaveChangesAsync(token);
            }
        }

        public async Task UpdateAsync(Post Post, CancellationToken token)
        {
            if (!TextService.IsBadText(Post.Text))
            {
                __context.Posts.Update(Post);
                await SaveChangesAsync(token);
            }
        }

        public async Task<Post> RemoveAsync(Guid id, CancellationToken token)
        {

            var Post = await __context.Posts.FirstOrDefaultAsync(__comment => __comment.Id == id, token).ConfigureAwait(false);

            __context.Remove(Post);

            await SaveChangesAsync(token);

            return Post;

        }

        private async Task SaveChangesAsync(CancellationToken token)
        {
            await __context.SaveChangesAsync(token).ConfigureAwait(false);
        }
    }
}
