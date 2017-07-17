using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Api;
using WebApi.Models;

namespace Test2
{
    class MessageServiceTests
    {
        Mock<ApiDbContext> mockContext;

        public MessageServiceTests()
        {
            mockContext = new Mock<ApiDbContext>();
            mockContext.Setup(m => m.Add(It.IsAny<Message>())).Verifiable();

            mockContext.Setup(m => m.SaveChanges()).Verifiable();

        }

        [Test]
        public void AddMessageWithBadWords()
        {
            AddMessage("Bad word");

            mockContext.Verify(m => m.Add(It.IsAny<Message>()), Times.Never);
            mockContext.Verify(m => m.SaveChanges(), Times.Never);
        }
        [Test]
        public void AddMessageWithPositiveWords()
        {
            AddMessage("It is good day");

            //mockContext.Verify(m => m.Add(It.IsAny<Message>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }


        void AddMessage(string text)
        {
            MessagesService messagesService = new MessagesService(mockContext.Object);
            messagesService.Add(new Message
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTimeOffset.UtcNow,
                Text = text
            });
        }
    }
}
