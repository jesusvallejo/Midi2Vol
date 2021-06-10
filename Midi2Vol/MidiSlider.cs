using Microsoft.Win32;
using NAudio.Midi;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSCore.CoreAudioAPI;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

/// <summary>
/// Add support for different apps volume control
/// Try catch in case the midi is already in use
/// Change controler value to Decimal: 16-19	Hex: 10-13h	 Controller Name: General Purpose Controllers (Nos. 1-4), midiSlider linux too
/// </summary>

namespace Midi2Vol
{
    public class MidiSlider
    {
        private MidiIn midiIn;
        private int nanoID = -1;
        private int contVal = -1; // controller value to change different programs volume
        private int potVal = -1;// potentiometer resistence value
        private int oldPotVal = -1; // value to check with the old value
        bool showed = false;
        List<App> apps;
        TrayApplicationContext nanoSliderTray;
        public MidiSlider()
        {
            String jsonString = File.ReadAllText("config.json");
            apps = JsonConvert.DeserializeObject<List<App>>(jsonString);
            nanoSliderTray = new TrayApplicationContext();
            if (!nanoSliderTray.ProgramAlreadyRuning())
            {
                Task.Run(() => Slider());
                Application.Run(nanoSliderTray);//run everything before this line or wont be runned
            }
        }


        private void Slider()
        {
            try
            {
                float volume;
                using (var enumerator = new CSCore.CoreAudioAPI.MMDeviceEnumerator())
                {
                    while (true)
                    {
                        if (potVal != oldPotVal && (potVal > oldPotVal + 3 || potVal < oldPotVal - 3)) // prevents ghost slides
                        {
                            oldPotVal = potVal;
                            volume = (float)(Math.Floor((potVal / 3 * 2.39)) / 100);
                            if (contVal == 62)
                            {
                                ChangeAllVolume(volume, enumerator);
                            }
                            else
                            {
                                ChangeAppVolume(volume, enumerator);
                            }
                        }
                        if (nanoID == -1)
                        {
                            // if nano undetected poll slower
                            NanoFind();
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            NanoFind();
                            Thread.Sleep(100);
                        }
                    }
                }
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine(e.StackTrace);
            }

        }


        void ChangeAllVolume(float volume, MMDeviceEnumerator enumerator)
        {
            using (var device = enumerator.GetDefaultAudioEndpoint(CSCore.CoreAudioAPI.DataFlow.Render, CSCore.CoreAudioAPI.Role.Multimedia))
            using (var endpointVolume = CSCore.CoreAudioAPI.AudioEndpointVolume.FromDevice(device))
            {
                endpointVolume.SetMasterVolumeLevelScalar(volume, Guid.Empty);
            }
        }

        void ChangeAppVolume(float volume, MMDeviceEnumerator enumerator)
        {
            using (var sessionManager = GetDefaultAudioSessionManager2(enumerator, CSCore.CoreAudioAPI.DataFlow.Render))
            using (var device = enumerator.EnumAudioEndpoints(CSCore.CoreAudioAPI.DataFlow.Render, CSCore.CoreAudioAPI.DeviceState.Active))
            {
                using (var sessionEnumerator = sessionManager.GetSessionEnumerator())
                {
                    foreach (var session in sessionEnumerator)
                    {
                        using (var session2 = session.QueryInterface<AudioSessionControl2>()) // get process ID , getName doesnt work with a lot of applications
                        using (var simpleVolume = session.QueryInterface<CSCore.CoreAudioAPI.SimpleAudioVolume>())
                        {
                            String name = Process.GetProcessById(session2.ProcessID).ProcessName;
                            String target = null;
                            foreach (var app in apps)
                            {
                                if (app.AppRaw != null)
                                {
                                    int num;
                                    if (app.AppRaw.StartsWith("0x"))
                                    {
                                        num = Int32.Parse(app.AppRaw.Substring(2), System.Globalization.NumberStyles.HexNumber);
                                    }
                                    else
                                    {
                                        num = Int32.Parse(app.AppRaw, System.Globalization.NumberStyles.HexNumber);
                                    }
                                    if (num == contVal)
                                    {
                                        target = app.PulseName;
                                    }
                                }
                            }
                            if (name == target && target != null)
                            {
                                simpleVolume.MasterVolume = volume;
                            }
                        }
                    }
                }
            }
        }

        private CSCore.CoreAudioAPI.AudioSessionManager2 GetDefaultAudioSessionManager2(CSCore.CoreAudioAPI.MMDeviceEnumerator enumerator, CSCore.CoreAudioAPI.DataFlow dataFlow)
        {
            using (var device = enumerator.GetDefaultAudioEndpoint(dataFlow, CSCore.CoreAudioAPI.Role.Multimedia))
            {
                var sessionManager = CSCore.CoreAudioAPI.AudioSessionManager2.FromMMDevice(device);
                return sessionManager;
            }

        }

        private void OnPowerChange(object s, PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case PowerModes.Resume:
                    nanoID = -1;
                    NanoFind();
                    break;
            }
        }

        private int NanoFind() //polling is mandatory, naudio does not implement a watcher
        {
            for (int device = 0; device < MidiIn.NumberOfDevices; device++)
            {
                if ((MidiIn.DeviceInfo(device).ProductId == 65535))//checks that nano slider is present
                {
                    if (nanoID != device)
                    {
                        nanoID = device;
                        try
                        {
                            midiIn = new MidiIn(nanoID);
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e.Message);
                            nanoSliderTray.midiAlredyInUse();
                        }
                        midiIn.MessageReceived += MidiIn_MessageReceived;
                        SystemEvents.PowerModeChanged += OnPowerChange;
                        midiIn.Start();
                        nanoSliderTray.Ready();
                    }
                    showed = false;
                    return nanoID;
                }
            }
            nanoID = -1;
            showed = nanoSliderTray.NanoNotPresentMB(showed);
            return nanoID;
        }

        private void MidiIn_MessageReceived(object sender, MidiInMessageEventArgs e)
        {
            MidiEvent me = e.MidiEvent;
            ControlChangeEvent cce = me as ControlChangeEvent;
            if (cce != null)
            {
                contVal = ((int)cce.Controller);
                potVal = cce.ControllerValue;
            }
        }
    }
    public class App
    {
        public App() { }
        public App(String name, String AppRaw, String PulseName)
        {
            this.name = name;
            this.AppRaw = AppRaw;
            this.PulseName = PulseName;
        }
        public String name { get; set; }
        public String AppRaw { get; set; }
        public String PulseName { get; set; }
    }
}
