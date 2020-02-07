using AudioSwitcher.AudioApi.CoreAudio;
using NAudio.Midi;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using Microsoft.Win32;
using Midi2Vol;
using System.Diagnostics;
using System.Reflection;

namespace Midi2Vol
{


    public class TrayApplicationContext : ApplicationContext
    {
        NotifyIcon _trayIcon = new NotifyIcon();
        public TrayApplicationContext()
        {

            if (MidiSlider.nanoFind() == -1) {
                nanoNotPresent();
            }
            ContextMenu _trayMenu = new ContextMenu { };
            _trayIcon.Icon = new System.Drawing.Icon("icon.ico");
            _trayIcon.Text = "Nano. Slider";
            _trayMenu.MenuItems.Add("E&xit", exit_Click);
            _trayIcon.ContextMenu = _trayMenu;
            _trayIcon.Visible = true;
        }


        public void nanoNotPresent()
        {
            const string message =
                "Nano. Slider not present.\nCheck if its connected or if  \"#define PRODUCT\" is set to:\nNano. Slider  ";
            const string caption = "Nano. Slider not found";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Error);
            // If the button was pressed ...
            if (result == DialogResult.OK)
            {
                // close the app.
                Environment.Exit(1);         // Kaboom!
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(1);         // Kaboom!
        }
    }
    public class startUp
    {
        private void SetStartup() //on developement
        {
            String AppName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            RegistryKey rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            // if (chkStartUp.Checked)
            // rk.SetValue(AppName, Application.ExecutablePath);
            // else
            //  rk.DeleteValue(AppName, false);

        }



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
                    //Make sure that the process is running from the exe file. 
                    if (Assembly.GetExecutingAssembly().Location.
                         Replace("/", "\\") == current.MainModule.FileName)

                    {
                        //Return the other process instance.  
                        return process;

                    }
                }
            }
            //No other instance was found, return null.  
            return null;
        }


        
    }
        



     public class MidiSlider
    {
         static int potVal = -1;// potentiometer resistence value
         static int oldPotVal = -1; // value to check with the old value
        static public  void midiSlider()
        {
            CoreAudioDevice defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;

            int nano = nanoFind();
            if (nano != -1)
            {
                MidiIn midiIn = new MidiIn(nano);
                midiIn.MessageReceived += midiIn_MessageReceived;
              
                midiIn.Start();
                while (true)
                {
                    if (potVal != oldPotVal && (potVal > oldPotVal + 3 || potVal < oldPotVal - 3) && potVal < 127) // prevents ghost slides
                    {
                        oldPotVal = potVal;
                        defaultPlaybackDevice.Volume = potVal / 3 * 2.380; // transform 127 scale into 100 scale
                    }
                    Thread.Sleep(100);
                }

            }

           
        }

        public static int nanoFind() {
            int nano = -1;
            for (int device = 0; device < MidiIn.NumberOfDevices; device++)
            {
                if (MidiIn.DeviceInfo(device).ProductName == "Nano. Slider")//checks that nano slider is present
                {
                    nano = device;
                    return nano;
                }
            }
            return nano;
        }

        static void midiIn_MessageReceived(object sender, MidiInMessageEventArgs e)
        {
            MidiEvent me = e.MidiEvent;
            ControlChangeEvent cce = me as ControlChangeEvent;
            if (cce != null)
            {
                potVal = cce.ControllerValue; 
            }
        }


        
    }

    class Program {

        [System.STAThread]
        static void Main()
        {
            if (startUp.RunningInstance() != null)
            {
                MessageBox.Show("Midi2Vol is already runing");
                Environment.Exit(1);
            }
            Task.Run(() => MidiSlider.midiSlider());
            Application.Run(new TrayApplicationContext());

        }




    }


}


