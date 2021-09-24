using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Midi2Vol
{
    class Config
    {
        private String configDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Midi2Vol";
        private String configFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+"\\Midi2Vol\\config.json";
        private String defaultConfig =  "[{\"name\": \"Default\",\"AppRaw\": \"0x3E\",\"ProcessName\": \"default\"}]";


        public Config() { }
        public List<App> SourceConfig()
        {

            if (!Directory.Exists(configDir)){ // User probably removed the config directory, creat it again
                Directory.CreateDirectory(configDir);
            }
            
            if (!File.Exists(configFile)) { // No config file detected, recreate it

                try
                {
                    File.Create(configFile).Dispose(); // create file

                }
                catch
                {
                    Console.WriteLine("Could not create '" + configFile + "'.");
                }

                try
                {
                    
                    System.IO.File.WriteAllText(configFile, defaultConfig); // fill with default config
                    
                }
                catch
                {
                    Console.WriteLine("Could not write to '" + configFile + "'. Possibly in use");
                }
            }

            FileAttributes fileAttributes = File.GetAttributes(configFile);
            if ((fileAttributes & FileAttributes.ReadOnly) != FileAttributes.ReadOnly)
            {
                try
                {
                    Console.WriteLine("Hello " + configFile + "'.");
                    String jsonString = System.IO.File.ReadAllText(configFile);
                    //probably should validate its a json, and that it has the correct structure
                    List<App> apps = JsonConvert.DeserializeObject<List<App>>(jsonString);
                    return apps;
                }
                catch {
                    Console.WriteLine("Empty File " + configFile + "'.");
                }
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
