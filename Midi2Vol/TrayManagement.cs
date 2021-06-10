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
            _trayIcon.Icon = Properties.Resources.NanoSlider;
            _trayIcon.Text = "Midi2Vol";
            _trayMenu.MenuItems.Add("Add/Remove Run on StartUp", ConfigClick);
            _trayMenu.MenuItems.Add("E&xit", ExitClick);
            _trayIcon.ContextMenu = _trayMenu;
            _trayIcon.Visible = true;
        }

        public void Ready() {
            _trayIcon.Visible = false;//to avoid showing two icons in the traybar
            _trayIcon.Icon = Properties.Resources.NanoSlider;
            _trayIcon.Visible = true;//to avoid showing two icons in the traybar
            _trayIcon.BalloonTipText = "Midi2Vol is now ready.";
              _trayIcon.BalloonTipTitle = "Midi2Vol";
              _trayIcon.BalloonTipIcon = ToolTipIcon.Info;
              _trayIcon.ShowBalloonTip(0);
            
        }
        public void notReady()
        {
            _trayIcon.Visible = false;//to avoid showing two icons in the traybar
            _trayIcon.Icon = Properties.Resources.NanoSliderDis;
            _trayIcon.Visible = true;//to avoid showing two icons in the traybar
            _trayIcon.BalloonTipText = "Midi2Vol is not connected.";
            _trayIcon.BalloonTipTitle = "Midi2Vol";
            _trayIcon.BalloonTipIcon = ToolTipIcon.Error;
            _trayIcon.ShowBalloonTip(250);

        }
        private void ExitProgram()
        {
            Application.Exit();           // closed everything
            Environment.Exit(1);         // Kaboom!
        }
        // will change it to a notification
         public bool NanoNotPresentMB(bool showed)// when nano not present , warn and wait for connection;
        {
            
            if (showed == false) {
            showed = true;
                notReady();
            }
            return true;
        }

         public bool ProgramAlreadyRuning()// 
        {
            
            if (StartUp.RunningInstance() != null)//check is not already running before start
            {
                const string message = "Midi2Vol is already runing.";
                const string caption = "Midi2Vol";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ExitProgram();
            }
            return false;
        }
        public void midiAlredyInUse()// 
        {
                const string message = "Nano. Slider is already in use by another application.";
                const string caption = "Midi2Vol";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ExitProgram();
       }
        public void thereWasAnIssue(String issue)// 
        {
             string message = "The was an issue with:"+issue+"\n Exiting Program";
             string caption = "Midi2Vol";
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            ExitProgram();
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
