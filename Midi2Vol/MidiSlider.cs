using AudioSwitcher.AudioApi.CoreAudio;
using NAudio.Midi;

using System.Diagnostics;
using System.Threading;
using System;

namespace Midi2Vol
{
    public class MidiSlider
    {
         
         private int potVal = -1;// potentiometer resistence value
         private int oldPotVal = -1; // value to check with the old value

        
        public void Slider()
        {
            try
            {
                CoreAudioDevice defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;  /// sometimes null reference exception

            
            int nano = GetNanoID();
                if (GetNano())//check its present
                {
                    Debug.WriteLine("MidiSlider setting up");
                    MidiIn midiIn = new MidiIn(nano);
                    midiIn.MessageReceived += MidiIn_MessageReceived;
                    midiIn.Start();
                    while (true)
                    {
                        Debug.WriteLine("checking change");
                        if (potVal != oldPotVal && (potVal > oldPotVal + 3 || potVal < oldPotVal - 3)) // prevents ghost slides
                        {
                            Debug.WriteLine("volume update");
                            oldPotVal = potVal;
                            defaultPlaybackDevice.Volume = Math.Ceiling(potVal / 3 * 2.39); // transform 127 scale into 100 scale
                        }
                        Thread.Sleep(100);
                    }

                }
                else
                {
                    TrayApplicationContext closeEverithing = new TrayApplicationContext();//not present so closing

                    closeEverithing.NanoNotPresentMB();
                }
            }
            catch (NullReferenceException e)
            {

                Debug.WriteLine(e.StackTrace);
                CoreAudioDevice defaultCaptureDevice = new CoreAudioController().DefaultCaptureDevice;
                defaultCaptureDevice.Volume = 100; ///seems to solve the bug , 

            }


        }

        private int NanoFind()
        {
            int nano = -1;
            for (int device = 0; device < MidiIn.NumberOfDevices; device++)
            {
                if ((MidiIn.DeviceInfo(device).ProductId == 65535))//checks that nano slider is present
                {
                    nano = device;
                    return nano;
                }
            }
            return nano;
        }
        public int GetNanoID() {
            return NanoFind();
        }
        public bool GetNano()
        {
            if (NanoFind() == -1)
            {
                Debug.WriteLine("MidiSlider not Found");
                return false;
            }
            else
            {
                Debug.WriteLine("MidiSlider found");
                return true;
            }
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
