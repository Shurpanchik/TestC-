using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Api;

namespace Test2
{
    class TextServiceTests
    {
        [Test]
        public void BadWordTest()
        {
            Assert.AreEqual(true, TextService.IsBadText("Bad word"));
        }

        [Test]
        public void GoodWordTest()
        {
            Assert.AreEqual(false, TextService.IsBadText("Good word"));
        }
    }
}
