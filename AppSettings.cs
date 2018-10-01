using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace DiscordBot
{
    public class AppSettings
    {
        private static AppSettings instance;
        public static AppSettings Current => instance = instance ?? new AppSettings();

        private AppSettings() { }
        static AppSettings()
        {
            Current.Deserialize();
        }

        private readonly string filePath = "./appsettings.json";

        public string Token { get; set; }
        public char Prefix { get; set; }

        public void Serialize()
        {
            using (var w = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                var json = JsonConvert.SerializeObject(instance);
                w.Write(json);
            }
        }

        public void Deserialize()
        {
            using (var r = new StreamReader(filePath, Encoding.UTF8))
            {
                instance = JsonConvert.DeserializeObject<AppSettings>(r.ReadToEnd());
            }
        }
    }
}