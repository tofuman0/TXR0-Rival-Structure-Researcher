using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TXR0_Rival_Structure_Researcher
{
    public partial class LabelEdit : Form
    {
        public LabelEdit(String labelName)
        {
            InitializeComponent();
            textBoxName.Text = labelName;
        }

        public String getName()
        {
            return textBoxName.Text;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
