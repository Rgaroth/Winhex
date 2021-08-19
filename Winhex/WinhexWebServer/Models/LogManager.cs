using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WinhexWebServer.Interfaces;

namespace WinhexWebServer.Models
{
    public class LogManager : ILogManager
    {
        private Context db;

        public LogManager(Context c)
        {
            db = c;
        }


        public bool AddUserLog(UserLog log)
        {
            try
            {
                var userLog = db.UserLog.FirstOrDefault(x => x.CompName == log.CompName);

                if (userLog == null)
                    db.UserLog.Add(log);
                else
                    userLog.Logs.AddRange(log.Logs);

                db.SaveChanges();
               
            }
            catch (Exception ex)
            {
                //todo loging
                return false;
            }
            return true;
        }

        public UserLog[] Users => db.UserLog.ToArray();
        public string UserKey => db.Settings.FirstOrDefault(x => x.ParameterName == "UsersKey").ParameterValue;

        public UserLog GetUserLog(Expression<Func<UserLog, bool>> act)
        {
            return db.UserLog.Include(x => x.Logs).FirstOrDefault(act);
        }

        public bool SetNote(int id, string note)
        {
            var user = db.UserLog.FirstOrDefault(x => x.Id == id);
            if (user == null) return false;
            user.CustomNote = note;
            db.SaveChanges();
            return true;
        }
    }
}