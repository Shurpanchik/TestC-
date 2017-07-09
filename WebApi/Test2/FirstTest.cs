
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
    class FirstTest
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
                    CreateDate = DateTimeOffset.UtcNow,
                   Text = Guid.NewGuid().ToString()
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
    }
}
