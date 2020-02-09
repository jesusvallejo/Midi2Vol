
using System.Threading.Tasks;
using System.Windows.Forms;
using System;


namespace Midi2Vol
{
    public class Program {

        public void ExitProgram()
        {
            Application.Exit();           // closed everything
            Environment.Exit(1);         // Kaboom!
        }


        [System.STAThread]
        static void Main()
        {
            TrayApplicationContext nanoSliderTray = new TrayApplicationContext();
            if (!nanoSliderTray.ProgramAlreadyRuning() && !nanoSliderTray.NanoNotPresentMB())
            {
                MidiSlider nanoSlider = new MidiSlider();
                Task.Run(() => nanoSlider.Slider());
                Application.Run(nanoSliderTray);
            }           
        }
    }
}


