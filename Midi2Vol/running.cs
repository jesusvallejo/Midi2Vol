using System;
using Microsoft.Win32;
using System.Diagnostics;
using System.Reflection;

namespace Midi2Vol
{
    public class StartUp
    {
        private void SetStartup() //on developement
        {
            String AppName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            RegistryKey rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            // if (chkStartUp.Checked)
            // rk.SetValue(AppName, Application.ExecutablePath);
            // else
            //  rk.DeleteValue(AppName, false);

        }



        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);

            //Loop through the running processes in with the same name 
            foreach (Process process in processes)
            {
                //Ignore the current process 
                if (process.Id != current.Id)
                {
                    //Make sure that the process is running from the exe file. 
                    if (Assembly.GetExecutingAssembly().Location.
                         Replace("/", "\\") == current.MainModule.FileName)

                    {
                        //Return the other process instance.  
                        return process;

                    }
                }
            }
            //No other instance was found, return null.  
            return null;
        }



    }
}
