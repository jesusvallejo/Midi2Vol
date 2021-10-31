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
        public Sett(bool bootStartUp, bool notifyStatus, bool notifyApp, String trayBarIcon )
        {
            this.bootStartUp = bootStartUp;
            this.notifyStatus = notifyStatus;
            this.notifyApp = notifyApp;
            this.trayBarIcon = trayBarIcon;
        }
        public bool bootStartUp { get; set; }
        public bool notifyStatus { get; set; }
        public bool notifyApp { get; set; }
        public String trayBarIcon { get; set; }


    }


}
