using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using static Vanara.PInvoke.WinMm;
using static Vanara.PInvoke.Kernel32;
using static Parabellum.Threading;
using static System.Math;
using System.Threading;
using Vanara.PInvoke;

namespace Parabellum
{
    public class Bytebeat
    {
        static Random rand = new Random();
        static SafeHWAVEOUT hWaveOut;
        public static void Beat1()
        {
            while (true)
            {
                WAVEFORMATEX wfx = new WAVEFORMATEX
                {
                    wFormatTag = WAVE_FORMAT.WAVE_FORMAT_PCM,
                    nChannels = 1,
                    nSamplesPerSec = 8000,
                    nAvgBytesPerSec = 8000,
                    nBlockAlign = 1,
                    wBitsPerSample = 8,
                    cbSize = 0
                };

                const uint WAVE_MAPPER = 0xFFFFFFFF;
                if (waveOutOpen(out hWaveOut, WAVE_MAPPER, in wfx, IntPtr.Zero, IntPtr.Zero, WAVE_OPEN.CALLBACK_NULL) != 0)
                    return;

                byte[] sbuffer = new byte[17000 * 60];

                int tc = 25;

                for (int t = 0; t < sbuffer.Length; t++)
                {
                    tc = 10;

                    sbuffer[t] = (byte)((t >> (tc) | t << (tc)) * (t >> (3) * (int)Cos(10) + (int)Sin(tc)));

                    tc += 10;

                    if (tc >= 50)
                    {
                        tc = 0;
                    }
                }

                GCHandle handle = GCHandle.Alloc(sbuffer, GCHandleType.Pinned);


                WAVEHDR header = new WAVEHDR
                {
                    lpData = handle.AddrOfPinnedObject(),
                    dwBufferLength = (uint)sbuffer.Length,
                    dwFlags = 0,
                    dwLoops = 1
                };

                waveOutPrepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
                waveOutWrite(hWaveOut, ref header, (uint)Marshal.SizeOf(header));

                waveOutUnprepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));

