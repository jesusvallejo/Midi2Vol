using AudioSwitcher.AudioApi.CoreAudio;
using NAudio.Midi;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using Microsoft.Win32;

namespace Nano_Slider
{

    public class TrayApplicationContext : ApplicationContext
    {
        NotifyIcon _trayIcon = new NotifyIcon();
        public TrayApplicationContext()
        {

  
        ContextMenu _trayMenu = new ContextMenu { };
        _trayIcon.Icon = new System.Drawing.Icon("icon.ico"); 
            _trayIcon.Text = "Nano. Slider";
            _trayMenu.MenuItems.Add("E&xit", exit_Click);
            _trayIcon.ContextMenu = _trayMenu;
            _trayIcon.Visible = true;
        }

        private void exit_Click(object sender, EventArgs e)
        {
            _trayIcon.Visible = false;
            Application.Exit();
        }  
    }
    public class startUp {
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


    }

     public class MidiSlider
    {
         static int potVal = -1;// potentiometer resistence value
         static int oldPotVal = -1; // value to check with the old value
        static public  void midiSlider()
        {
            CoreAudioDevice defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;

            MidiIn midiIn = nanoFind();
            if (midiIn != null)
                {
                    midiIn.MessageReceived += midiIn_MessageReceived;
                    midiIn.Start();
                    while (true)
                    {
                        if (potVal != oldPotVal && (potVal > oldPotVal + 3 || potVal < oldPotVal - 3) && potVal<127) // prevents ghost slides
                        {
                            oldPotVal = potVal;
                            defaultPlaybackDevice.Volume = potVal / 3 * 2.380; // transform 127 scale into 100 scale
                        }
                        Thread.Sleep(100);
                    }
                
            }
        }


        static MidiIn nanoFind() {
            MidiIn nano = null;
            for (int device = 0; device < MidiIn.NumberOfDevices; device++)
            {
                if (MidiIn.DeviceInfo(device).ProductName == "Nano. Slider")
                {
                    nano = new MidiIn(device);
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
            Task.Run(() => MidiSlider.midiSlider());
            Application.Run(new TrayApplicationContext());

        }




    }


}


