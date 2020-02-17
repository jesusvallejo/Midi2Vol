//deprecated was too slow
//using AudioSwitcher.AudioApi.CoreAudio; 
using NAudio.CoreAudioApi;
using Microsoft.Win32;
using NAudio.Midi;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Midi2Vol
{
    public class MidiSlider
    {
        private MidiIn midiIn;
        private int nanoID = -1;
        private int potVal = -1;// potentiometer resistence value
        private int oldPotVal = -1; // value to check with the old value
        bool showed = false;
        TrayApplicationContext nanoSliderTray ;
        public MidiSlider() {

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
                //deprecated was too slow
                //CoreAudioDevice defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;  /// sometimes null reference exception                   
                MMDeviceEnumerator devEnum = new MMDeviceEnumerator();
                MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                while (true)
                {
                    if (potVal != oldPotVal && (potVal > oldPotVal + 3 || potVal < oldPotVal - 3)) // prevents ghost slides
                    {
                        oldPotVal = potVal;
                        // deprecated //deprecated was too slow
                        //defaultPlaybackDevice.Volume = Math.Ceiling(potVal / 3 * 2.39); // transform 127 scale into 100 scale
                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = (float)(Math.Floor((potVal / 3 * 2.39)) / 100);                        
                    }
                    NanoFind();
                    Thread.Sleep(100);
                }        
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine(e.StackTrace);
                // deprecated //deprecated was too slow
                //CoreAudioDevice defaultCaptureDevice = new CoreAudioController().DefaultCaptureDevice;
                //defaultCaptureDevice.Volume = 100; ///seems to solve the bug, 
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

        private int NanoFind()
        {
            for (int device = 0; device < MidiIn.NumberOfDevices; device++)
            {
                if ((MidiIn.DeviceInfo(device).ProductId == 65535))//checks that nano slider is present
                {
                    if (nanoID != device)
                    {
                        
                        nanoID = device;
                        midiIn = new MidiIn(nanoID);
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
                potVal = cce.ControllerValue;
            }
        }



    }
}