                try
                {
                    Thread.Sleep(Timeout.Infinite);
                }
                catch (ThreadAbortException)
                {
                    handle.Free();
                    waveOutReset(hWaveOut);
                    waveOutClose(hWaveOut);
                }
            }
        }

        public static void Beat2()
        {
            WAVEFORMATEX wfx = new WAVEFORMATEX
            {
                wFormatTag = WAVE_FORMAT.WAVE_FORMAT_PCM,
                nChannels = 1,
                nSamplesPerSec = 10000,
                nAvgBytesPerSec = 800,
                nBlockAlign = 1,
                wBitsPerSample = 8,
                cbSize = 0
            };

            const uint WAVE_MAPPER = 0xFFFFFFFF;
            if (waveOutOpen(out hWaveOut, WAVE_MAPPER, in wfx, IntPtr.Zero, IntPtr.Zero, WAVE_OPEN.CALLBACK_NULL) != 0)
                return;

            byte[] sbuffer = new byte[17000 * 60];

            for (int t = 0; t < sbuffer.Length; t++)
            {
                sbuffer[t] = (byte)(t >> 3 * t >> 1 * t * (int)Sin(3) / 70);
            }

            GCHandle handle = GCHandle.Alloc(sbuffer, GCHandleType.Pinned);


            WAVEHDR header = new WAVEHDR
            {
                lpData = handle.AddrOfPinnedObject(),
                dwBufferLength = (uint)sbuffer.Length,
                dwFlags = 0,
                dwLoops = 1
            };

            waveOutPrepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
            waveOutWrite(hWaveOut, ref header, (uint)Marshal.SizeOf(header));

            waveOutUnprepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));


            try
            {
                Thread.Sleep(Timeout.Infinite);
            }
            catch (ThreadAbortException)
            {
                handle.Free();
                waveOutReset(hWaveOut);
                waveOutClose(hWaveOut);
            }
        }


        public static void Beat3()
        {
            WAVEFORMATEX wfx = new WAVEFORMATEX
            {
                wFormatTag = WAVE_FORMAT.WAVE_FORMAT_PCM,
                nChannels = 1,
                nSamplesPerSec = 8000,
                nAvgBytesPerSec = 8000,
                nBlockAlign = 1,
                wBitsPerSample = 8,
                cbSize = 0
            };

            const uint WAVE_MAPPER = 0xFFFFFFFF;
            if (waveOutOpen(out hWaveOut, WAVE_MAPPER, in wfx, IntPtr.Zero, IntPtr.Zero, WAVE_OPEN.CALLBACK_NULL) != 0)
                return;

            byte[] sbuffer = new byte[17000 * 60];

            for (int t = 0; t < sbuffer.Length; t++)
            {
                int btt = (int)(t * t / (Cos(32) - t));

                sbuffer[t] = (byte)(btt * (t >> (t * t >> 3)) & t >> 32 *
                    (t - 2 * btt * rand.Next(3, 40) >> t * 3 | t >> 4) * 3 >> 2 >>
                    (t * 9 >> 2 * 7 + 2 >> 1));

            }

            GCHandle handle = GCHandle.Alloc(sbuffer, GCHandleType.Pinned);


            WAVEHDR header = new WAVEHDR
            {
                lpData = handle.AddrOfPinnedObject(),
                dwBufferLength = (uint)sbuffer.Length,
                dwFlags = 0,
                dwLoops = 1
            };

            waveOutPrepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
            waveOutWrite(hWaveOut, ref header, (uint)Marshal.SizeOf(header));

            waveOutUnprepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
            try
            {
                Thread.Sleep(Timeout.Infinite);
            }
            catch (ThreadAbortException)
            {
                handle.Free();
                waveOutReset(hWaveOut);
                waveOutClose(hWaveOut);
            }
        }


        public static void Beat4()
        {
            WAVEFORMATEX wfx = new WAVEFORMATEX
            {
                wFormatTag = WAVE_FORMAT.WAVE_FORMAT_PCM,
                nChannels = 1,
                nSamplesPerSec = 40000,
                nAvgBytesPerSec = 40000,
                nBlockAlign = 1,
                wBitsPerSample = 8,
                cbSize = 0
            };

            const uint WAVE_MAPPER = 0xFFFFFFFF;
            if (waveOutOpen(out hWaveOut, WAVE_MAPPER, in wfx, IntPtr.Zero, IntPtr.Zero, WAVE_OPEN.CALLBACK_NULL) != 0)
                return;

            byte[] sbuffer = new byte[17000 * 60];

            for (int t = 0; t < sbuffer.Length; t++)
            {
                sbuffer[t] = (byte)((t + ((t & t >> 12) * t >> 12) + 3e5 / (t % 1000)));
            }

            GCHandle handle = GCHandle.Alloc(sbuffer, GCHandleType.Pinned);


            WAVEHDR header = new WAVEHDR
            {
                lpData = handle.AddrOfPinnedObject(),
                dwBufferLength = (uint)sbuffer.Length,
                dwFlags = 0,
                dwLoops = 1
            };

            waveOutPrepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
            waveOutWrite(hWaveOut, ref header, (uint)Marshal.SizeOf(header));

            waveOutUnprepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
            try
            {
                Thread.Sleep(Timeout.Infinite);
            }
            catch (ThreadAbortException)
            {
                handle.Free();
                waveOutReset(hWaveOut);
                waveOutClose(hWaveOut);
            }
        }


        public static void Beat5()
        {
            WAVEFORMATEX wfx = new WAVEFORMATEX
            {
                wFormatTag = WAVE_FORMAT.WAVE_FORMAT_PCM,
                nChannels = 1,
                nSamplesPerSec = 44600,
                nAvgBytesPerSec = 44600,
                nBlockAlign = 1,
                wBitsPerSample = 8,
                cbSize = 0
            };

            const uint WAVE_MAPPER = 0xFFFFFFFF;
            if (waveOutOpen(out hWaveOut, WAVE_MAPPER, in wfx, IntPtr.Zero, IntPtr.Zero, WAVE_OPEN.CALLBACK_NULL) != 0)
                return;

            byte[] sbuffer = new byte[17000 * 60];

            for (int t = 0; t < sbuffer.Length; t++)
            {
                sbuffer[t] = (byte)(t * (-t >> (t >> 3) % 32 + 2 & (t >> 12) % 200) / 2 + (20 + (t % 32) & 28) >> (322 * 2));
            }

            GCHandle handle = GCHandle.Alloc(sbuffer, GCHandleType.Pinned);


            WAVEHDR header = new WAVEHDR
            {
                lpData = handle.AddrOfPinnedObject(),
                dwBufferLength = (uint)sbuffer.Length,
                dwFlags = 0,
                dwLoops = 1
            };

            waveOutPrepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
            waveOutWrite(hWaveOut, ref header, (uint)Marshal.SizeOf(header));

            waveOutUnprepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
            try
            {
                Thread.Sleep(Timeout.Infinite);
            }
            catch (ThreadAbortException)
            {
                handle.Free();
                waveOutReset(hWaveOut);
                waveOutClose(hWaveOut);
            }
        }


        public static void Beat6()
        {
            WAVEFORMATEX wfx = new WAVEFORMATEX
            {
                wFormatTag = WAVE_FORMAT.WAVE_FORMAT_PCM,
                nChannels = 1,
                nSamplesPerSec = 48000,
                nAvgBytesPerSec = 48000,
                nBlockAlign = 1,
                wBitsPerSample = 8,
                cbSize = 0
            };

            const uint WAVE_MAPPER = 0xFFFFFFFF;
            if (waveOutOpen(out hWaveOut, WAVE_MAPPER, in wfx, IntPtr.Zero, IntPtr.Zero, WAVE_OPEN.CALLBACK_NULL) != 0)
                return;

            byte[] sbuffer = new byte[17000 * 60];

            for (int t = 0; t < sbuffer.Length; t++)
            {
                sbuffer[t] = (byte)(t * t / (t + 50000 >> ((int)PI & t >> 1000)) * 1);
            }

            GCHandle handle = GCHandle.Alloc(sbuffer, GCHandleType.Pinned);


            WAVEHDR header = new WAVEHDR
            {
                lpData = handle.AddrOfPinnedObject(),
                dwBufferLength = (uint)sbuffer.Length,
                dwFlags = 0,
                dwLoops = 1
            };

            waveOutPrepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
            waveOutWrite(hWaveOut, ref header, (uint)Marshal.SizeOf(header));

            waveOutUnprepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
            try
            {
                Thread.Sleep(Timeout.Infinite);
            }
            catch (ThreadAbortException)
            {
                handle.Free();
                waveOutReset(hWaveOut);
                waveOutClose(hWaveOut);
            }
        }


        public static void Beat7()
        {
            WAVEFORMATEX wfx = new WAVEFORMATEX
            {
                wFormatTag = WAVE_FORMAT.WAVE_FORMAT_PCM,
                nChannels = 1,
                nSamplesPerSec = 10000,
                nAvgBytesPerSec = 10000,
                nBlockAlign = 1,
                wBitsPerSample = 8,
                cbSize = 0
            };

            const uint WAVE_MAPPER = 0xFFFFFFFF;
            if (waveOutOpen(out hWaveOut, WAVE_MAPPER, in wfx, IntPtr.Zero, IntPtr.Zero, WAVE_OPEN.CALLBACK_NULL) != 0)
                return;

            byte[] sbuffer = new byte[17000 * 60];

            for (int t = 0; t < sbuffer.Length; t++)
            {
                sbuffer[t] = (byte)(t * (40 >> t * t / 40) >> ((int)PI + (int)Cos(Sin(t >> t + 2))) >> (t | 2  * 4 >> t));
            }

            GCHandle handle = GCHandle.Alloc(sbuffer, GCHandleType.Pinned);


            WAVEHDR header = new WAVEHDR
            {
                lpData = handle.AddrOfPinnedObject(),
                dwBufferLength = (uint)sbuffer.Length,
                dwFlags = 0,
                dwLoops = 1
            };

            waveOutPrepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
            waveOutWrite(hWaveOut, ref header, (uint)Marshal.SizeOf(header));

            waveOutUnprepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
            try
            {
                Thread.Sleep(Timeout.Infinite);
            }
            catch (ThreadAbortException)
            {
                handle.Free();
                waveOutReset(hWaveOut);
                waveOutClose(hWaveOut);
            }
        }


        public static void Beat8()
        {
            WAVEFORMATEX wfx = new WAVEFORMATEX
            {
                wFormatTag = WAVE_FORMAT.WAVE_FORMAT_PCM,
                nChannels = 1,
                nSamplesPerSec = 9000,
                nAvgBytesPerSec = 9000,
                nBlockAlign = 1,
                wBitsPerSample = 8,
                cbSize = 0
            };

            const uint WAVE_MAPPER = 0xFFFFFFFF;
            if (waveOutOpen(out hWaveOut, WAVE_MAPPER, in wfx, IntPtr.Zero, IntPtr.Zero, WAVE_OPEN.CALLBACK_NULL) != 0)
                return;

            byte[] sbuffer = new byte[16000 * 60];

            for (int t = 0; t < sbuffer.Length; t++)
            {
                sbuffer[t] = (byte)((t >> 1) * (t >> 7) | t >> 33 ^ t >> 2 | (t >> 2));
            }

            GCHandle handle = GCHandle.Alloc(sbuffer, GCHandleType.Pinned);


            WAVEHDR header = new WAVEHDR
            {
                lpData = handle.AddrOfPinnedObject(),
                dwBufferLength = (uint)sbuffer.Length,
                dwFlags = 0,
                dwLoops = 1
            };

            waveOutPrepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
            waveOutWrite(hWaveOut, ref header, (uint)Marshal.SizeOf(header));

            waveOutUnprepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));

            try
            {
                Thread.Sleep(Timeout.Infinite);
            }
            catch (ThreadAbortException)
            {
                handle.Free();
                waveOutReset(hWaveOut);
                waveOutClose(hWaveOut);
            }
        }


        public static void Beat9()
        {
            WAVEFORMATEX wfx = new WAVEFORMATEX
            {
                wFormatTag = WAVE_FORMAT.WAVE_FORMAT_PCM,
                nChannels = 1,
                nSamplesPerSec = 8000,
                nAvgBytesPerSec = 8000,
                nBlockAlign = 1,
                wBitsPerSample = 8,
                cbSize = 0
            };

            const uint WAVE_MAPPER = 0xFFFFFFFF;
            if (waveOutOpen(out hWaveOut, WAVE_MAPPER, in wfx, IntPtr.Zero, IntPtr.Zero, WAVE_OPEN.CALLBACK_NULL) != 0)
                return;

            byte[] sbuffer = new byte[17000 * 60];

            for (int t = 0; t < sbuffer.Length; t++)
            {
                sbuffer[t] = (byte)((t * (3 & t >> 8 | (t >> 10 & 9) + 2 * (6 & t >> (10 & 9)) + 3) & 100));
            }

            GCHandle handle = GCHandle.Alloc(sbuffer, GCHandleType.Pinned);


            WAVEHDR header = new WAVEHDR
            {
                lpData = handle.AddrOfPinnedObject(),
                dwBufferLength = (uint)sbuffer.Length,
                dwFlags = 0,
                dwLoops = 1
            };

            waveOutPrepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
            waveOutWrite(hWaveOut, ref header, (uint)Marshal.SizeOf(header));

            waveOutUnprepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));

            try
            {
                Thread.Sleep(Timeout.Infinite);
            }
            catch (ThreadAbortException)
            {
                handle.Free();
                waveOutReset(hWaveOut);
                waveOutClose(hWaveOut);
            }
        }


        public static void Beat10()
        {
            WAVEFORMATEX wfx = new WAVEFORMATEX
            {
                wFormatTag = WAVE_FORMAT.WAVE_FORMAT_PCM,
                nChannels = 1,
                nSamplesPerSec = 500,
                nAvgBytesPerSec = 12000,
                nBlockAlign = 1,
                wBitsPerSample = 8,
                cbSize = 0
            };

            const uint WAVE_MAPPER = 0xFFFFFFFF;
            if (waveOutOpen(out hWaveOut, WAVE_MAPPER, in wfx, IntPtr.Zero, IntPtr.Zero, WAVE_OPEN.CALLBACK_NULL) != 0)
                return;

            byte[] sbuffer = new byte[17000 * 60];

            for (int t = 0; t < sbuffer.Length; t++)
            {
                sbuffer[t] = (byte)((t << 4) ^ -(t >> 4 & 1) ^ -(t >> 5));
            }

            GCHandle handle = GCHandle.Alloc(sbuffer, GCHandleType.Pinned);


            WAVEHDR header = new WAVEHDR
            {
                lpData = handle.AddrOfPinnedObject(),
                dwBufferLength = (uint)sbuffer.Length,
                dwFlags = 0,
                dwLoops = 1
            };

            waveOutPrepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
            waveOutWrite(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
            waveOutUnprepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));


            try
            {
                Thread.Sleep(Timeout.Infinite);
            }
            catch (ThreadAbortException)
            {
                handle.Free();
                waveOutReset(hWaveOut);
                waveOutClose(hWaveOut);
            }
        }

    }
}
