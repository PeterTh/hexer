using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hexer
{
    public partial class MarkerEditor : Form
    {
        private DataMarker marker;
        private HexView hexview;

        public MarkerEditor(DataMarker marker, HexView hexview)
        {
            InitializeComponent();
            this.marker = marker;
            this.hexview = hexview;

            markerAtTextBox.Text = DataType.AddressToString(marker.Address);
            noteTextBox.Text = marker.Note;
            dataTypeComboBox.Items.AddRange(DataType.GetKnownDataTypes().ToArray());
            dataTypeComboBox.SelectedIndex = dataTypeComboBox.FindString(marker.Type.Name);
            sizeNumericUpDown.Value = marker.NumBytes;
            sizeNumericUpDown.ReadOnly = !marker.Type.VariableNumBytes;
            valueTextBox.Text = marker.Type.DecodeToString(hexview.GetDataAt(marker.Address));
        }
    }
}
