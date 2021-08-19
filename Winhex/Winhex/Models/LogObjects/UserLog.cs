using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Threading.Tasks;

namespace Winhex.Models
{
    /// <summary>
    /// Объект пользователя
    /// </summary>
    [Serializable]
    public class UserLog
    {
        public int Id { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string CompName { get; set; }
        /// <summary>
        /// Кастомное примечание
        /// </summary>
        public string CustomNote { get; set; }
        public List<UserAction> Logs { get; set; }

        public UserLog()
        {
            Logs = new List<UserAction>();
            CustomNote = "";

            string serial = "";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
            foreach (ManagementObject hdd in searcher.Get())
            {
                try
                {
                    serial = hdd["SerialNumber"].ToString().Trim();
                }
                catch { }
            }
            CompName = Environment.UserName + " - " + serial;

           //CompName = "Asus - 4231-1241-15232";
        }
    }
}
