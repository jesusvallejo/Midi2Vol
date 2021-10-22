using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Midi2Vol
{
    public class TrayApplicationContext : ApplicationContext
    {
        NotifyIcon _trayIcon = new NotifyIcon();
        public List<App> apps;
        public Sett settings;
        private MenuItem appConfig;
        private MenuItem noti;
        private MenuItem runStartup;
        private MenuItem exit;

        public TrayApplicationContext(Sett settings)
        {
            this.settings = settings;
            ContextMenu _trayMenu = new ContextMenu { };
            _trayIcon.Icon = Properties.Resources.NanoSlider;
            _trayIcon.Text = "Midi2Vol";
            appConfig = _trayMenu.MenuItems.Add("App Config", configClick);
            noti = _trayMenu.MenuItems.Add("Notify", notif);
            if (settings.notifyApp) {
                noti.Checked = true;
            }
            runStartup =_trayMenu.MenuItems.Add("Run on StartUp", startupClick);
            if (settings.bootStartUp)
            {
                runStartup.Checked = true;
            }
            exit = _trayMenu.MenuItems.Add("Exit", ExitClick);
            _trayIcon.ContextMenu = _trayMenu;
            _trayIcon.Visible = true;
        }

        private void notif(object sender, EventArgs e)
        {
            if (noti.Checked)
            {
                noti.Checked = false;
                settings.notifyApp = false;
            }
            else 
            {
                noti.Checked = true;
                settings.notifyApp = true;
            }
            //throw new NotImplementedException();
        }

        public void appVolume(App app) {
            _trayIcon.BalloonTipText = app.name + " volume is now controlled";
            _trayIcon.BalloonTipTitle = "Midi2Vol";
            _trayIcon.BalloonTipIcon = ToolTipIcon.Info;
            _trayIcon.ShowBalloonTip(0);
        }
        /*
         * Edits already instanciated trayIcon icon and launches notification.
         */
        public void Ready()
        {
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

        /*
         * Calls not ready only one time, then the app will poll until nano slider is present.
         */

        public bool NanoNotPresentMB(bool showed)// when nano not present , warn and wait for connection;
        {
            if (showed == false)
            {
                notReady();
            }
            return true;
        }

        public void ProgramAlreadyRuning()
        {
                const string message = "Midi2Vol is already runing.";
                const string caption = "Midi2Vol";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ExitProgram();
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
            string message = "The was an issue with:" + issue + "\n Exiting Program";
            string caption = "Midi2Vol";
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            ExitProgram();
        }
         private void configClick(object sender, EventArgs e) {
            Config config = new Config(); 
            apps = config.SourceAppConfig();
            Edit edit = new Edit(apps);
            edit.Show();
            edit.FormClosed += new FormClosedEventHandler(edit_FormClosed) ;
        }

         void edit_FormClosed(object sender, FormClosedEventArgs e)
        {
            Config config = new Config();
            config.saveAppConfig(apps);
            Application.Restart();
            Environment.Exit(0);
        }


         private void startupClick(object sender, EventArgs e)
        {
            StartUp start = new StartUp();
            string Path = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string shortcutfile = Path + "\\" + Application.ProductName + ".lnk";
            Debug.WriteLine(shortcutfile);
            if (!File.Exists(shortcutfile))
            {
                runStartup.Checked = true;
                settings.bootStartUp = true;
                start.CreateStartupFolderShortcut();
                MessageBox.Show("Now " + Application.ProductName + " will launch on StartUp.");
            }
            else
            {
                runStartup.Checked = false;
                settings.bootStartUp = false;
                start.DeleteStartupFolderShortcuts(Application.ProductName);
                MessageBox.Show("Now " + Application.ProductName + " will not launch on StartUp.");
            }
        }

        private void ExitClick(object sender, EventArgs e)
        {
            ExitProgram();
        }
    }
}
