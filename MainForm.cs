using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hexer
{
    public partial class MainForm : Form, IMessageFilter
    {
        public MainForm()
        {
            InitializeComponent();
            Application.AddMessageFilter(this);
            Refresh();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fd = new OpenFileDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                hexView.FileName = fd.FileName;
            }
        }

        #region Mousewheel
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_MOUSEWHEEL)
            {
                // WM_MOUSEWHEEL, find the control at screen position m.LParam
                var hWnd = WindowFromPoint(Cursor.Position);

                if (hWnd != IntPtr.Zero && hWnd != m.HWnd && Control.FromHandle(hWnd) != null)
                {
                    SendMessage(hWnd, WM_MOUSEWHEEL, m.WParam, m.LParam);
                    return true;
                }
            }
            return false;
        }

        // P/Invoke declarations
        private const Int32 WM_MOUSEWHEEL = 0x20a;
        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(Point pt);
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        #endregion

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var gt = new GoToForm();
            if (gt.ShowDialog() == DialogResult.OK)
            {
                hexView.NavigateToAddress(gt.Address);
            }
        }
        private void goToSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hexView.NavigateToAddress();
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sf = new SearchForm();
            if (sf.ShowDialog() == DialogResult.OK)
            {
                if (hexView.Search(sf.ToSearch))
                {
                    statusStrip.Text = "Found search data";
                }
                else
                {
                    statusStrip.Text = "No occurance found";
                }
            }
        }

        private void hexView_SelectedAddressChanged(object sender, EventArgs e)
        {
            selectedAddressInspector.Target = hexView.GetSelectedData();
        }

        private void hexView_HoverAddressChanged(object sender, EventArgs e)
        {
            hoverAddressInspector.Target = hexView.GetHoverData();
        }
    }
}

