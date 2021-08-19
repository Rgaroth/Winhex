using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using WinhexWebServer.Interfaces;
using WinhexWebServer.Models;

namespace WinhexWebServer.Controllers
{
    [Route("download")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        private ILogManager _logManager;
        public DownloadController(ILogManager logManager)
        {
            _logManager = logManager;
        }
        [HttpGet("{id}/{key}")]
        public UserLog GetUserLog(int id, string key)
        {
            if (GetHash(key) == _logManager.UserKey)
                return _logManager.GetUserLog(x => x.Id == id) ?? new UserLog();
            return new UserLog();
        }
        [HttpGet]
        public UserLog[] GetUsers()
        {
            return _logManager.Users;
        }

        [HttpPost]
        public IActionResult SetCustomNote(UserLog note)
        {
            if (_logManager.SetNote(note.Id, note.CustomNote)) return Ok();
            return BadRequest();
        }

        private static string GetHash(string str)
        {
            byte[] hash = Encoding.ASCII.GetBytes(str);
            byte[] hashenc = new MD5CryptoServiceProvider().ComputeHash(hash);
            string result = "";
            foreach (var b in hashenc)
            {
                result += b.ToString("x2");
            }
            return result;
        }
    }
}
