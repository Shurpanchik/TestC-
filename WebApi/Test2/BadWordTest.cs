
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using WebApi.Models;
using WebApi.Api;
using System.Threading.Tasks;

namespace Test2
{
    class BadWordTest
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
            var mockContext = new Mock<ApiDbContext>();
            mockContext.Setup(m => m.SaveChanges()).Verifiable();   
            
            MessagesService messagesService = new MessagesService(mockContext.Object);
            messagesService.Add(new Message
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTimeOffset.UtcNow,
                Text = "Bad word"
            });

            //mockContext.Verify(m => m.Add(It.IsAny<Message>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Never);
        }
        [Test]
        public void NotBadWordsByAddMessage()
        {
            var mockContext = new Mock<ApiDbContext>();
            mockContext.Setup(m => m.SaveChanges()).Verifiable();

            MessagesService messagesService = new MessagesService(mockContext.Object);
            messagesService.Add(new Message
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTimeOffset.UtcNow,
                Text = "Hi"
            });

                //mockContext.Verify(m => m.Add(It.IsAny<Message>()), Times.Once);
                mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

    }
    }
