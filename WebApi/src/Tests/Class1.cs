
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using WebApi.Models;

namespace Tests
{
    public class FirstTest
    {
        [Test]
        public void PassingTest() { Assert.AreEqual(2, 1 << 1); }

        [Test]
        public void FailingTest() { Assert.AreEqual(2, 1 >> 1); }

        [Test]
        public void DbContext()
        {
            var data = new List<Message>
            {
                new Message
                {
                    Id = Guid.NewGuid(),
                    CreateDate = DateTimeOffset.UtcNow,
                    Text = Guid.NewGuid().ToString()
                },
                new Message
                {
                    Id = Guid.NewGuid(),
                   // CreateDate = DateTimeOffset.UtcNow,
                   // Text = Guid.NewGuid().ToString()
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Message>>();
            mockSet.As<IQueryable<Message>>().Setup(__messages => __messages.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Message>>().Setup(__messages => __messages.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Message>>().Setup(__messages => __messages.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Message>>().Setup(__messages => __messages.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApiDbContext>();
            mockContext.Setup(c => c.Messages).Returns(mockSet.Object);

            Assert.AreEqual(2, mockContext.Object.Messages.Count());
        }

        [Test]
        public void BadWordsByAddMessage()
        {
            var data = new List<Message>();
            var queryableData = data.AsQueryable();

            var mockSet = new Mock<DbSet<Message>>();
            mockSet.As<IQueryable<Message>>().Setup(__messages => __messages.Provider).Returns(queryableData.Provider);
            mockSet.As<IQueryable<Message>>().Setup(__messages => __messages.Expression).Returns(queryableData.Expression);
            mockSet.As<IQueryable<Message>>().Setup(__messages => __messages.ElementType).Returns(queryableData.ElementType);
            mockSet.As<IQueryable<Message>>().Setup(__messages => __messages.GetEnumerator()).Returns(() => data.GetEnumerator());
            mockSet.Setup(__messages => __messages.Add(It.IsAny<Message>())).Callback<Message>((__message) => data.Add(__message));

            var mockContext = new Mock<ApiDbContext>();
            mockContext.Setup(c => c.Messages).Returns(mockSet.Object);

            mockSet.Object.Add(new Message
            {
                Id = Guid.NewGuid(),
                // CreateDate = DateTimeOffset.UtcNow,
                Text = null
            });

            mockSet.Object.Add(new Message
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTimeOffset.UtcNow,
                Text = "Bad word"
            });
            /*
            MessagesService messagesService = new messagesService(mockContext);
            messagesService.Add(new Message
            {
                Id = Guid.NewGuid(),
                // CreateDate = DateTimeOffset.UtcNow,
                Text = null
            })
            messagesService.Add(new Message
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTimeOffset.UtcNow,
                Text = "Bad word"
            })

            // такое слово не должно добавляться в контекст
            Assert.AreEqual(0, messagesService.mockContext.Object.Messages.Count());
            */
            
            // такое слово не должно добавляться в контекст
            Assert.AreEqual(0, mockContext.Object.Messages.Count());

        }


    }
}
