﻿
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Midi2Vol
{
    public class Program
    {
        [System.STAThread]
        static void Main()
        {
            String jsonString = File.ReadAllText("config.json");
            List<App> apps = JsonConvert.DeserializeObject<List<App>>(jsonString);
            MidiSlider nanoSlider = new MidiSlider(apps);              
        }
    }
}


