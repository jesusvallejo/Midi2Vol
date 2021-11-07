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
        private MenuItem settingsmenu;
        private MenuItem noti;
        private MenuItem notifyStatus;
        private MenuItem runStartup;
        private MenuItem icon;
        private MenuItem iconSlider;
        private MenuItem iconBento;
        private MenuItem iconWavez;
        private MenuItem iconMizu;
        private MenuItem iconWhite;
        private MenuItem iconDynamic;
        private MenuItem exit;
        private System.Drawing.Icon dynamicIcon = Properties.Resources.NanoWhite;

        public TrayApplicationContext(Sett settings)
        {
            this.settings = settings;
            ;
            ContextMenu _trayMenu = new ContextMenu { };
            _trayIcon.Icon = GetIcon(settings.trayBarIcon)[0];
            _trayIcon.Text = Application.ProductName + " - Ver: "+ Application.ProductVersion;
            // Main menu
            appConfig = _trayMenu.MenuItems.Add("Volume Settings", configClick);
            settingsmenu = _trayMenu.MenuItems.Add("Midi2Vol Settings");
            icon = _trayMenu.MenuItems.Add("Icon Settings");
            _trayMenu.MenuItems.Add("-");
            exit = _trayMenu.MenuItems.Add("Exit", ExitClick);

            // Settings submenu
            noti = settingsmenu.MenuItems.Add("Notify", notif);
            if (settings.notifyApp) {
                noti.Checked = true;
            }

            runStartup = settingsmenu.MenuItems.Add("Run on StartUp", startupClick);
            if (settings.bootStartUp)
            {
                runStartup.Checked = true;
            }

            notifyStatus = settingsmenu.MenuItems.Add("Notify Status", notifS);
            if (settings.notifyStatus)
            {
                notifyStatus.Checked = true;
            }

            // Icon submenu 
            iconSlider = icon.MenuItems.Add("Default",clickSlider);
            iconBento = icon.MenuItems.Add("NanoBento",clickBento);
            iconWavez = icon.MenuItems.Add("NanoWavez",clickWavez);
            iconMizu = icon.MenuItems.Add("NanoMizu",clickMizu);
            iconWhite = icon.MenuItems.Add("NanoWhite", clickWhite);
            iconDynamic = icon.MenuItems.Add("NanoDynamic", clickDynamic);


            switch (settings.trayBarIcon)
            {
                case "NanoSlider":
                    iconSlider.Checked = true;
                    break;
                case "NanoBento":
                    iconBento.Checked = true;
                    break;
                case "NanoWavez":
                   iconWavez.Checked = true;
                    break;
                case "NanoMizu":
                    iconMizu.Checked = true;
                    break;
                case "NanoWhite":
                    iconWhite.Checked = true;
                    break;
                case "NanoDynamic":
                    iconDynamic.Checked = true;
                    break;
                default:
                    iconSlider.Checked = true;
                    break;
            }

            _trayIcon.ContextMenu = _trayMenu;
            _trayIcon.Visible = true;
        }

        // receives volume and if NanoDynamic selected in settings changes icon accordingly
        public void DynamicIcon(float volume) {
            if (settings.trayBarIcon == "NanoDynamic")
            {
                if (volume == 0.0)
                {
                    dynamicIcon = Properties.Resources._0white;
                }
                else if (volume < 0.15)
                {
                    dynamicIcon = Properties.Resources._10white;
                }
                else if (volume < 0.25)
                {
                    dynamicIcon = Properties.Resources._20white;
                }
                else if (volume < 0.35)
                {
                    dynamicIcon = Properties.Resources._30white;
                }
                else if (volume < 0.45)
                {
                    dynamicIcon = Properties.Resources._40white;
                }
                else if (volume < 0.55)
                {
                    dynamicIcon = Properties.Resources._50white;
                }
                else if (volume < 0.65)
                {
                    dynamicIcon = Properties.Resources._60white;
                }
                else if (volume < 0.75)
                {
                    dynamicIcon = Properties.Resources._70white;
                }
                else if (volume < 0.85)
                {
                    dynamicIcon = Properties.Resources._80white;
                }
                else if (volume < 0.95)
                {
                    dynamicIcon = Properties.Resources._90white;
                }
                else if (volume == 100)
                {
                    dynamicIcon = Properties.Resources._100white;
                }

                // update icon 
                _trayIcon.Icon = dynamicIcon;
            }
        }


        private void clickDynamic(object sender, EventArgs e)
        {
            
            iconWavez.Checked = false;
            iconBento.Checked = false;
            iconSlider.Checked = false;
            iconMizu.Checked = false;
            iconWhite.Checked = false;
            iconDynamic.Checked = true;
            settings.trayBarIcon = "NanoDynamic";
            Ready();
        }

        private void clickWhite(object sender, EventArgs e)
        {
            iconDynamic.Checked = false;
            iconWavez.Checked = false;
            iconBento.Checked = false;
            iconSlider.Checked = false;
            iconMizu.Checked = false;
            iconWhite.Checked = true;
            settings.trayBarIcon = "NanoWhite";
            Ready();
        }

        private void clickMizu(object sender, EventArgs e)
        {
            iconDynamic.Checked = false;
            iconWhite.Checked = false;
            iconWavez.Checked = false;
            iconBento.Checked = false;
            iconSlider.Checked = false;
            iconMizu.Checked = true;
            settings.trayBarIcon = "NanoMizu";
            Ready();

        }

        private void clickWavez(object sender, EventArgs e)
        {
            iconDynamic.Checked = false;
            iconWhite.Checked = false;
            iconBento.Checked = false;
            iconSlider.Checked = false;
            iconMizu.Checked = false;
            iconWavez.Checked = true;
            settings.trayBarIcon = "NanoWavez";
            Ready();
        }

        private void clickBento(object sender, EventArgs e)
        {
            iconDynamic.Checked = false;
            iconWhite.Checked = false;
            iconWavez.Checked = false;
            iconSlider.Checked = false;
            iconMizu.Checked = false;
            iconBento.Checked = true;
            settings.trayBarIcon = "NanoBento";
            Ready();
        }

        private void clickSlider(object sender, EventArgs e)
        {
            iconDynamic.Checked = false;
            iconWhite.Checked = false;
            iconWavez.Checked = false;
            iconBento.Checked = false;
            iconSlider.Checked = true;
            iconMizu.Checked = false;
            settings.trayBarIcon = "NanoSlider";
            Ready();
             
        }

        private void notifS(object sender, EventArgs e)
        {
            if (notifyStatus.Checked)
            {
                notifyStatus.Checked = false;
                settings.notifyStatus = false;
            }
            else
            {
                notifyStatus.Checked = true;
                settings.notifyStatus = true;
            }   
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
        }

        public System.Drawing.Icon [] GetIcon(String trayBarIcon)
        {
            System.Drawing.Icon [] icon= new System.Drawing.Icon[2];
            switch (trayBarIcon)
            {
                case "NanoSlider":
                    icon[0] = Properties.Resources.NanoSlider;
                    icon[1] = Properties.Resources.NanoSliderDis;
                    break;
                case "NanoBento":
                    icon[0] = Properties.Resources.NanoBento;
                    icon[1] = Properties.Resources.NanoBentoDis;
                    break;
                case "NanoWavez":
                    icon[0] = Properties.Resources.NanoWavez;
                    icon[1] = Properties.Resources.NanoWavezDis;
                    break;
                case "NanoMizu":
                    icon[0] = Properties.Resources.NanoMizu;
                    icon[1] = Properties.Resources.NanoMizuDis;
                    break;
                case "NanoWhite":
                    icon[0] = Properties.Resources.NanoWhite;
                    icon[1] = Properties.Resources.NanoWhiteDis;
                    break;
                case "NanoDynamic":
                    icon[0] = Properties.Resources.NanoWhite;
                    icon[1] = Properties.Resources.NanoWhiteDis;
                    break;
                default:
                    icon[0] = dynamicIcon;
                    icon[1] = Properties.Resources.NanoSliderDis;
                    break;
            }

            return icon;
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
            _trayIcon.Icon = GetIcon(settings.trayBarIcon)[0];
            _trayIcon.Visible = true;//to avoid showing two icons in the traybar
        }
        public void ReadyBaloon()
        {
            _trayIcon.BalloonTipText = "Midi2Vol is now ready.";
            _trayIcon.BalloonTipTitle = "Midi2Vol";
            _trayIcon.BalloonTipIcon = ToolTipIcon.Info;
            _trayIcon.ShowBalloonTip(0);
        }

        public void notReady()
        {
            _trayIcon.Visible = false;//to avoid showing two icons in the traybar
            _trayIcon.Icon = GetIcon(settings.trayBarIcon)[1];
            _trayIcon.Visible = true;//to avoid showing two icons in the traybar
        }
        public void notReadyBaloon()
        {
            _trayIcon.BalloonTipText = "Midi2Vol is not connected.";
            _trayIcon.BalloonTipTitle = "Midi2Vol";
            _trayIcon.BalloonTipIcon = ToolTipIcon.Error;
            _trayIcon.ShowBalloonTip(0);
        }
        private void ExitProgram()
        {
            Config config = new Config();
            config.saveConfig(settings);
            //config.saveAppConfig(apps);
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
                if (settings.notifyStatus)
                {
                    notReadyBaloon();
                }
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
            config.saveConfig(settings);
            _trayIcon.Visible = false;
            Application.Restart(); // restart to load new app configs
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
                if (!runStartup.Checked)
                {
                    runStartup.Checked = true;
                    settings.bootStartUp = true;
                    start.CreateStartupFolderShortcut();
                    MessageBox.Show("Now " + Application.ProductName + " will launch on StartUp.");
                }
                else {
                    runStartup.Checked = false;
                    settings.bootStartUp = false;
                }
            }
            else
            {
                if (runStartup.Checked)
                {
                    runStartup.Checked = false;
                    settings.bootStartUp = false;
                    start.DeleteStartupFolderShortcuts(Application.ProductName);
                    MessageBox.Show("Now " + Application.ProductName + " will not launch on StartUp.");
                }
                else {
                    runStartup.Checked = true;
                    settings.bootStartUp = true;
                }
            }
        }

        private void ExitClick(object sender, EventArgs e)
        {
            ExitProgram();
        }
    }
}
