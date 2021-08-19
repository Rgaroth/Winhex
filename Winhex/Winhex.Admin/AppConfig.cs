using System.IO;
using Newtonsoft.Json;

namespace Winhex.Admin
{
    public class AppConfig
    {
        /// <summary>
        /// URL сервера, с которого будем забирать логи
        /// </summary>
        public string Url { get; set; }
        public string Key { get; set; }

        public static void SaveConfig(AppConfig conf)
        {
            File.WriteAllText("config.txt", JsonConvert.SerializeObject(conf));
        }

        public static AppConfig LoadConfig()
        {
            if (!File.Exists("config.txt"))
                File.WriteAllText("config.txt", JsonConvert.SerializeObject(new AppConfig(){ Url = "http://www.ihih.somee.com/", Key = ""}));
            return JsonConvert.DeserializeObject<AppConfig>(File.ReadAllText("config.txt"));
        }
    }
}