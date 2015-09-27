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
            sizeNumericUpDown.Enabled = marker.Type.VariableNumBytes;
            valueTextBox.Text = marker.Type.DecodeToString(hexview.GetDataAt(marker.Address));
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            marker.Note = noteTextBox.Text;
            marker.Type = DataType.FromString(dataTypeComboBox.Text);
            sizeNumericUpDown.Enabled = marker.Type.VariableNumBytes;
            marker.NumBytes = (int)sizeNumericUpDown.Value;

            hexview.ApplyEdit(marker.Type.EncodeString(marker.Address, valueTextBox.Text));
        }

        private void dataTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            marker.Type = DataType.FromString(dataTypeComboBox.Text);
            sizeNumericUpDown.Value = marker.Type.NumBytes;
            sizeNumericUpDown.Enabled = marker.Type.VariableNumBytes;
            valueTextBox.Text = marker.Type.DecodeToString(hexview.GetDataAt(marker.Address));
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            applyButton_Click(sender, e);
            Close();
        }
    }
}
