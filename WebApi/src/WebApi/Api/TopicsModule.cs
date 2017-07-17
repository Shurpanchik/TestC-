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
    public class TopicsModule : ApiModuleBase
    {
        public TopicsModule(TopicsService Topicservice) : base("/topics")
        {

            // получаем топик со списком всех постов
            Get("/{id}", name: "GetTopicById", action: async (__params, __token) =>
            {
                this.RequiresAuthentication();

                Guid id = __params.Id;

                return Topicservice.GetTopicByIdAsync(id, __token);
            });

            Post("/", name: "AddTopic", action: async (__, __token) =>
            {
                this.RequiresAuthentication();

                Topic topic = this.Bind();

                return Topicservice.AddAsync(topic, __token);
            });

            Put("/", name: "UpdateTopic", action: async (__, __token) =>
            {
                this.RequiresAuthentication();
                this.RequiresClaims(c => c.Type == "admin");

                Topic topic = this.Bind();

                return Topicservice.UpdateAsync(topic, __token);
            });

            Delete("/{id}", name: "DeleteTopic", action: async (__params, __token) =>
            {
                this.RequiresAuthentication();
                this.RequiresClaims(c => c.Type == "admin");

                Guid id = __params.Id;

                return Topicservice.RemoveAsync(id, __token);
            });

        }
    }
}
