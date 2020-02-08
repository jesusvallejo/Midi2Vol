using System.Windows.Forms;
using System;
using System.Drawing;

namespace Midi2Vol
{
    public class TrayApplicationContext : ApplicationContext
    {
        NotifyIcon _trayIcon = new NotifyIcon();
        public TrayApplicationContext()
        {

            if (MidiSlider.NanoFind() == -1)
            {
                NanoNotPresent();
            }
            ContextMenu _trayMenu = new ContextMenu { };
            _trayIcon.Icon = Properties.Resources.NanoSlider;
            _trayIcon.Text = "Nano. Slider";
            _trayMenu.MenuItems.Add("E&xit", Exit_Click);
            _trayIcon.ContextMenu = _trayMenu;
            _trayIcon.Visible = true;
        }


        public void NanoNotPresent()// when nano not present , warn and close app
        {
            const string message =
                "Nano. Slider not found.\nCheck if its connected or if  \"#define PRODUCT\" is set to:\nNano. Slider  ";
            const string caption = "Nano. Slider";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Error);
            // If the button was pressed ...
            if (result == DialogResult.OK)
            {
                // close the app.
                Environment.Exit(1);         // Kaboom!
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(1);         // Kaboom!
        }
    }
}
