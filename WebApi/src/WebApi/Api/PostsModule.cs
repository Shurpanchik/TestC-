using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Api
{
    public class PostsModule : ApiModuleBase
    {
        public PostsModule(ApiDbContext context) : base("/posts")
        {
            PostsService postservice = new PostsService(context);

            // получаем пост по id
            Get("/{id}", name: "GetPostById", action: async (__params, __token) =>
            {
                this.RequiresAuthentication();

                Guid id = __params.Id;

                return postservice.GetPostByIdAsync(id, __token);
            });

            Post("/", name: "AddPost", action: async (__, __token) =>
            {
                this.RequiresAuthentication();

                Post post = this.Bind();

                return postservice.AddAsync(post, __token);
            });

            Put("/", name: "UpdatePost", action: async (__, __token) =>
            {
                this.RequiresAuthentication();
                this.RequiresClaims(c => c.Type == ClaimTypes.Role && c.Value == "admin");

                Post post = this.Bind();

                return postservice.UpdateAsync(post, __token);
            });

            Delete("/{id}", name: "DeletePost", action: async (__params, __token) =>
            {
                this.RequiresAuthentication();
                this.RequiresClaims(c => c.Type == ClaimTypes.Role && c.Value == "admin");

                Guid id = __params.Id;

                return postservice.RemoveAsync(id, __token);
            });
        }
    }
}
