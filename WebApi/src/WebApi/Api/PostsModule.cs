using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Api
{
    public class PostsModule : ApiModuleBase
    {
        public PostsModule(PostsService Postservice) : base("/posts")
        {
            // получаем пост по id
            Get("/{id}", name: "GetPostById", action: async (__params, __token) =>
            {
                this.RequiresAuthentication();

                Guid id = __params.Id;

                return Postservice.GetPostByIdAsync(id, __token);
            });

            Post("/", name: "AddPost", action: async (__, __token) =>
            {
                this.RequiresAuthentication();

                Post post = this.Bind();

                return Postservice.AddAsync(post, __token);
            });

            Put("/", name: "UpdatePost", action: async (__, __token) =>
            {
                this.RequiresAuthentication();
                this.RequiresClaims(c => c.Type == "admin");

                Post post = this.Bind();

                return Postservice.UpdateAsync(post, __token);
            });

            Delete("/{id}", name: "DeletePost", action: async (__params, __token) =>
            {
                this.RequiresAuthentication();
                this.RequiresClaims(c => c.Type == "admin");

                Guid id = __params.Id;

                return Postservice.RemoveAsync(id, __token);
            });
        }
    }
}
