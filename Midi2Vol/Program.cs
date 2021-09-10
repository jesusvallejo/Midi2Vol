
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
            Config config = new Config();
            List<App> apps = config.SourceConfig();
            if (apps != null)
            {
                MidiSlider nanoSlider = new MidiSlider(apps);
            }
        }
    }
}


