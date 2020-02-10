using System.Windows.Forms;
using System;
using System.Drawing;
using System.Diagnostics;
using System.IO;

namespace Midi2Vol
{
    public class TrayApplicationContext : ApplicationContext
    {
        NotifyIcon _trayIcon = new NotifyIcon();
        Program exit = new Program();
        MidiSlider slider = new MidiSlider();
        public TrayApplicationContext()
        {
            ContextMenu _trayMenu = new ContextMenu { };

            _trayIcon.Icon = Properties.Resources.NanoBento;
            _trayIcon.BalloonTipText = "Volume controled by Nano. Slider";
            _trayIcon.BalloonTipTitle = "Nano. Slider Win2Vol";
            _trayIcon.BalloonTipIcon = ToolTipIcon.Info;
            _trayIcon.Text = "Nano. Slider";
            _trayMenu.MenuItems.Add("Add/Remove Run on StartUp", ConfigClick);
            _trayMenu.MenuItems.Add("E&xit", ExitClick);
            _trayIcon.ContextMenu = _trayMenu;
            _trayIcon.Visible = true;
            _trayIcon.ShowBalloonTip(500);
        }


        public bool NanoNotPresentMB()// when nano not present , warn and close app
        {
            if (!slider.GetNano()) {
                const string message = "Nano. Slider not found.\nCheck if its connected or if  +" +
                                                                         "\"#define PRODUCT\" is set to:\nNano. Slider  ";
                const string caption = "Nano. Slider";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Error);
                // If the button was pressed ...
                if (result == DialogResult.OK)
                {
                    // close the app.
                    exit.ExitProgram();        // Kaboom!
                }
            }
            return false;
        }

        public bool ProgramAlreadyRuning()// when nano not present , warn and close app
        {
            if (StartUp.RunningInstance() != null)//check is not already runing before start
            {
                const string message = "Midi2Vol is already runing ";
                const string caption = "Nano. Slider";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                exit.ExitProgram();
            }
            return false;
        }



        private void ConfigClick(object sender, EventArgs e)
        {
            
            StartUp pp = new StartUp();
            string Path = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string shortcutfile = Path + "\\"+ Application.ProductName + ".lnk";
            Debug.WriteLine(shortcutfile);
            if (!File.Exists(shortcutfile))
            {
                pp.CreateStartupFolderShortcut();
                MessageBox.Show("Now "+Application.ProductName+" will launch on StartUp.");
            }
            else
            {
                pp.DeleteStartupFolderShortcuts(Application.ProductName);
                MessageBox.Show("Now " + Application.ProductName + " will not launch on StartUp.");
            }



        }
        private void ExitClick(object sender, EventArgs e)
        {
            exit.ExitProgram();
        }
    }
}
