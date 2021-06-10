using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace Midi2Vol
{
    class Config
    {
        public String configFile = "config.json";
        public Config() { }
        public List<App> SourceConfig()
        {
            String jsonString = System.IO.File.ReadAllText(configFile);
            List<App> apps = JsonConvert.DeserializeObject<List<App>>(jsonString);
            return apps;
        }
        public void saveConfig(List<App> apps)
        {
            String objectJson = JsonConvert.SerializeObject(apps);
            System.IO.File.WriteAllText(configFile, objectJson);


        }
    }
}
