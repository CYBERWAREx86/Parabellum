using System;
using static Vanara.PInvoke.User32;
using static Vanara.PInvoke.Gdi32;
using static Vanara.PInvoke.Kernel32;
using Vanara.PInvoke;
using System.Drawing;
using static Parabellum.SuportGdi;
using static Vanara.PInvoke.Gdi32.RasterOperationMode;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using static Vanara.PInvoke.AviFil32;
using Theraot;

namespace Parabellum
{
    public class PayloadGdi
    {
        public static (int x, int y)[] iconPositions = new (int x, int y)[900];
        public static (double dx, double dy)[] iconVelocities = new (double dx, double dy)[900];

        static int w = GetSystemMetrics(0);
        static int h = GetSystemMetrics((SystemMetric)1);

        static Random rand = new Random();

        public static void ClearScreen()
        {
            RedrawWindow(HWND.NULL, null, HRGN.NULL, RedrawWindowFlags.RDW_INVALIDATE | RedrawWindowFlags.RDW_ERASE | RedrawWindowFlags.RDW_ALLCHILDREN);
        }
        public static void Load1()
        {
            while (true)
            {
                for (int i = 0; i <= 10; i++)
                {
                    var dc = GetDC(HWND.NULL);

                    int depth = i;

                    Point p1 = new Point(w / 2, 50);
                    Point p2 = new Point(50, h - 50);
                    Point p3 = new Point(w - 50, h - 50);
                    Sierpinski(dc, p1, p2, p3, depth);


                    if (i == 11)
                    {
                        i = 0;
                    }
                }
            }
        }

        public static void Load2()
        {
            while (true)
            {
                var dc = GetDC(HWND.NULL);

                int centerX = rand.Next(w);
                int centerY = rand.Next(h);

                int size = rand.Next(100, 200);
                int layers = 10;

                byte r = (byte)rand.Next(255);
                byte g = (byte)rand.Next(255);
                byte b = (byte)rand.Next(255);

                for (int i = layers; i > 0; i--)
                {
                    byte adjustedR = (byte)(r / i);
                    byte adjustedG = (byte)(g / i);
                    byte adjustedB = (byte)(b / i);

                    HBRUSH brush = CreateSolidBrush(new COLORREF(adjustedR, adjustedG, adjustedB));
                    var hOldBrush = SelectObject(dc, brush);

                    Point[] vertices = new Point[3];
                    for (int j = 0; j < vertices.Length; j++)
                    {
                        double angle = 2 * Math.PI * j / 3;
                        vertices[j] = new Point(
                            centerX + (int)(size * i / layers * Math.Cos(angle)),
                            centerY + (int)(size * i / layers * Math.Sin(angle))
                        );
                    }

                    Polygon(dc, vertices, vertices.Length);

                    SelectObject(dc, hOldBrush);
                    DeleteObject(brush);
                }
                ReleaseDC(HWND.NULL, dc);
            }
        }

