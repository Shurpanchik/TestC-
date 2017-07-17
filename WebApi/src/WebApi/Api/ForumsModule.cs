using Microsoft.EntityFrameworkCore;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Security.Claims;
using WebApi.Models;

namespace WebApi.Api
{
    public class ForumsModule : ApiModuleBase
    {

        public ForumsModule(ApiDbContext context) : base("/forums")
        {
            ForumsService forumService = new ForumsService(context);

            // получаем все форумы с вложениями
            Get("/", name: "GetAllForums", action: async (__params, __token) =>
            {
                this.RequiresAuthentication();

                return forumService.GetAllForumsAsync(__token);
            });

            // получаем форум с вложениями
            Get("/{id}", name: "GetForumById", action: async (__params, __token) =>
            {
                this.RequiresAuthentication();

                Guid id = __params.Id;

                return forumService.GetForumByIdAsync(id, __token);
            });

            Post("/", name: "AddForum", action: async (__, __token) =>
            {
                this.RequiresAuthentication();

                Forum forum = this.Bind();

                return forumService.AddAsync(forum, __token);
            });

            Put("/", name: "UpdateForum", action: async (__, __token) =>
            {
                this.RequiresAuthentication();
                this.RequiresClaims(c => c.Type == "admin");

                Forum forum = this.Bind();

                return forumService.UpdateAsync(forum, __token);
            });

            Delete("/{id}", name: "DeleteForum", action: async (__params, __token) =>
            {
                this.RequiresAuthentication();
                this.RequiresClaims(c => c.Type == "admin");

                Guid id = __params.Id;

                return forumService.RemoveAsync(id, __token);
            });

        }
    }
}
