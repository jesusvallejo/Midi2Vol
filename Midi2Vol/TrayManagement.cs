using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Midi2Vol
{
     public class TrayApplicationContext : ApplicationContext
    {
        static NotifyIcon _trayIcon = new NotifyIcon();
        

         public TrayApplicationContext()
        {
            ContextMenu _trayMenu = new ContextMenu { };
            _trayIcon.Icon = Properties.Resources.NanoWavez;
            _trayIcon.Text = "Nano. Slider";
            _trayMenu.MenuItems.Add("Add/Remove Run on StartUp", ConfigClick);
            _trayMenu.MenuItems.Add("E&xit", ExitClick);
            _trayIcon.ContextMenu = _trayMenu;
            _trayIcon.Visible = true;
        }


        public void Ready() {
              _trayIcon.BalloonTipText = "Nano. Slider is now ready.";
              _trayIcon.BalloonTipTitle = "Nano. Slider Win2Vol";
              _trayIcon.BalloonTipIcon = ToolTipIcon.Info;
              _trayIcon.ShowBalloonTip(500);
            
        }

         private void ExitProgram()
        {
            Application.Exit();           // closed everything
            Environment.Exit(1);         // Kaboom!
        }

         public bool NanoNotPresentMB(bool showed)// when nano not present , warn and close app
        {
            if (showed == false) {
            showed = true;
            _trayIcon.Visible = false;//to avoid showing two icons in the traybar  
            const string message = "Nano. Slider not found, please connect it. Do you want to close Nano. Slider Win2Vol? ";
            const string caption = "Nano. Slider";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OKCancel,
                                         MessageBoxIcon.Error);
                // If the button was pressed ...
                if (result == DialogResult.OK)
                {
                    // close the app.
                    ExitProgram();        // Kaboom!
                }
                else {
                    _trayIcon.Visible = true;//now can show the icon
                }
                

                
        }
            return true;
        }

         public bool ProgramAlreadyRuning()// when nano not present , warn and close app
        {
            if (StartUp.RunningInstance() != null)//check is not already runing before start
            {
                const string message = "Midi2Vol is already runing ";
                const string caption = "Nano. Slider";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ExitProgram();
            }
            return false;
        }



        static private void ConfigClick(object sender, EventArgs e)
        {
            StartUp pp = new StartUp();
            string Path = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string shortcutfile = Path + "\\" + Application.ProductName + ".lnk";
            Debug.WriteLine(shortcutfile);
            if (!File.Exists(shortcutfile))
            {
                pp.CreateStartupFolderShortcut();
                MessageBox.Show("Now " + Application.ProductName + " will launch on StartUp.");
            }
            else
            {
                pp.DeleteStartupFolderShortcuts(Application.ProductName);
                MessageBox.Show("Now " + Application.ProductName + " will not launch on StartUp.");
            }
        }
         private void ExitClick(object sender, EventArgs e)
        {
           ExitProgram();
        }
    }
}
