using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Midi2Vol
{
    class Config
    {
        public String configFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+"/Midi2Vol/config.json";
        public Config() { }
        public List<App> SourceConfig()
        {
            FileAttributes fileAttributes = File.GetAttributes(configFile);
            if ((fileAttributes & FileAttributes.ReadOnly) != FileAttributes.ReadOnly)
            {
                String jsonString = System.IO.File.ReadAllText(configFile);
                List<App> apps = JsonConvert.DeserializeObject<List<App>>(jsonString);
                return apps;
            }
            return null;
        }
        public void saveConfig(List<App> apps)
        {
            String objectJson = JsonConvert.SerializeObject(apps);
            FileAttributes fileAttributes = File.GetAttributes(configFile);
            if ((fileAttributes & FileAttributes.ReadOnly) != FileAttributes.ReadOnly)
            {
                try
                {
                    System.IO.File.WriteAllText(configFile, objectJson);
                }
                catch
                {
                    Console.WriteLine("Could not get Stream for '" + configFile + "'. Possibly in use");
                }
            }
        }
    }
}