        public static void Load3()
        {
            while (true)
            {
                Point[] lppoint = new Point[3];
                var dc = GetDC(HWND.NULL);
                var dcC = CreateCompatibleDC(dc);

                var hbit = CreateCompatibleBitmap(dcC, w, h);
                var hdbit = SelectObject(dcC, hbit);

                RECT rc = new RECT(0, 0, w, h);

                HBRUSH brush = CreateSolidBrush(new COLORREF((byte)rand.Next(255), (byte)rand.Next(255), (byte)rand.Next(255)));
                var hOldBrush = SelectObject(dc, brush);

                lppoint[0].X = (rc.left + 50) + 0;
                lppoint[0].Y = (rc.top - 50) + 0;
                lppoint[1].X = (rc.right + 50) + 0;
                lppoint[1].Y = (rc.top + 50) + 0;
                lppoint[2].X = (rc.left - 50) + 0;
                lppoint[2].Y = (rc.bottom - 50) + 0;

                int mode = rand.Next(1, 5);

                HBRUSH brushh = CreateSolidBrush(new COLORREF((byte)rand.Next(255), (byte)rand.Next(255), (byte)rand.Next(255)));
                var hOldBrushh = SelectObject(dc, brushh);

                Point[] verticess = new Point[200];
                for (int i = 1; i < verticess.Length; i++)
                {
                    verticess[i] = new Point(rand.Next(w), rand.Next(h));
                }

                Polygon(dc, verticess, verticess.Length);

                StretchBlt(dc, -50, -50, w + 100, h + 100, dc, 0, 0, w, h, RasterOperationMode.SRCCOPY);

                switch (mode)
                {
                    case 1:
                        Point[] vertices = new Point[200];
                        for (int i = 1; i < vertices.Length; i++)
                        {
                            vertices[i] = new Point(rand.Next(w), rand.Next(h));
                        }

                        Polygon(dc, vertices, vertices.Length);

                        StretchBlt(dc, -400, -400, w + 800, h + 8000, dc, 0, 0, w, h, SRCCOPY);
                        PlgBlt(dc, lppoint, dc, rc.left - 20, rc.top - 20, (rc.right - rc.left) + 40, (rc.bottom - rc.top) + 40, IntPtr.Zero, 0, 0);

                        ReleaseDC(HWND.NULL, dc);
                        SelectObject(dc, hOldBrush);
                        DeleteObject(brush);
                        ReleaseDC(HWND.NULL, dc);
                        break;

                    case 2:
                        Point[] vertices2 = new Point[200];
                        for (int i = 1; i < vertices2.Length; i++)
                        {
                            vertices2[i] = new Point(rand.Next(w), rand.Next(h));
                        }

                        Polygon(dc, vertices2, vertices2.Length);

                        StretchBlt(dc, -400, -400, w + 200, h + 200, dc, 0, 0, w, h, SRCCOPY);

                        ReleaseDC(HWND.NULL, dc);
                        SelectObject(dc, hOldBrush);
                        DeleteObject(brush);
                        ReleaseDC(HWND.NULL, dc);
                        break;

                    case 3:
                        Point[] vertices3 = new Point[200];
                        for (int i = 1; i < vertices3.Length; i++)
                        {
                            vertices3[i] = new Point(rand.Next(w), rand.Next(h));
                        }

                        Polygon(dc, vertices3, vertices3.Length);

                        PlgBlt(dc, lppoint, dc, rc.left - 200, rc.top - 100, (rc.right - rc.left) + 200, (rc.bottom - rc.top) + 100, IntPtr.Zero, 0, 0);

                        ReleaseDC(HWND.NULL, dc);
                        SelectObject(dc, hOldBrush);
                        DeleteObject(brush);
                        ReleaseDC(HWND.NULL, dc);
                        break;

                    case 4:
                        Point[] vertices4 = new Point[200];
                        for (int i = 1; i < vertices4.Length; i++)
                        {
                            vertices4[i] = new Point(rand.Next(w), rand.Next(h));
                        }

                        Polygon(dc, vertices4, vertices4.Length);

                        StretchBlt(dc, -400, -300, w + 200, h + 100, dc, 0, 0, w, h, SRCCOPY);
                        PlgBlt(dc, lppoint, dc, rc.left - 20, rc.top - 20, (rc.right - rc.left) + 40, (rc.bottom - rc.top) + 40, IntPtr.Zero, 0, 0);

                        ReleaseDC(HWND.NULL, dc);
                        SelectObject(dc, hOldBrush);
                        DeleteObject(brush);
                        ReleaseDC(HWND.NULL, dc);
                        break;


                    case 5:
                        for (int m = 1; m >= 100; m++)
                        {
                            Point[] vertices5 = new Point[200];
                            for (int i = 1; i < vertices5.Length; i++)
                            {
                                vertices5[i] = new Point(rand.Next(w), rand.Next(h));
                            }

                            Polygon(dc, vertices5, vertices5.Length);

                            StretchBlt(dc, -1200, -5200, w + 7020, h + 7030, dc, 0, 0, w, h, SRCCOPY);
                            PlgBlt(dc, lppoint, dc, rc.left - 220, rc.top - 220, (rc.right - rc.left) + 420, (rc.bottom - rc.top) + 40, IntPtr.Zero, 0, 0);

                            ReleaseDC(HWND.NULL, dc);
                            SelectObject(dc, hOldBrush);
                            DeleteObject(brush);
                            ReleaseDC(HWND.NULL, dc);
                        }
                        break;
                }
            }

        }

