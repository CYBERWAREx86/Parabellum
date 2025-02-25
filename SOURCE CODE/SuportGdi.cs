using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using static Vanara.PInvoke.User32;
using static Vanara.PInvoke.Gdi32;
using static System.Math;
using static Vanara.PInvoke.Kernel32;
using static Vanara.PInvoke.Gdi32.RasterOperationMode;
using Vanara.PInvoke;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Parabellum
{
    public class SuportGdi
    {
        static int w = GetSystemMetrics(0);
        static int h = GetSystemMetrics((SystemMetric)1);
        static Random rand = new Random();

        static (int x, int y)[] iconPositions = new (int x, int y)[50];
        static (double dx, double dy)[] iconVelocities = new (double dx, double dy)[50];

        public static void Sierpinski(HDC dc, Point p1, Point p2, Point p3, int depth)
        {
            if (depth == 0)
            {
                Point[] vertices = { p1, p2, p3 };
                using (var brush = CreateSolidBrush(new COLORREF((byte)rand.Next(255), (byte)rand.Next(255), (byte)rand.Next(255))))
                {
                    var hOldBrush = SelectObject(dc, brush);
                    Polygon(dc, vertices, vertices.Length);
                    SelectObject(dc, hOldBrush);
                    DeleteObject(brush);
                }
                return;
            }

            Point middle1 = new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
            Point middle2 = new Point((p2.X + p3.X) / 2, (p2.Y + p3.Y) / 2);
            Point middle3 = new Point((p3.X + p1.X) / 2, (p3.Y + p1.Y) / 2);

            Sierpinski(dc, p1, middle1, middle3, depth - 1);
            Sierpinski(dc, middle1, p2, middle2, depth - 1);
            Sierpinski(dc, middle3, middle2, p3, depth - 1);
        }


        static void HLS2RGB(double h, double l, double s, out byte r, out byte g, out byte b)
        {
            double q = l < 0.5 ? l * (1 + s) : l + s - l * s;
            double p = 2 * l - q;

            double[] trgb = { h + 1.0 / 3.0, h, h - 1.0 / 3.0 };

            for (int i = 0; i < 3; i++)
            {
                if (trgb[i] < 0) trgb[i] += 1.0;
                if (trgb[i] > 1) trgb[i] -= 1.0;

                if (trgb[i] < 1.0 / 6.0)
                    trgb[i] = p + ((q - p) * 6.0 * trgb[i]);
                else if (trgb[i] < 1.0 / 2.0)
                    trgb[i] = q;
                else if (trgb[i] < 2.0 / 3.0)
                    trgb[i] = p + ((q - p) * (2.0 / 3.0 - trgb[i]) * 6.0);
                else
                    trgb[i] = p;
            }

            r = (byte)(trgb[0] * 255.0);
            g = (byte)(trgb[1] * 255.0);
            b = (byte)(trgb[2] * 255.0);
        }

        public static void RgbDraw()
        {
            var dc = GetDC(HWND.NULL);
            var dcCopy = CreateCompatibleDC(dc);

            BITMAPINFO bmpi = new BITMAPINFO();
            bmpi.bmiHeader.biSize = (uint)Marshal.SizeOf(bmpi.bmiHeader);
            bmpi.bmiHeader.biWidth = w;
            bmpi.bmiHeader.biHeight = h;
            bmpi.bmiHeader.biPlanes = 1;
            bmpi.bmiHeader.biBitCount = 32;
            bmpi.bmiHeader.biCompression = 0;

            IntPtr bits;
            var hBitmap = CreateDIBSection(dc, in bmpi, 0, out bits, IntPtr.Zero, 0);
            SelectObject(dcCopy, hBitmap);

            while (true)
            {
                StretchBlt(dcCopy, 0, 0, w, h, dc, 0, 0, w, h, SRCCOPY);

                unsafe
                {
                    RGBQUAD* rgbquad = (RGBQUAD*)bits;

                    Parallel.For(0, h, y =>
                    {
                        for (int x = 0; x < w; x++)
                        {
                            int index = y * w + x;

                            int centerX = w / 2;
                            int centerY = h / 2;
                            double distance = Sqrt(Pow(x - centerX, 2) + Pow(y - centerY, 2));

                            double hueShift = distance * 0.05 + Environment.TickCount * 0.02;
                            double newHue = (hueShift % 360) / 360.0;

                            double r = rgbquad[index].rgbRed / 255.0;
                            double g = rgbquad[index].rgbGreen / 255.0;
                            double b = rgbquad[index].rgbBlue / 255.0;

                            double max = Max(r, Max(g, b));
                            double min = Min(r, Min(g, b));
                            double delta = max - min;

                            double hue = 0, saturation = 0, luminance = (max + min) / 2;

                            if (delta > 0)
                            {
                                saturation = luminance < 0.5 ? delta / (max + min) : delta / (2 - max - min);
                                if (r == max)
                                    hue = (g - b) / delta + (g < b ? 6 : 0);
                                else if (g == max)
                                    hue = (b - r) / delta + 2;
                                else
                                    hue = (r - g) / delta + 4;

                                hue /= 6;
                            }

                            hue = (hue + newHue) % 1.0;
                            HLS2RGB(hue, luminance, saturation, out byte red, out byte green, out byte blue);

                            rgbquad[index].rgbRed = red;
                            rgbquad[index].rgbGreen = green;
                            rgbquad[index].rgbBlue = blue;
                            rgbquad[index].rgbReserved = 255;
                        }
                    });
                }

                AlphaBlend(dc, 0, 0, w, h, dcCopy, 0, 0, w, h, new BLENDFUNCTION(255));
                Sleep(200);
            }
        }


        public static void DrawIconsMove(ResourceId icoName, int quantityIcon, int DisplayTime, bool RandomIcons = false)
        {
            for (int i = 0; i < quantityIcon; i++)
            {
                iconPositions[i] = (rand.Next(0, w), rand.Next(0, h));

                double angle = rand.NextDouble() * 2 * PI;
                double speed = rand.Next(1, 5);
                iconVelocities[i] = (speed * Cos(angle), speed * Sin(angle));
            }

            while (true)
            {
                for (int index = 0; index < quantityIcon; index++)
                {
                    var dc = GetDC(HWND.NULL);

                    iconPositions[index] = (iconPositions[index].x + (int)iconVelocities[index].dx,
                                            iconPositions[index].y + (int)iconVelocities[index].dy);

                    if (iconPositions[index].x <= 0 || iconPositions[index].x >= w)
                    {
                        iconVelocities[index] = (-iconVelocities[index].dx, iconVelocities[index].dy);
                    }
                    if (iconPositions[index].y <= 0 || iconPositions[index].y >= h)
                    {
                        iconVelocities[index] = (iconVelocities[index].dx, -iconVelocities[index].dy);
                    }

                    SafeHICON hIconRandom = RandomIcon();
                    SafeHICON hIcon = LoadIcon(HINSTANCE.NULL, icoName);

                    if (hIcon != null && !hIcon.IsInvalid)
                    {
                        if (RandomIcons)
                        {
                            DrawIcon(dc, iconPositions[index].x, iconPositions[index].y, hIconRandom);
                        }
                        else
                        {
                            DrawIcon(dc, iconPositions[index].x, iconPositions[index].y, hIcon);
                        }
                    }

                    double randomAngleChange = (rand.NextDouble() - 0.5) * 0.1;
                    double randomSpeedChange = (rand.NextDouble() - 0.5) * 0.5;

                    double angle = Atan2(iconVelocities[index].dy, iconVelocities[index].dx) + randomAngleChange;
                    double speed = Sqrt(iconVelocities[index].dx * iconVelocities[index].dx + iconVelocities[index].dy * iconVelocities[index].dy) + randomSpeedChange;

                    iconVelocities[index] = (speed * Cos(angle), speed * Sin(angle));
                }

                Thread.Sleep(DisplayTime);
            }
        }
        public static SafeHICON RandomIcon()
        {
            SafeHICON[] icons = new SafeHICON[]
            {
                LoadIcon(HINSTANCE.NULL, IDI_APPLICATION),
                LoadIcon(HINSTANCE.NULL, IDI_ASTERISK),
                LoadIcon(HINSTANCE.NULL, IDI_ERROR),
                LoadIcon(HINSTANCE.NULL, IDI_EXCLAMATION),
                LoadIcon(HINSTANCE.NULL, IDI_HAND),
                LoadIcon(HINSTANCE.NULL, IDI_INFORMATION),
                LoadIcon(HINSTANCE.NULL, IDI_QUESTION),
                LoadIcon(HINSTANCE.NULL, IDI_WARNING),
                LoadIcon(HINSTANCE.NULL, IDI_WINLOGO)
            };

            return icons[rand.Next(icons.Length)];
        }


        public static void BlurEffect()
        {
            var hdc = GetDC(HWND.NULL);
            var dcCopy = CreateCompatibleDC(hdc);

            try
            {
                BITMAPINFO bmi = new BITMAPINFO();
                bmi.bmiHeader.biSize = (uint)Marshal.SizeOf(bmi.bmiHeader);
                bmi.bmiHeader.biWidth = w;
                bmi.bmiHeader.biHeight = h;
                bmi.bmiHeader.biPlanes = 1;
                bmi.bmiHeader.biBitCount = 32;
                bmi.bmiHeader.biCompression = 0;

                IntPtr bits;
                var hbmp = CreateDIBSection(hdc, in bmi, 0, out bits, IntPtr.Zero, 0);
                var oldBmp = SelectObject(dcCopy, hbmp);

                int blurSize = 10;
                int blurRadius = blurSize / 2;

                while (true)
                {
                    BitBlt(dcCopy, 0, 0, w, h, hdc, 0, 0, SRCCOPY);

                    unsafe
                    {
                        RGBQUAD* rgbquad = (RGBQUAD*)bits.ToPointer();

                        for (int y = blurRadius; y < h - blurRadius; y++)
                        {
                            for (int x = blurRadius; x < w - blurRadius; x++)
                            {
                                int r = 0, g = 0, b = 0;
                                int count = 0;

                                for (int ky = -blurRadius; ky <= blurRadius; ky++)
                                {
                                    for (int kx = -blurRadius; kx <= blurRadius; kx++)
                                    {
                                        int posX = x + kx;
                                        int posY = y + ky;
                                        if (posX >= 0 && posX < w && posY >= 0 && posY < h)
                                        {
                                            RGBQUAD pixel = rgbquad[posY * w + posX];
                                            r += pixel.rgbRed;
                                            g += pixel.rgbGreen;
                                            b += pixel.rgbBlue;
                                            count++;
                                        }
                                    }
                                }

                                RGBQUAD* pixelBlurred = rgbquad + y * w + x;
                                pixelBlurred->rgbRed = (byte)(r / count);
                                pixelBlurred->rgbGreen = (byte)(g / count);
                                pixelBlurred->rgbBlue = (byte)(b / count);
                            }
                        }
                    }

                    BitBlt(hdc, 0, 0, w, h, dcCopy, 0, 0, SRCCOPY);

                    Thread.Sleep(200);
                }
            }
            finally
            {
                DeleteDC(dcCopy);
                ReleaseDC(IntPtr.Zero, hdc);
            }
        }

        public static void Scribble()
        {
            var dc = GetDC(IntPtr.Zero);
            var dcCopy = CreateCompatibleDC(dc);

            BITMAPINFO bmpi = new BITMAPINFO();
            bmpi.bmiHeader.biSize = (uint)Marshal.SizeOf(bmpi.bmiHeader);
            bmpi.bmiHeader.biWidth = w;
            bmpi.bmiHeader.biHeight = h;
            bmpi.bmiHeader.biPlanes = 1;
            bmpi.bmiHeader.biBitCount = 32;
            bmpi.bmiHeader.biCompression = 0;

            IntPtr ppvBits;
            var bmp = CreateDIBSection(dc, in bmpi, 0, out ppvBits, IntPtr.Zero, 0);
            SelectObject(dcCopy, bmp);

            unsafe
            {

                RGBQUAD* rgbquad = (RGBQUAD*)ppvBits;
                int i = 0;

                while (true)
                {
                    StretchBlt(dcCopy, 0, 0, w, h, dc, 0, 0, w, h, SRCCOPY);

                    for (int y = 0; y < h; y++)
                    {

                        for (int x = 0; x < w; x++)
                        {
                            int index = y * w + x;

                            int offsetX = (int)(10 * Math.Sin((double)x / 1 + i * 0.1));
                            int offsetY = (int)(10 * Math.Cos((double)y / 2 + i * 0.1));

                            int newX = x + offsetX;
                            int newY = y + offsetY;

                            if (newX >= 0 && newX < w && newY >= 0 && newY < h)
                            {
                                int newIndex = newY * w + newX;
                                rgbquad[index] = rgbquad[newIndex];

                            }
                        }
                    }

                    i++;
                    StretchBlt(dc, 0, 0, w, h, dcCopy, 0, 0, w, h, SRCCOPY);

                }
            }
        }

    }
}
