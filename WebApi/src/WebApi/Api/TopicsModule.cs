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
    public class TopicsModule : ApiModuleBase
    {
        public TopicsModule(ApiDbContext context) : base("/topics")
        {
            TopicsService topicservice = new TopicsService(context);

            // получаем топик со списком всех постов
            Get("/{id}", name: "GetTopicById", action: async (__params, __token) =>
            {
                this.RequiresAuthentication();

                Guid id = __params.Id;

                return topicservice.GetTopicByIdAsync(id, __token);
            });

            Post("/", name: "AddTopic", action: async (__, __token) =>
            {
                this.RequiresAuthentication();

                Topic topic = this.Bind();

                return topicservice.AddAsync(topic, __token);
            });

            Put("/", name: "UpdateTopic", action: async (__, __token) =>
            {
                this.RequiresAuthentication();
                this.RequiresClaims(c => c.Type == ClaimTypes.Role && c.Value == "admin");

                Topic topic = this.Bind();

                return topicservice.UpdateAsync(topic, __token);
            });

            Delete("/{id}", name: "DeleteTopic", action: async (__params, __token) =>
            {
                this.RequiresAuthentication();
                this.RequiresClaims(c => c.Type == ClaimTypes.Role && c.Value == "admin");

                Guid id = __params.Id;

                return topicservice.RemoveAsync(id, __token);
            });

        }
    }
}
