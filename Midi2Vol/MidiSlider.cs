using Microsoft.Win32;
using NAudio.Midi;
using System;
using System.Diagnostics;
using System.Threading;

using CSCore.CoreAudioAPI;
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
        private float volume;
        private int nanoID = -1;
        private int contVal = -1; // controller value to change different programs volume
        private int oldContVal = -1;
        private int potVal = -1;// potentiometer resistence value
        private int oldPotVal = -1; // value to check with the old value
        private SimpleAudioVolume appEnd = null; // current app audio source
        private const int defaultOutputSink = 62;
        private const int  defaultInputSink = 63;
        bool showed = false;
        private List<App> apps;
        private Sett settings;
        TrayApplicationContext nanoSliderTray;
        public MidiSlider(Sett settings, List<App> apps, TrayApplicationContext nanoSliderTray)
        {
            this.settings = settings;
            this.apps = apps;
            this.nanoSliderTray = nanoSliderTray;
        }


        public void Slider()
        {
            try
            {
                
                using (var enumerator = new CSCore.CoreAudioAPI.MMDeviceEnumerator())
                {
                    while (true)
                    {

                        if (contVal != oldContVal) {
                            // update value
                            oldContVal = contVal;
                            if (appEnd != null) {
                                appEnd.Dispose();
                            }
                            appEnd = getProcessAudioEndpoint(enumerator);
                            // notify editing volume on another app
                            if (settings.notifyApp) { 
                                nanoSliderTray.appVolume(getApp());
                            }
                        }
                        if (potVal != oldPotVal && (potVal > oldPotVal + 1 || potVal < oldPotVal - 1)) // prevents ghost slides
                        {
                            oldPotVal = potVal;
                            volume = (float)(Math.Floor((potVal / 3 * 2.395)) / 100);
                            nanoSliderTray.DynamicIcon(volume);
                            
                            if (contVal == defaultOutputSink )
                            {
                                ChangeOutputVolume(volume, enumerator);
                            }
                            else if (contVal == defaultInputSink)
                            {
                                ChangeInputVolume(volume, enumerator);
                            }
                            else
                            {
                                ChangeAppVolume(volume);
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
            catch (Exception k)
            {
                Console.WriteLine(k);
            }

        }


        void ChangeOutputVolume(float volume, MMDeviceEnumerator enumerator)
        {
            using (var device = enumerator.GetDefaultAudioEndpoint(CSCore.CoreAudioAPI.DataFlow.Render, CSCore.CoreAudioAPI.Role.Multimedia))
            using (var endpointVolume = CSCore.CoreAudioAPI.AudioEndpointVolume.FromDevice(device))
            {
                endpointVolume.SetMasterVolumeLevelScalar(volume, Guid.Empty);
            }
        }

        public float getVolume() {
            return volume;
        }
        void ChangeInputVolume(float volume, MMDeviceEnumerator enumerator)
        {
            using (var device = enumerator.GetDefaultAudioEndpoint(CSCore.CoreAudioAPI.DataFlow.Capture, CSCore.CoreAudioAPI.Role.Multimedia))
            using (var endpointVolume = CSCore.CoreAudioAPI.AudioEndpointVolume.FromDevice(device))
            {
                endpointVolume.SetMasterVolumeLevelScalar(volume, Guid.Empty);
            }
        }

        App getApp() {
            foreach (var app in apps)  // mb change this with find or smt
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
                        return app;
                    }
                }
            }
            return null; 
        }

       
         
        SimpleAudioVolume  getProcessAudioEndpoint(MMDeviceEnumerator enumerator) {
            String target = getApp().ProcessName;
            Process process;
            using (var sessionManager = GetDefaultAudioSessionManager2(enumerator, CSCore.CoreAudioAPI.DataFlow.Render))
            {
                using (var sessionEnumerator = sessionManager.GetSessionEnumerator())
                {
                    foreach (var session in sessionEnumerator)
                    {
                        using (var session2 = session.QueryInterface<AudioSessionControl2>()) // get process ID , getName doesnt work with many 
                        {
                            process = Process.GetProcessById(session2.ProcessID);
                            Debug.WriteLine(process);
                            //if (process.ProcessName == target && target != null)
                           // {
                            //    return session.QueryInterface<SimpleAudioVolume>();
                            //}
                        }
                    }
                    Console.WriteLine("app not found: "+ target);
                    return null;
                }
            }
        }
        

        void ChangeAppVolume(float volume) // refactor to cache the current app and offload the process
        {
            if (appEnd != null) {
                appEnd.MasterVolume = volume;
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
                        if (settings.notifyStatus)
                        {
                            nanoSliderTray.ReadyBaloon();
                        }
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
    
}
