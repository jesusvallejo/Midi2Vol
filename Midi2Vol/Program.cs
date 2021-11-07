
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Midi2Vol
{
    public class Program
    {
        [System.STAThread]

        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            //Loop through the running processes in with the same name 
            foreach (Process process in processes)
            {
                //Ignore the current process 
                if (process.Id != current.Id)
                {
                    //Return the other process instance.  
                    return process;
                }
            }
            //No other instance was found, return null.  
            return null;
        }
        static void Main()
        {
            Config config = new Config();
            Sett settings = config.SourceSettings();
            List<App> apps = config.SourceAppConfig();


            if (apps == null) { // error in json
                const string message = "Configuration Volume file error.";
                const string caption = "Midi2Vol";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                config.removeAppConfig();
                Application.Restart(); // restart to load new app configs
                Environment.Exit(0);
            }
            else if (settings == null) { //error in json
                const string message = "Configuration Setting file error.";
                const string caption = "Midi2Vol";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                config.removeSett();
                Application.Restart(); // restart to load new sett configs
                Environment.Exit(0);
            }

            else
            {
                TrayApplicationContext nanoSliderTray = new TrayApplicationContext(settings);
                MidiSlider nanoSlider = new MidiSlider(settings, apps, nanoSliderTray);

                if (RunningInstance() == null)
                {
                    Debug.WriteLine("lets run");
                    Task.Run(() => nanoSlider.Slider());
                    Application.Run(nanoSliderTray);//run everything before this line or wont be runned
                }
                else
                {
                    nanoSliderTray.ProgramAlreadyRuning();
                }
            }
            


        }
    }
}


