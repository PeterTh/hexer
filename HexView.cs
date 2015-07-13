using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace hexer
{
    public partial class HexView : UserControl
    {
        const int WHEEL_DELTA = 120;

        private byte[] fileBytes = null;
        private Font cFont = new Font("Consolas", 11);
        private Font tFont = new Font("Tahoma", 6);
        private Font vFont = new Font("Segoe UI", 9);

        private float hSpacing = 1.0f, vSpacing = 0.0f;
        private float xStart, yStart;
        private SizeF byteSize;

        private int startLine = 0, totalLines = 1;
        private int StartLine
        {
            get { return startLine; }
            set
            {
                if (value < 0) startLine = 0;
                else if (value >= totalLines - 1) startLine = totalLines - 1;
                else startLine = value;
                vScrollBar.Value = startLine;
                Refresh();
            }
        }

        private int visibleLines = 1;
        private Interval<int> visibleAddresses = new Interval<int>(0, 0);

        private string fileName = @"E:\Steam\userdata\31474140\251150\remote\fc\SVDAT149.SAV";
        [Description("Hex file name"), Category("Hex")]
        public string FileName
        {
            get { return fileName; }
            set
            {
                fileName = value;
                if (fileName.Length > 0) LoadFile(fileName);
            }
        }

        private int numBytesInLine = 32;
        [Description("Number of bytes shown in a line"), Category("Hex")]
        public int NumBytesInLine
        {
            get { return numBytesInLine; }
            set { numBytesInLine = value; }
        }

        [Description("Triggered when hover address changes"), Category("Hex")]
        public event EventHandler HoverAddressChanged = delegate { };
        private int hoverAddress = -1;
        public int HoverAddress
        {
            get { return hoverAddress; }
            set
            {
                hoverAddress = value;
                Refresh();
                HoverAddressChanged(this, new EventArgs());
            }
        }

        [Description("Triggered when selected address changes"), Category("Hex")]
        public event EventHandler SelectedAddressChanged = delegate { };
        private int selectedAddress = -1;
        public int SelectedAddress
        {
            get { return selectedAddress; }
            set
            {
                selectedAddress = value;
                Refresh();
                SelectedAddressChanged(this, new EventArgs());
            }
        }

        private float LineHeight
        { get { return byteSize.Height + vSpacing; } }
        private float ColumnWidth
        { get { return byteSize.Width + hSpacing; } }

        public HexView()
        {
            InitializeComponent();

            foreach (var dt in DataType.GetKnownDataTypes())
            {
                var markAs = new ToolStripMenuItem(dt.Name);
                markAs.Click += MarkAs_Click;
                markAs.Tag = dt;
                markAsToolStripMenuItem.DropDownItems.Add(markAs);
            }

            SelectedAddress = -1;
            HoverAddress = -1;

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserMouse, true);
            ComputeMetrics();
        }

        private void MarkAs_Click(object sender, EventArgs e)
        {
            var ts = sender as ToolStripMenuItem;
            var dt = ts.Tag as DataType;
            MarkerRepository.Instance.AddMarker(SelectedAddress, dt);
        }

        private void ComputeMetrics()
        {
            var g = CreateGraphics();
            byteSize = g.MeasureString("00", cFont);
            xStart = g.MeasureString("0x00000000", cFont).Width + hSpacing * 3;
            yStart = 1.0f;
        }
        private void ComputeVisible()
        {
            visibleLines = (int)Math.Floor((Height - yStart) / LineHeight);
            visibleAddresses.Min = StartLine * 8 * numBytesInLine;
            visibleAddresses.Max = visibleAddresses.Min + visibleLines * numBytesInLine * 8;
        }

        private void LoadFile(string fileName)
        {
            fileBytes = File.ReadAllBytes(fileName);
            totalLines = fileBytes.Length / NumBytesInLine;
            vScrollBar.Minimum = 0;
            vScrollBar.Maximum = totalLines;
            vScrollBar.Value = 0;
            SelectedAddress = -1;
            HoverAddress = -1;
            Refresh();
        }

        public void SaveToFile(string fileName)
        {
            this.fileName = fileName;
            File.WriteAllBytes(fileName, fileBytes);
        }
        public void SaveToFile()
        {
            File.WriteAllBytes(fileName, fileBytes);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.FillRectangle(Brushes.Black, e.ClipRectangle);

            var hoverLoc = GetLocationOfAddress(hoverAddress);

            int skipAddresses = 0;
            if (fileBytes != null)
            {
                float y = yStart;
                int line = StartLine;
                while (y < e.ClipRectangle.Bottom)
                {
                    int lineAddress = numBytesInLine * line * 8;
                    var addrString = "0x" + Convert.ToString(lineAddress, 16).PadLeft(8, '0').ToUpper();
                    g.DrawString(addrString, cFont, Brushes.Aqua, new PointF(hSpacing, y));

                    for (int i = 0; i < numBytesInLine; ++i)
                    {
                        int pos = i + numBytesInLine * line;
                        if (pos >= fileBytes.Length) break;

                        int address = pos * 8;
                        var point = new PointF(xStart + i * ColumnWidth, y);

                        // in a marker, draw line and skip rest
                        if (skipAddresses > 0)
                        {
                            skipAddresses--;
                            point.Y += 15;
                            var point2 = new PointF(point.X + ColumnWidth, point.Y);
                            if (skipAddresses == 0) point2.X -= hSpacing;
                            g.DrawLine(new Pen(Brushes.DarkOrange), point, point2);
                            continue;
                        }

                        // handle markers
                        var marker = MarkerRepository.Instance.GetMarker(address);
                        if (marker != null)
                        {
                            var drawPoint = new PointF(point.X, point.Y);
                            drawPoint.Y -= 2;
                            g.DrawString(marker.Type.DecodeToString(new DataFragment(address, fileBytes, pos, marker.NumBytes)), vFont, Brushes.White, drawPoint);
                            drawPoint.Y += 13;
                            g.DrawString(marker.Type.ShortName, tFont, Brushes.Orange, drawPoint);
                            skipAddresses = marker.NumBytes - 1;
                            // draw line
                            var measure = g.MeasureString(marker.Type.ShortName, tFont);
                            drawPoint.X += measure.Width + hSpacing;
                            drawPoint.Y = point.Y + 15;
                            var point2 = new PointF(point.X + ColumnWidth, drawPoint.Y);
                            g.DrawLine(new Pen(Brushes.DarkOrange), drawPoint, point2);
                            continue;
                        }

                        // highlighting and selection
                        if (address == hoverAddress) g.FillRectangle(Brushes.Blue, new RectangleF(point, byteSize));
                        else if (hoverAddress > 0 && address > hoverAddress && address - hoverAddress < 8 * 8)
                            g.FillRectangle(Brushes.DarkBlue, new RectangleF(point, byteSize));
                        if (address == selectedAddress) g.DrawRectangle(new Pen(Brushes.Red, 1.0f), Rectangle.Round(new RectangleF(point, byteSize)));
                        else if (selectedAddress > 0 && address > selectedAddress && address - selectedAddress < 8 * 8)
                            g.DrawRectangle(new Pen(Brushes.DarkRed, 1.0f), Rectangle.Round(new RectangleF(point, byteSize)));

                        // handle normal bytes
                        var byt = fileBytes[pos];
                        var byteString = Convert.ToString(byt, 16).PadLeft(2, '0').ToUpper();
                        var brush = Brushes.LightGray;
                        if (byt == 0) brush = Brushes.Gray;
                        g.DrawString(byteString, cFont, brush, point);
                    }

                    line++;
                    y += LineHeight;
                }
            }
        }

        public override void Refresh()
        {
            base.Refresh();
            ComputeVisible();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            int offset = e.Delta / WHEEL_DELTA;
            if (Control.ModifierKeys.HasFlag(Keys.Control)) offset *= 15;
            StartLine -= offset;
        }

        // Terrible hack to fix initial form drawing 
        private int formInited = 0;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            var p = new Point(e.X, e.Y);
            HoverAddress = GetAddressAt(p);
            if (formInited < 2)
            {
                FindForm().Refresh();
                ++formInited;
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            var p = new Point(e.X, e.Y);
            SelectedAddress = GetAddressAt(p);
            if (e.Button == MouseButtons.Right) contextMenuStrip.Show(PointToScreen(p));
        }

        private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            StartLine = vScrollBar.Value;
        }

        public PointF GetLocationOfAddress(int addr)
        {
            int startAddr = StartLine * 8 * numBytesInLine;
            addr -= startAddr;
            int xidx = (addr / 8) % numBytesInLine;
            int yidx = (addr / 8) / numBytesInLine;
            return new PointF(xStart + xidx * ColumnWidth, yStart + yidx * LineHeight);
        }

        public int GetAddressAt(Point loc)
        {
            int address = numBytesInLine * StartLine * 8; // start
            address += (int)(Math.Max(0, (loc.Y - yStart)) / LineHeight) * numBytesInLine * 8; // lines
            int xoffset = (int)(Math.Max(0, (loc.X - xStart)) / ColumnWidth) * 8; // x position
            address += Math.Min(xoffset, (numBytesInLine - 1) * 8);
            return address;
        }

        public DataFragment GetDataAt(Point loc)
        {
            var addr = GetAddressAt(loc);
            return new DataFragment(addr, fileBytes, addr / 8);
        }

        public DataFragment GetDataAt(int address)
        {
            if (address < 0 || address >= fileBytes.Length * 8) return new DataFragment();
            return new DataFragment(address, fileBytes, address / 8);
        }
        public DataFragment GetSelectedData() { return GetDataAt(SelectedAddress); }
        public DataFragment GetHoverData() { return GetDataAt(HoverAddress); }

        public void NavigateToAddress(int address)
        {
            if (address < 0) address = 0;
            if (address / 8 >= fileBytes.Length) address = fileBytes.Length - 1;
            SelectedAddress = address;

            if (!visibleAddresses.Contains(SelectedAddress))
            {
                StartLine = ((address / 8) / numBytesInLine) - (visibleLines / 2);
            }
        }
        internal void NavigateToAddress()
        {
            NavigateToAddress(SelectedAddress);
        }

        private static bool IsSubArrayEqual(byte[] x, byte[] y, int start)
        {
            for (int i = 0; i < y.Length; i++)
            {
                if (x[start++] != y[i]) return false;
            }
            return true;
        }
        public static int StartingIndex(byte[] x, byte[] y)
        {
            int max = 1 + x.Length - y.Length;
            for (int i = 0; i < max; i++)
            {
                if (IsSubArrayEqual(x, y, i)) return i;
            }
            return -1;
        }

        internal bool Search(DataFragment toSearch)
        {
            byte[] target = new byte[toSearch.Length];
            Array.Copy(toSearch.Data, target, toSearch.Length);
            int idx = StartingIndex(fileBytes, target);
            if (idx >= 0)
            {
                NavigateToAddress(idx * 8);
            }
            return idx >= 0;
        }

        internal void ApplyEdit(DataFragment data)
        {
            Array.Copy(data.Data, 0, fileBytes, data.Address / 8, data.Length);
            NavigateToAddress();
        }
    }
}
