using System.IO;
using Newtonsoft.Json;

namespace Winhex
{
    public class Config
    {
        private static string configFileName = @"config.conf";
        public SendingMode SendingMode { get; set; }

        public static void SaveConfig(Config conf)
        {
            File.WriteAllText(configFileName, JsonConvert.SerializeObject(conf));
        }

        public static Config LoadConfig()
        {
            if (!File.Exists(configFileName))
                File.WriteAllText(configFileName, JsonConvert.SerializeObject(
                    new Config { SendingMode = SendingMode.ToWebServer}));

            return JsonConvert.DeserializeObject<Config>(File.ReadAllText(configFileName));
        }
    }

    public enum SendingMode
    {
        ToWebServer, ToTgBot
    }
}