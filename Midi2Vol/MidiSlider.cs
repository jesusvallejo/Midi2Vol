using AudioSwitcher.AudioApi.CoreAudio;
using NAudio.Midi;
using System.Threading;

namespace Midi2Vol
{
    public class MidiSlider
    {
        static int potVal = -1;// potentiometer resistence value
        static int oldPotVal = -1; // value to check with the old value
        static public void Slider()
        {
            CoreAudioDevice defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;

            int nano = NanoFind();
            if (nano != -1)//check its present
            {
                MidiIn midiIn = new MidiIn(nano);
                midiIn.MessageReceived += MidiIn_MessageReceived;

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
            else
            {
                TrayApplicationContext closeEverithing = new TrayApplicationContext();//not present so closing
                closeEverithing.NanoNotPresent();
            }


        }

        public static int NanoFind()
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

        static void MidiIn_MessageReceived(object sender, MidiInMessageEventArgs e)
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