        public static void Load4()
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
                unsafe
                {
                    RGBQUAD* rgbquad = (RGBQUAD*)bits;

                    double time = Environment.TickCount * 0.001;
                    double cRe = Math.Cos(time) * 0.2;
                    double cIm = Math.Sin(time) * 0.7;

                    Parallel.For(0, h, y =>
                    {
                        for (int x = 0; x < w; x++)
                        {
                            double zx = 1.5 * (x - w / 2) / (0.5 * w);
                            double zy = (y - h / 2) / (0.5 * h);
                            int iteration = 0;
                            const int maxIteration = 300;

                            while (zx * zx + zy * zy < 4 && iteration < maxIteration)
                            {
                                double temp = zx * zx - zy * zy + cRe;
                                zy = 2.0 * zx * zy + cIm;
                                zx = temp;
                                iteration++;
                            }

                            double t = (double)iteration / maxIteration;
                            byte red = (byte)(9 * (1 - t) * t * t * t * 255);
                            byte green = (byte)(32 * (1 - t) * (1 - t) * t * t * 255);
                            byte blue = (byte)(8.5 * (1 - t) * (1 - t) * (1 - t) * t * 255);

                            int index = y * w + x;
                            rgbquad[index].rgbRed = red;
                            rgbquad[index].rgbGreen = green;
                            rgbquad[index].rgbBlue = blue;
                            rgbquad[index].rgbReserved = 255;
                        }
                    });
                }

