using Microsoft.EntityFrameworkCore;
using Nancy.ModelBinding;
using System;
using WebApi.Models;

namespace WebApi.Api
{
    public class CommentsModule : ApiModuleBase
    {
        public CommentsModule(ApiDbContext context) : base("/comment")
        {
            // получаем комментарий по id
            Get("/{id}", name: "GetCommentById", action: async (__params, __token) =>
            {
                Guid id = __params.Id;

                var comment = await context.Comments.FirstOrDefaultAsync(__comment => __comment.Id == id, __token).ConfigureAwait(false);

                return comment;
            });

            // добавление нового комментария
            Post("/{messageId}", name: "AddNewComment", action: async (__params, __token) =>
            {
                Guid id = __params.messageId;

                Comment comment = this.Bind();

                comment.CreateDate = DateTimeOffset.UtcNow;
                comment.ChangeDate = DateTimeOffset.UtcNow;

                await context.AddAsync(comment, __token).ConfigureAwait(false);

                await context.SaveChangesAsync(__token).ConfigureAwait(false);

                return comment;
            });

            // изменения комментария
            Post("/update", name: "UpdateNewComment", action: async (__, __token) =>
            {
                Comment comment = this.Bind();

                comment.ChangeDate = DateTimeOffset.UtcNow;

                context.Comments.Update(comment);

                await context.SaveChangesAsync(__token).ConfigureAwait(false);

                return comment;
            });

            // удаление комментария
            Delete("/{id}", name: "DeleteCommentById", action: async (__params, __token) =>
            {
                Guid id = __params.Id;

                var comment = await context.Comments.FirstOrDefaultAsync(__comment => __comment.Id == id, __token).ConfigureAwait(false);

                context.Remove(comment);

                await context.SaveChangesAsync(__token).ConfigureAwait(false);

                return comment;
            });

        }
    }
}
