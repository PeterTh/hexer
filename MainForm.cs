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
            selectedAddressInspector.DataChanged += SelectedAddressInspector_DataChanged;
            Refresh();
        }

        private void SelectedAddressInspector_DataChanged(DataFragment data)
        {
            hexView.ApplyEdit(data);
            selectedAddressInspector.Target = hexView.GetSelectedData();
        }

        #region Mousewheel
        public bool PreFilterMessage(ref Message m)
        {
            if(m.Msg == WM_MOUSEWHEEL) {
                // WM_MOUSEWHEEL, find the control at screen position m.LParam
                var hWnd = WindowFromPoint(Cursor.Position);

                if(hWnd != IntPtr.Zero && hWnd != m.HWnd && Control.FromHandle(hWnd) != null) {
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
            if(gt.ShowDialog() == DialogResult.OK) {
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
            if(sf.ShowDialog() == DialogResult.OK) {
                if(hexView.Search(sf.ToSearch)) {
                    statusStrip.Text = "Found search data";
                }
                else {
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

        // --------------------------------------------------------------------- File Menu
        #region File Menu
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fd = new OpenFileDialog();
            if(fd.ShowDialog() == DialogResult.OK) {
                hexView.FileName = fd.FileName;
            }
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.FileName = hexView.FileName;
            if(sfd.ShowDialog() == DialogResult.OK) {
                hexView.SaveToFile(sfd.FileName);
                toolStripStatusLabel.Text = "Saved to " + sfd.FileName;
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hexView.SaveToFile();
            toolStripStatusLabel.Text = "Saved to " + hexView.FileName;
        }
        #endregion

        // --------------------------------------------------------------------- Markers Menu
        #region Markers Menu
        private void ConfigureMarkersDialog(FileDialog fd)
        {
            fd.Filter = "Hexer marker files (*.hmf)|*.hmf";
            fd.DefaultExt = "hmf";
        }
        private void openMarkersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ConfigureMarkersDialog(ofd);
            if(ofd.ShowDialog() == DialogResult.OK) {
                MarkerRepository.Instance.LoadFromFile(ofd.FileName);
                hexView.Refresh();
                toolStripStatusLabel.Text = "Loaded Markers from " + ofd.FileName;
            }
        }
        private void saveMarkersAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            ConfigureMarkersDialog(sfd);
            if(sfd.ShowDialog() == DialogResult.OK) {
                MarkerRepository.Instance.SaveToFile(sfd.FileName);
                toolStripStatusLabel.Text = "Saved Markers to " + sfd.FileName;
            }
        }
        private void saveMarkersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MarkerRepository.Instance.HasFileName()) MarkerRepository.Instance.SaveToFile();
            else saveMarkersAsToolStripMenuItem_Click(sender, e);
        }
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
        }
    }
}