                AlphaBlend(dc, 0, 0, w, h, dcCopy, 0, 0, w, h, new BLENDFUNCTION(255));
                Sleep(10);
            }
        }


        public static void Load5()
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
                unsafe
                {
                    RGBQUAD* rgbquad = (RGBQUAD*)bits;

                    double time = Environment.TickCount * 0.001;
                    double cRe = Math.Cos(time) * 02;
                    double cIm = Math.Sin(time) * 0;

                    Parallel.For(0, h, y =>
                    {
                        for (int x = 0; x < w; x++)
                        {
                            double zx = 1.5 * (x - w / 2) / (0.5 * w);
                            double zy = (y - h / 2) / (0.5 * h);

                            double distance = Math.Sqrt(zx * zx + zy * zy);
                            int iteration = 0;
                            const int maxIteration = 300;

                            double adjustedMaxIteration = maxIteration * (1 - distance * 0.5);

                            while (zx * zx + zy * zy < 4 && iteration < adjustedMaxIteration)
                            {
                                double temp = zx * zx - zy * zy + cRe;
                                zy = 2.0 * zx * zy + cIm;
                                zx = temp;
                                iteration++;
                            }

                            double t = iteration / adjustedMaxIteration;
                            byte red = (byte)(9 * (1 - t) * t * t * t * 255);
                            byte green = (byte)(32 * (1 - t) * (1 - t) * t * t * 255);
                            byte blue = (byte)(8.5 * (1 - t) * (1 - t) * (1 - t) * t * 255);

                            int index = y * w + x;
                            rgbquad[index].rgbRed = red;
                            rgbquad[index].rgbGreen = green;
                            rgbquad[index].rgbBlue = blue;
                            rgbquad[index].rgbReserved = 255;
                        }
                    });
                }

                AlphaBlend(dc, 0, 0, w, h, dcCopy, 0, 0, w, h, new BLENDFUNCTION(255));
            }
        }


        public static void Load6()
        {
            var hWnd = GetForegroundWindow();
            var dc = GetDC((IntPtr)0);
            GetWindowRect(hWnd, out RECT rect);

            int x = rect.left;
            int y = rect.top;

            Random rand = new Random();

            for (int i = 0; i < 30; i++)
            {
                int offsetX = rand.Next(-10, 10);
                int offsetY = rand.Next(-10, 10);

                SetWindowPos(hWnd, IntPtr.Zero, x + offsetX, y + offsetY, 0, 0,
                    SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_NOZORDER | SetWindowPosFlags.SWP_SHOWWINDOW);
                BitBlt(dc, offsetX, offsetY, w, h, dc, 0, 0, RasterOperationMode.SRCCOPY);
            }

            SetWindowPos(hWnd, IntPtr.Zero, x, y, 0, 0,
                SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_NOZORDER | SetWindowPosFlags.SWP_SHOWWINDOW);

            ReleaseDC(IntPtr.Zero, dc);

            //==============================

            uint timeSleep = 800;

            while (true)
            {
                //  (ENG)
                //  I Could Have Done the Circle and Square Effects in Separate Functions, But I Got Too Lazy    :D
                //  (It's even better because that way the skidders won't understand LOL)

                //  (PTBR)
                //  Eu poderia ter feito os efeitos de círculo e quadrado em funções separadas, mas fiquei com preguiça :D
                //  (É ainda melhor porque assim os skidders não vão entender LOL)
                var hdc = GetDC(IntPtr.Zero);
                var Brush = CreateSolidBrush(new COLORREF((byte)rand.Next(255), (byte)rand.Next(255), (byte)rand.Next(255)));

                byte r = (byte)rand.Next(255);
                byte g = (byte)rand.Next(255);
                byte b = (byte)rand.Next(255);

                HBRUSH brush = CreateSolidBrush(new COLORREF(r, g, b));
                var hOldBrush = SelectObject(dc, brush);

                int centerX = rand.Next(w);
                int centerY = rand.Next(h);

                int centerX2 = rand.Next(w);
                int centerY2 = rand.Next(h);

                Ellipse(dc, centerX - rand.Next(100), centerY - rand.Next(100), centerX + rand.Next(100), centerY + rand.Next(100));
                SelectObject(dc, hOldBrush);

                Point[] vertices = new Point[4];

                for (int i = 0; i < 4; i++)
                {
                    double angle = 2 * Math.PI * i / 4;
                    vertices[i] = new Point(
                        centerX2 + (int)(rand.Next(50, 100) * Math.Cos(angle)),
                        centerY2 + (int)(rand.Next(50, 100) * Math.Sin(angle))

                    );
                }

                Polygon(dc, vertices, vertices.Length);
                SelectObject(dc, hOldBrush);

                SelectObject(hdc, Brush);
                PatBlt(hdc, 0, 0, w, h, PATINVERT);

                Sleep(timeSleep);

                if (timeSleep > 0)
                {
                    timeSleep -= 100;
                }
                else
                {
                    continue;
                }

                DeleteObject(Brush);
                DeleteDC(hdc);
            }

        }

        public static void Load7()
        {
            float time = 0.0f;

            int centerX = w / rand.Next(2, 4);
            int centerY = h / rand.Next(2, 4);

            int speedX = rand.Next(10);
            int speedY = rand.Next(10);

            while (true)
            {
                var hdc = GetDC(HWND.NULL);

                for (int i = 0; i < 30; i++)
                {
                    float wave = (float)Math.Sin(time + i * 0.3) * 50;

                    int x = centerX + (int)(Math.Cos(time + i * 0.5) * wave);
                    int y = centerY + (int)(Math.Sin(time + i * 0.5) * wave);

                    byte r = (byte)((Math.Sin(time + i * 0.1) * 127) + 128);
                    byte g = (byte)((Math.Cos(time + i * 0.2) * 127) + 128);
                    byte b = (byte)((Math.Sin(time + i * 0.3) * 127) + 128);

                    HBRUSH brush = CreateSolidBrush(new COLORREF(r, g, b));
                    SelectObject(hdc, brush);

                    Ellipse(hdc, x - 20, y - 20, x + 20, y + 20);

                    DeleteObject(brush);
                }

                centerX += speedX;
                centerY += speedY;

                if (centerX <= 50 || centerX >= w - 50) speedX = -speedX;
                if (centerY <= 50 || centerY >= h - 50) speedY = -speedY;

                time += 0.1f;

                ReleaseDC(HWND.NULL, hdc);
                Sleep(5);
            }
        }

        public static void Load8()
        {
            var dc = GetDC(IntPtr.Zero);
            var dcCopy = CreateCompatibleDC(dc);
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;

            BITMAPINFO bmpi = new BITMAPINFO();
            bmpi.bmiHeader.biSize = (uint)Marshal.SizeOf(bmpi);
            bmpi.bmiHeader.biWidth = w;
            bmpi.bmiHeader.biHeight = -h;
            bmpi.bmiHeader.biPlanes = 1;
            bmpi.bmiHeader.biBitCount = 32;
            bmpi.bmiHeader.biCompression = 0;

            IntPtr bits;
            var hBitmap = CreateDIBSection(dc, in bmpi, 0, out bits, IntPtr.Zero, 0);
            SelectObject(dcCopy, hBitmap);

            while (true)
            {
                unsafe
                {
                    RGBQUAD* rgbquad = (RGBQUAD*)bits;

                    double time = Environment.TickCount * 0.0011;
                    double scale = 1.5 + Math.Sin(time * 0.5) * 0.5;
                    double offsetX = Math.Cos(time) * 0.5;
                    double offsetY = Math.Sin(time * 0.7) * 0.5;

                    Parallel.For(0, h, y =>
                    {
                        for (int x = 0; x < w; x++)
                        {
                            double zx = 0, zy = 0;
                            double cx = (x - w / 2.0) / (0.5 * scale * w) + offsetX;
                            double cy = (y - h / 2.0) / (0.5 * scale * h) + offsetY;

                            int iteration = 0;
                            const int maxIteration = 300;

                            while (zx * zx + zy * zy < 4 && iteration < maxIteration)
                            {
                                double temp = zx * zx - zy * zy + cx;
                                zy = 2.0 * zx * zy + cy;
                                zx = temp;
                                iteration++;
                            }

                            double t = (double)iteration / maxIteration;
                            byte red = (byte)(9 * (1 - t) * t * t * t * 255);
                            byte green = (byte)(15 * (1 - t) * (1 - t) * t * t * 255);
                            byte blue = (byte)(8.5 * (1 - t) * (1 - t) * (1 - t) * t * 255);

                            int index = y * w + x;
                            rgbquad[index].rgbRed = red;
                            rgbquad[index].rgbGreen = green;
                            rgbquad[index].rgbBlue = blue;
                            rgbquad[index].rgbReserved = 255;
                        }
                    });
                }
                AlphaBlend(dc, 0, 0, w, h, dcCopy, 0, 0, w, h, new BLENDFUNCTION(255));
                Sleep(10);
            }
        }

        public static void Load9()
        {
            float time = 0.0f;
            int radiusCustom = 10;
            int direction = 1;

            while (true)
            {
                HDC hdc = GetDC(HWND.NULL);

                int centerX = (int)(w / 2 + Math.Sin(time * 0.1) * (w / 4));
                int centerY = (int)(h / 2 + Math.Cos(time * 0.1) * (h / 4));

                for (int i = 0; i < 12; i++)
                {
                    float angle = (float)(time * 0.2 + i * Math.PI / 6);
                    int radius = (int)(Math.Sin(time * 0.5) * 50 + 100);
                    int x = centerX + (int)(Math.Cos(angle) * radius);
                    int y = centerY + (int)(Math.Sin(angle) * radius);

                    byte red = (byte)((Math.Sin(time * 0.5 + i * 0.2) * 127) + 128);
                    byte green = (byte)((Math.Cos(time * 0.3 + i * 0.3) * 127) + 128);
                    byte blue = (byte)((Math.Sin(time * 0.2 + i * 0.1) * 127) + 128);

                    HBRUSH brush = CreateSolidBrush(new COLORREF(red, green, blue));
                    SelectObject(hdc, brush);

                    Ellipse(hdc, x - radiusCustom, y - radiusCustom, x + radiusCustom, y + radiusCustom);
                    DeleteObject(brush);
                }

                time += 0.1f;

                ReleaseDC(HWND.NULL, hdc);
                Sleep(11);

                radiusCustom += direction;
                if (radiusCustom >= 50 || radiusCustom <= 10)
                    direction *= -1;
            }
        }

        public static void Load10()
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

            List<(double x, double y, double radius)> metaBalls = new List<(double, double, double)>
            {
                (w / 3.0, h / 3.0, 80),  // Boll 1
                (w / 1.5, h / 2.0, 80),  // Boll 2
                (w / 2.0, h / 1.5, 100)  // Boll 3
            };

            double speedX1 = 50, speedY1 = 50;
            double speedX2 = -50, speedY2 = 50;
            double speedX3 = 50, speedY3 = -50;

            double colorShift1 = 0, colorShift2 = 0, colorShift3 = 0;

            while (true)
            {
                unsafe
                {
                    RGBQUAD* rgbquad = (RGBQUAD*)bits;

                    Parallel.For(0, h, y =>
                    {
                        for (int x = 0; x < w; x++)
                        {
                            double value = 0.0;

                            foreach (var (ballX, ballY, radius) in metaBalls)
                            {
                                double dx = x - ballX;
                                double dy = y - ballY;
                                double dist = Math.Sqrt(dx * dx + dy * dy);

                                double influence = radius / (dist + 1);
                                value += influence;
                            }

                            byte red = (byte)(Math.Min(value, 1.0) * 255);
                            byte green = (byte)(Math.Min(value + colorShift1, 1.0) * 255);
                            byte blue = (byte)(Math.Min(value + colorShift2, 1.0) * 255);

                            rgbquad[y * w + x].rgbRed = red;
                            rgbquad[y * w + x].rgbGreen = green;
                            rgbquad[y * w + x].rgbBlue = blue;
                            rgbquad[y * w + x].rgbReserved = 255;
                        }
                    });
                }

                metaBalls[0] = (metaBalls[0].x + speedX1, metaBalls[0].y + speedY1, metaBalls[0].radius);
                metaBalls[1] = (metaBalls[1].x + speedX2, metaBalls[1].y + speedY2, metaBalls[1].radius);
                metaBalls[2] = (metaBalls[2].x + speedX3, metaBalls[2].y + speedY3, metaBalls[2].radius);

                colorShift1 += 0.02;
                colorShift2 += 0.03;
                colorShift3 += 0.01;

                if (colorShift1 > 1) colorShift1 = 0;
                if (colorShift2 > 1) colorShift2 = 0;
                if (colorShift3 > 1) colorShift3 = 0;

                if (metaBalls[0].x < 0 || metaBalls[0].x > w) speedX1 = -speedX1;
                if (metaBalls[0].y < 0 || metaBalls[0].y > h) speedY1 = -speedY1;
                if (metaBalls[1].x < 0 || metaBalls[1].x > w) speedX2 = -speedX2;
                if (metaBalls[1].y < 0 || metaBalls[1].y > h) speedY2 = -speedY2;
                if (metaBalls[2].x < 0 || metaBalls[2].x > w) speedX3 = -speedX3;
                if (metaBalls[2].y < 0 || metaBalls[2].y > h) speedY3 = -speedY3;

                AlphaBlend(dc, 0, 0, w, h, dcCopy, 0, 0, w, h, new BLENDFUNCTION(255));

                Sleep(66);
            }
        }

    }
}
