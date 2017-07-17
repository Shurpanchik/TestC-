using System;
using Microsoft.EntityFrameworkCore;
using Nancy.ModelBinding;
using WebApi.Models;
using System.Linq;

namespace WebApi.Api
{
    
	public sealed class MessagesModule : ApiModuleBase
	{
        public MessagesModule(MessagesService messagesService) : base("/messages")
        {
            Get("/", name: "GetAllMessages", action: async (__, __token) =>
            {
                var messages = await messagesService.GetAllMessagesAsync(__token);


                return messages;
            });

            Get("/{id}", name: "GetMessageById", action: async (__params, __token) =>
            {
                Guid id = __params.Id;

                var message = await messagesService.GetMessageByIdAsync(id, __token);

                return message;
            });

            Post("/{questionId}/", name: "AddResponse", action: async (__params, __token) =>
            {
                Guid id = __params.questionId;
                Message response = this.Bind();

                response = await messagesService.AddResponseAsync(id,response, __token);

                return response;
            });



            Post("/", name: "AddQuestion", action: async (__, __token) =>
            {
                Message question = this.Bind();

                question = await messagesService.AddQuestionAsync(question, __token);

                return question;
            });


            // метод получения списка комментариев
            Get("/{id}/comments", name: "GetAllCommentsByMessageId", action: async (__params, __token) =>
            {
                Guid id = __params.id;

                var comments = await messagesService.GetAllCommentsByMessageIdAsync(id, __token);

                return comments;
            });
         
        }   
    } 
}