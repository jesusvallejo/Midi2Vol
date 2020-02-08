
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using Microsoft.Win32;
using System.Diagnostics;
using System.Reflection;

namespace Midi2Vol
{
    class Program {

        [System.STAThread]
        static void Main()
        {
            if (StartUp.RunningInstance() != null)//check is not already runing before start
            {
                const string message =
                "Midi2Vol is already runing ";
                const string caption = "Nano. Slider";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Error);
                Environment.Exit(1);
            }
            Task.Run(() => MidiSlider.Slider());
            Application.Run(new TrayApplicationContext());

        }




    }


}


