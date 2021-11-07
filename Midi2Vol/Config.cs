using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Midi2Vol
{


class Config
    {

        
        private String appConfigDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Midi2Vol";
        private String appConfigFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Midi2Vol\\appConfig.json";
        private String defaultAppConfig = @"
    [
    {
      'name': 'Default',
      'AppRaw': '0x3E',
      'ProcessName': 'default'
    },
    {
      'name': 'Microphone',
      'AppRaw': '0x3F',
      'ProcessName': 'Microphone'
    },
    {
      'name': 'Spotify',
      'AppRaw': '0x40',
      'ProcessName': 'Spotify'
    },
    {
      'name': 'Discord',
      'AppRaw': '0x41',
      'ProcessName': 'Discord'
    },
    {
      'name': 'Google Chrome',
      'AppRaw': '0x42',
      'ProcessName': 'chrome'
    }
  ]";

        private String configDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Midi2Vol";
        private String configFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Midi2Vol\\config.json";
        private String defaultConfig = @"
        {
        'bootStartup': 'false',
        'NotifyStatus': 'false',
        'NotifyApp': 'false',
        'trayBarIcon': 'Default'
        }";


        public Config() { 
        
        }

        public void removeSett() {
            File.Delete(configFile);
        }
        public void removeAppConfig() {
            File.Delete(appConfigFile);
        }
        public Sett SourceSettings() {
            if (!Directory.Exists(configDir))
            { // User probably removed the config directory, creat it again
                Directory.CreateDirectory(configDir);
            }
            if (!File.Exists(configFile))
            { // No config file detected, recreate it
                try
                {
                    File.Create(configFile).Dispose(); // create file
                }
                catch
                {
                    Debug.WriteLine("Could not create '" + configFile + "'.");
                }
                try
                {
                    System.IO.File.WriteAllText(configFile, defaultConfig); // fill with default config  
                }
                catch
                {
                    Debug.WriteLine("Could not write to '" + configFile + "'. Possibly in use");
                }
            }

            FileAttributes fileAttributes = File.GetAttributes(configFile);
            if ((fileAttributes & FileAttributes.ReadOnly) != FileAttributes.ReadOnly)
            {
                try
                {
                    Debug.WriteLine("Hello " + configFile + "'.");
                    String jsonString = System.IO.File.ReadAllText(configFile);
                    //probably should validate its a json, and that it has the correct structure
                    Sett settings = JsonConvert.DeserializeObject<Sett>(jsonString);
                    if (settings == null)
                    {
                        return null;
                    }
                    return settings;
                }
                catch
                {
                    Debug.WriteLine("Empty File " + appConfigFile + "'.");
                }
            }
            return null;

        }


        public void saveConfig(Sett settings)
        {
            String objectJson = JsonConvert.SerializeObject(settings);
            FileAttributes fileAttributes = File.GetAttributes(configFile);
            if ((fileAttributes & FileAttributes.ReadOnly) != FileAttributes.ReadOnly)
            {
                try
                {
                    System.IO.File.WriteAllText(configFile, objectJson);
                }
                catch
                {
                    Debug.WriteLine("Could not get Stream for '" + configFile + "'. Possibly in use");
                }
            }
        }

       



        public List<App> SourceAppConfig()
        {
            if (!Directory.Exists(appConfigDir)){ // User probably removed the config directory, creat it again
                Directory.CreateDirectory(appConfigDir);
            }
            if (!File.Exists(appConfigFile)) { // No app config file detected, recreate it
                try
                {
                    File.Create(appConfigFile).Dispose(); // create file
                }
                catch
                {
                    Debug.WriteLine("Could not create '" + appConfigFile + "'.");
                }
                try
                { 
                    System.IO.File.WriteAllText(appConfigFile, defaultAppConfig); // fill with default config  
                }
                catch
                {
                    Debug.WriteLine("Could not write to '" + appConfigFile + "'. Possibly in use");
                }
            }

            FileAttributes fileAttributes = File.GetAttributes(appConfigFile);
            if ((fileAttributes & FileAttributes.ReadOnly) != FileAttributes.ReadOnly)
            {
                try
                {
                    Debug.WriteLine("Hello " + appConfigFile + "'.");
                    String jsonString = System.IO.File.ReadAllText(appConfigFile);
                    //probably should validate its a json, and that it has the correct structure
                    List<App> apps = JsonConvert.DeserializeObject<List<App>>(jsonString);
                    if (apps == null) {
                        
                        return null;
                    }
                    return apps;
                }
                catch {
                    Debug.WriteLine("Empty File " + appConfigFile + "'.");
                }
            }
            return null;
        }
        public void saveAppConfig(List<App> apps)
        {
            String objectJson = JsonConvert.SerializeObject(apps);
            FileAttributes fileAttributes = File.GetAttributes(appConfigFile);
            if ((fileAttributes & FileAttributes.ReadOnly) != FileAttributes.ReadOnly)
            {
                try
                {
                    System.IO.File.WriteAllText(appConfigFile, objectJson);
                }
                catch
                {
                    Debug.WriteLine("Could not get Stream for '" + appConfigFile + "'. Possibly in use");
                }
            }
        }
    }
}
