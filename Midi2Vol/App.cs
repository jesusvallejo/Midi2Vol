using IWshRuntimeLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Midi2Vol
{
    public class App
    {
        
        public App() { }
        public App(String name, String AppRaw, String ProcessName)
        {
            this.name = name;
            this.AppRaw = AppRaw;
            this.ProcessName = ProcessName;
        }
        public String name { get; set; }
        public String AppRaw { get; set; }
        public String ProcessName { get; set; }
    }


}
