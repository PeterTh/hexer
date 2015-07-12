using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hexer
{
    public class SuspendDrawing : IDisposable
    {
        private const int WM_SETREDRAW = 0x000B;
        private readonly Control control;
        private readonly NativeWindow window;

        public SuspendDrawing(Control control)
        {
            this.control = control;
            var msgSuspendUpdate = Message.Create(this.control.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
            window = NativeWindow.FromHandle(this.control.Handle);
            window.DefWndProc(ref msgSuspendUpdate);
        }

        public void Dispose()
        {
            var wparam = new IntPtr(1);  // Create a C "true" boolean as an IntPtr
            var msgResumeUpdate = Message.Create(control.Handle, WM_SETREDRAW, wparam, IntPtr.Zero);
            window.DefWndProc(ref msgResumeUpdate);
            control.Invalidate();
        }
    }
}
