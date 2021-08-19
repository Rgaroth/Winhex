using Microsoft.AspNetCore.Mvc;
using WinhexWebServer.Interfaces;
using WinhexWebServer.Models;

namespace WinhexWebServer.Controllers
{
    [Route("upload")]
    [ApiController]
    public class UploadFileController : ControllerBase, IUploadFileController
    {
        private readonly ILogManager _logManager;
        public UploadFileController(ILogManager logManager)
        {
            _logManager = logManager;
        }
        [HttpPost]
        public bool Post(UserLog file)
        {
            return _logManager.AddUserLog(file); 
        }
    }
}
