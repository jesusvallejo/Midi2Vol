using IWshRuntimeLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Midi2Vol
{
    public class Sett
    {

        public Sett() { }
        public Sett(bool bootStartUp, bool notifyStartUp, bool notifyApp)
        {
            this.bootStartUp = bootStartUp;
            this.notifyStartUp = notifyStartUp;
            this.notifyApp = notifyApp;
        }
        public bool bootStartUp { get; set; }
        public bool notifyStartUp { get; set; }
        public bool notifyApp { get; set; }
    }


}
