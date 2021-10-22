using IWshRuntimeLibrary;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Midi2Vol
{
    public class StartUp
    {
        public void CreateStartupFolderShortcut()
        {
            WshShell wshShell = new WshShell();
            IWshRuntimeLibrary.IWshShortcut shortcut;
            string startUpFolderPath =
              Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            // Create the shortcut
            shortcut =
              (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(
                startUpFolderPath + "\\" +
                Application.ProductName + ".lnk");
            shortcut.TargetPath = Application.ExecutablePath;
            shortcut.WorkingDirectory = Application.StartupPath;
            shortcut.Description = "Launch My Application";
            shortcut.Save();
        }

        public void DeleteStartupFolderShortcuts(string targetExeName)
        {
            string startUpFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string shortcutfile = startUpFolderPath + "\\" + Application.ProductName + ".lnk";
            DirectoryInfo di = new DirectoryInfo(startUpFolderPath);
            FileInfo[] files = di.GetFiles("*.lnk");
            if (System.IO.File.Exists(shortcutfile))
            {
                System.IO.File.Delete(shortcutfile);
            }
        }

       
    }
}
