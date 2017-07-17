using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Api
{
    public class ForumsService
    {
        public ApiDbContext _context;

        public ForumsService(ApiDbContext context)
        {
            this._context = context;
        }

        public async Task<List<Forum>> GetAllForumsAsync(CancellationToken token)
        {
            return await _context.Forums
                            .Include(__forum =>__forum.Topics)
                            .ToListAsync(token).ConfigureAwait(false);
        }


        public async Task<Forum> GetForumByIdAsync(Guid id, CancellationToken token)
        {
            var forum = await _context.Forums
                .Include(__forum => __forum.Topics)
                .FirstOrDefaultAsync(__Forum => __Forum.Id == id , token).ConfigureAwait(false);

            return forum;
        }

        public async Task AddAsync(Forum response, CancellationToken token)
        {
            if (!TextService.IsBadText(response.Name))
            {
                await _context.AddAsync(response, token).ConfigureAwait(false);
                await SaveChangesAsync(token);
            }
        }

        public async Task UpdateAsync(Forum forum, CancellationToken token)
        {
            if (!TextService.IsBadText(forum.Name))
            {
                _context.Forums.Update(forum);
                await SaveChangesAsync(token);
            }
        }

        public async Task<Forum> RemoveAsync(Guid id, CancellationToken token)
        {

            var forum = await _context.Forums.FirstOrDefaultAsync(__comment => __comment.Id == id, token).ConfigureAwait(false);

            _context.Remove(forum);

            await SaveChangesAsync(token);

            return forum;

        }

        private async Task SaveChangesAsync(CancellationToken token)
        {
            await _context.SaveChangesAsync(token).ConfigureAwait(false);
        }

    }
}
