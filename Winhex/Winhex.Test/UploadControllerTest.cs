using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using WinhexWebServer.Controllers;
using WinhexWebServer.Interfaces;
using WinhexWebServer.Models;

namespace Winhex.Test
{
    public class UploadControllerests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddNullLogTest()
        {
            var mockRepo = new Mock<ILogManager>();
            mockRepo.Setup(x => x.AddUserLog(It.Is<UserLog>(t => t != null && t.Logs != null && t.Logs.Count > 0  && t.CompName != null && t.Logs.Any(a => a.TextLog != null && a.AppTitle != null)))).Returns(true);
            var c = new UploadFileController(mockRepo.Object);
            Assert.AreEqual(c.Post(null), false);
        }

        [Test]
        public void AddCorrectLogTest()
        {
            var mockRepo = new Mock<ILogManager>();
            mockRepo.Setup(x => x.AddUserLog(It.Is<UserLog>(t => t != null && t.Logs.Count > 0 && t.CompName != ""))).Returns(true);
            var c = new UploadFileController(mockRepo.Object);
            var log = new UserLog() { CompName = "Test", Logs = new System.Collections.Generic.List<UserAction>() };
            var action = new UserAction() { AppTitle = "TestTitle", TextLog = "test.test." };
            log.Logs.Add(action);
            Assert.AreEqual(c.Post(log), true);
        }

        [Test]
        public void AddNullActionsTest()
        {
            var mockRepo = new Mock<ILogManager>();
            mockRepo.Setup(x => x.AddUserLog(It.Is<UserLog>(t => t != null && t.Logs.Count > 0 && t.CompName != ""))).Returns(true);
            var c = new UploadFileController(mockRepo.Object);
            var log = new UserLog() { CompName = "Test", Logs = new System.Collections.Generic.List<UserAction>() };
           
            Assert.AreEqual(c.Post(log), false);
        }
    }
}