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
        private Font vFont = new Font("Segoe UI", 10);

        private int hSpacing = 1, vSpacing = 0;
        private int xStart, yStart;
        private SizeF byteSize;

        private int startLine = 0, totalLines = 1;
        private int StartLine
        {
            get { return startLine; }
            set
            {
                if(value < 0) startLine = 0;
                else if(value >= totalLines - 1) startLine = totalLines - 1;
                else startLine = value;
                vScrollBar.Value = startLine;
                Refresh();
            }
        }

        private int visibleLines = 1;
        private Interval<int> visibleAddresses = new Interval<int>(0, 0);

        private string fileName = "";
        [Description("Hex file name"), Category("Hex")]
        public string FileName
        {
            get { return fileName; }
            set
            {
                if(value != null) fileName = value;
                else fileName = "";
                if(fileName.Length > 0) LoadFile(fileName);
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

        private int LineHeight
        { get { return (int)Math.Ceiling(byteSize.Height + vSpacing); } }
        private int ColumnWidth
        { get { return (int)Math.Ceiling(byteSize.Width + hSpacing); } }
        private Size CellSize
        { get { return new Size(ColumnWidth, LineHeight); } }

        public HexView()
        {
            InitializeComponent();

            foreach(var dt in DataType.GetKnownDataTypes()) {
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

        // -------------------------------------------------------------------------- Marker Handling
        #region Marker Handling
        private void MarkAs_Click(object sender, EventArgs e)
        {
            var ts = sender as ToolStripMenuItem;
            var dt = ts.Tag as DataType;
            MarkerRepository.Instance.AddMarker(SelectedAddress, dt);
        }
        private void deleteMarkerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MarkerRepository.Instance.RemoveMarker(SelectedAddress);
        }
        private void editMarkerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MarkerRepository.Instance.EditMarker(SelectedAddress, this);
        }
        #endregion

        private void ComputeMetrics()
        {
            var g = CreateGraphics();
            byteSize = g.MeasureString("00", cFont);
            xStart = (int)g.MeasureString("0x00000000", cFont).Width + hSpacing * 3;
            yStart = 1;
        }
        private void ComputeVisible()
        {
            if(LineHeight == 0) return;
            visibleLines = (Height - yStart) / LineHeight;
            visibleAddresses.Min = StartLine * 8 * numBytesInLine;
            visibleAddresses.Max = visibleAddresses.Min + visibleLines * numBytesInLine * 8;
        }

        // -------------------------------------------------------------------------- File Handling
        #region File Handling
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
        #endregion

        private Bitmap DrawMarker(DataMarker marker, int address)
        {
            // draw marker to offscreen surface
            Point origin = new Point(0, 0);
            int mWidth = ColumnWidth * marker.NumBytes;
            Bitmap markerBmp = new Bitmap(mWidth, LineHeight);
            var mg = Graphics.FromImage(markerBmp);
            var markerRect = new Rectangle(origin, new Size(mWidth, LineHeight));

            // highlighting and selection
            if(address == hoverAddress) mg.FillRectangle(Brushes.DarkBlue, markerRect);
            if(address == selectedAddress) mg.FillRectangle(Brushes.DarkRed, markerRect);

            // strings
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Near;
            mg.DrawString(marker.Type.DecodeToString(new DataFragment(address, fileBytes, address / 8, marker.NumBytes)), vFont, Brushes.White, markerRect, sf);
            origin.Y += 11;
            mg.DrawString(marker.Type.ShortName, tFont, Brushes.Orange, origin);

            // draw line
            var measure = mg.MeasureString(marker.Type.ShortName, tFont);
            origin.X += (int)measure.Width + hSpacing;
            origin.Y = LineHeight - 3;
            var point2 = new PointF(mWidth, origin.Y);
            mg.DrawLine(new Pen(Brushes.DarkOrange), origin, point2);

            return markerBmp;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.FillRectangle(Brushes.Black, e.ClipRectangle);
            
            Bitmap markerBmp = null;
            int skipAddresses = 0;
            int skippedAddresses = 0;
            if(fileBytes != null) {
                int y = yStart;
                int line = StartLine;
                while(y < e.ClipRectangle.Bottom) {
                    int lineAddress = numBytesInLine * line * 8;
                    var addrString = "0x" + Convert.ToString(lineAddress, 16).PadLeft(8, '0').ToUpper();
                    g.DrawString(addrString, cFont, Brushes.Aqua, new PointF(hSpacing, y));

                    for(int i = 0; i < numBytesInLine; ++i) {
                        int pos = i + numBytesInLine * line;
                        if(pos >= fileBytes.Length) break;

                        int address = pos * 8;
                        var point = new Point(xStart + i * ColumnWidth, y);

                        // in a marker, draw bg/line and skip rest
                        if(skipAddresses > 0) {
                            skipAddresses--;
                            skippedAddresses++;
                            // blit remaining portions of marker
                            Point origin = new Point(ColumnWidth * skippedAddresses, 0);
                            g.DrawImage(markerBmp, new Rectangle(point, CellSize), new Rectangle(origin, CellSize), GraphicsUnit.Pixel);
                            continue;
                        }

                        var marker = MarkerRepository.Instance.GetMarker(address);
                        // handle markers
                        if(marker != null) {
                            markerBmp = DrawMarker(marker, address);
                            skipAddresses = marker.NumBytes - 1;
                            skippedAddresses = 0;

                            // blit first portion of marker
                            g.DrawImage(markerBmp, new Rectangle(point, CellSize), new Rectangle(new Point(0, 0), CellSize), GraphicsUnit.Pixel);
                            continue;
                        }

                        // highlighting and selection
                        if(address == hoverAddress) g.FillRectangle(Brushes.Blue, new RectangleF(point, byteSize));
                        else if(hoverAddress >= 0 && address > hoverAddress && address - hoverAddress < 8 * 8 && !MarkerRepository.Instance.isMarker(HoverAddress))
                            g.FillRectangle(Brushes.DarkBlue, new RectangleF(point, byteSize));
                        if(address == selectedAddress) g.DrawRectangle(new Pen(Brushes.Red, 1.0f), Rectangle.Round(new RectangleF(point, byteSize)));
                        else if(selectedAddress >= 0 && address > selectedAddress && address - selectedAddress < 8 * 8 && !MarkerRepository.Instance.isMarker(SelectedAddress))
                            g.DrawRectangle(new Pen(Brushes.DarkRed, 1.0f), Rectangle.Round(new RectangleF(point, byteSize)));

                        // handle normal bytes
                        var byt = fileBytes[pos];
                        var byteString = Convert.ToString(byt, 16).PadLeft(2, '0').ToUpper();
                        var brush = Brushes.LightGray;
                        if(byt == 0) brush = Brushes.Gray;
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
            if(Control.ModifierKeys.HasFlag(Keys.Control)) offset *= 15;
            StartLine -= offset;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Application.DoEvents();
            base.OnMouseMove(e);
            var p = new Point(e.X, e.Y);
            var hAddr = GetAddressAt(p);
            var m = MarkerRepository.Instance.GetMarkerCovering(hAddr);
            if(m != null) hAddr = m.Address;
            HoverAddress = hAddr;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            var p = new Point(e.X, e.Y);
            SelectedAddress = GetAddressAt(p);
            var m = MarkerRepository.Instance.GetMarkerCovering(SelectedAddress);
            if(m != null) {
                SelectedAddress = m.Address;
                if(e.Button == MouseButtons.Right) markerMenuStrip.Show(PointToScreen(p));
            }
            else {
                if(e.Button == MouseButtons.Right) contextMenuStrip.Show(PointToScreen(p));
            }
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
            if(fileBytes == null) return new DataFragment();
            if(address < 0 || address >= fileBytes.Length * 8) return new DataFragment();
            return new DataFragment(address, fileBytes, address / 8);
        }
        public DataFragment GetSelectedData() { return GetDataAt(SelectedAddress); }
        public DataFragment GetHoverData() { return GetDataAt(HoverAddress); }
        
        public void NavigateToAddress(int address)
        {
            if(address < 0) address = 0;
            if(address / 8 >= fileBytes.Length) address = fileBytes.Length - 1;
            SelectedAddress = address;

            if(!visibleAddresses.Contains(SelectedAddress)) {
                StartLine = ((address / 8) / numBytesInLine) - (visibleLines / 2);
            }
        }
        internal void NavigateToAddress()
        {
            NavigateToAddress(SelectedAddress);
        }

        private static bool IsSubArrayEqual(byte[] x, byte[] y, int start)
        {
            for(int i = 0; i < y.Length; i++) {
                if(x[start++] != y[i]) return false;
            }
            return true;
        }

        private static int StartingIndex(byte[] x, byte[] y)
        {
            int max = 1 + x.Length - y.Length;
            for(int i = 0; i < max; i++) {
                if(IsSubArrayEqual(x, y, i)) return i;
            }
            return -1;
        }

        internal bool Search(DataFragment toSearch)
        {
            byte[] target = new byte[toSearch.Length];
            Array.Copy(toSearch.Data, target, toSearch.Length);
            int idx = StartingIndex(fileBytes, target);
            if(idx >= 0) {
                NavigateToAddress(idx * 8);
            }
            return idx >= 0;
        }

        internal void ApplyEdit(DataFragment data)
        {
            if(data.Length > 0) {
                Array.Copy(data.Data, 0, fileBytes, data.Address / 8, data.Length);
                NavigateToAddress();
            }
        }
    }
}
