using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace hexer
{
    public partial class SearchForm : Form
    {
        static readonly string SEARCH_TYPE_KEY = "SearchType";

        public DataFragment ToSearch {
            get;
            internal set;
        }

        public SearchForm()
        {
            InitializeComponent();

            dtComboBox.Items.AddRange(DataType.GetKnownDataTypes().ToArray());
            string dts = Program.regKey.GetValue(SEARCH_TYPE_KEY, "int8") as string;
            dtComboBox.SelectedIndex = dtComboBox.FindString(dts);
        }

        private void dtComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (dtComboBox.SelectedIndex != -1) Program.regKey.SetValue(SEARCH_TYPE_KEY, dtComboBox.SelectedItem.ToString());
        }

        private void valueTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (dtComboBox.SelectedIndex > 0) dtComboBox.SelectedIndex--;
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (dtComboBox.SelectedIndex < dtComboBox.Items.Count-1) dtComboBox.SelectedIndex++;
            }
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            ToSearch = (dtComboBox.Items[dtComboBox.SelectedIndex] as DataType).EncodeString(0, valueTextBox.Text);
        }
    }
}
