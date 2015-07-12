using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hexer
{
    public partial class GoToForm : Form
    {
        public int Address { get; internal set; }

        public GoToForm()
        {
            InitializeComponent();
        }

        private void targetTextBox_TextChanged(object sender, EventArgs e)
        {
            Address = DataType.StringToAddress(targetTextBox.Text);
        }
    }
}
