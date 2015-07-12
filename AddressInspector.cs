using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hexer
{
    public partial class AddressInspector : UserControl
    {
        public delegate void EditedData(DataFragment df);
        [Description("Fired when data is edited"), Category("Hex")]
        public event EditedData DataChanged = delegate { };

        private string caption = "Memory";
        [Description("Caption"), Category("Hex")]
        public string Caption
        {
            get { return caption; }
            set
            {
                caption = value;
                addressLabel.Text = caption + " address:";
            }
        }

        private bool editable = true;
        [Description("Is value editable?"), Category("Hex")]
        public bool Editable {
            get { return editable; }
            set
            {
                editable = value;
                foreach(var c in Controls)
                {
                    if(c is TextBox && c != addressTextBox)
                    {
                        var ct = c as TextBox;
                        ct.ReadOnly = !editable;
                    }
                }
            }
        }

        private DataFragment target = null;
        [Description("Target memory fragment"), Category("Hex")]
        public DataFragment Target
        {
            get { return target; }
            set
            {
                target = value;
                if(target != null) RecomputeStrings();
            }
        }

        private void RecomputeStrings()
        {
            using (new SuspendDrawing(this))
            {
                addressTextBox.Text = "0x" + Convert.ToString(target.Address, 16).PadLeft(8, '0').ToUpper();
                foreach (var dt in DataType.GetKnownDataTypes())
                {
                    var tb = Controls[dt.Name + "TextBox"] as TextBox;
                    tb.Text = dt.DecodeToString(target);
                }
            }
            Refresh();
        }

        public AddressInspector()
        {
            InitializeComponent();

            SuspendLayout();
            
            int y = 29;
            int lblX = 5;
            int textBoxX = 50;
            foreach (var dt in DataType.GetKnownDataTypesAndSeperators())
            {
                if (dt.Seperator)
                {
                    var lbl = new Label();
                    lbl.AutoSize = true;
                    lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl.ForeColor = System.Drawing.SystemColors.GrayText;
                    lbl.Location = new System.Drawing.Point(textBoxX, y);
                    lbl.Name = dt.Name.ToLower() + "Label";
                    lbl.Text = dt.Name;
                    lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    Controls.Add(lbl);
                    y += 15;
                }
                else
                {
                    // text box
                    var textBox = new TextBox();
                    textBox.Anchor |= System.Windows.Forms.AnchorStyles.Right;
                    textBox.Location = new System.Drawing.Point(textBoxX, y);
                    textBox.Name = dt.Name + "TextBox";
                    textBox.Size = new System.Drawing.Size(208, 20);
                    textBox.KeyDown += TextBox_KeyDown;
                    textBox.Tag = dt;
                    textBox.AcceptsReturn = false;
                    Controls.Add(textBox);
                    // label
                    var lbl = new Label();
                    lbl.AutoSize = false;
                    lbl.Location = new System.Drawing.Point(lblX, y+3);
                    lbl.Name = dt.Name + "Label";
                    lbl.Size = new System.Drawing.Size(textBoxX-lblX-4, 13);
                    lbl.Text = dt.Name;
                    lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                    Controls.Add(lbl);
                    y += 20;
                }
            }
            
            ResumeLayout(false);
            PerformLayout();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var tb = sender as TextBox;
                var dt = tb.Tag as DataType;

                var editedFragment = dt.EncodeString(target.Address, tb.Text);
                editedFragment.Length = dt.NumBytes;
                DataChanged(editedFragment);
            }
        }
    }
}
