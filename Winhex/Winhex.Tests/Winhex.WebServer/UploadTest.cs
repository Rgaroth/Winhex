using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using WinhexWebServer.Controllers;
using WinhexWebServer.Interfaces;
using Xunit;

namespace Winhex.Tests.Winhex.WebServer
{
    public class UploadTest
    {
        [Fact]
        public void UploadFile()
        {
            UploadFileController controller = new UploadFileController(new );
            Assert.Equal(controller.Post(new WinhexWebServer.Models.UserLog() { CompName = "TestCompName", CustomNote = "TestNote", Logs = new List<WinhexWebServer.Models.UserAction>() }), new OkResult());
        }
    }
}
