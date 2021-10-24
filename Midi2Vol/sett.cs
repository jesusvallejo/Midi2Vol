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
        public Sett(bool bootStartUp, bool notifyStatus, bool notifyApp)
        {
            this.bootStartUp = bootStartUp;
            this.notifyStatus = notifyStatus;
            this.notifyApp = notifyApp;
        }
        public bool bootStartUp { get; set; }
        public bool notifyStatus { get; set; }
        public bool notifyApp { get; set; }
    }


}
