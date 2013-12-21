using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EbayMaster
{
    public partial class FrmProgress : Form
    {
        private bool m_Cancel = false;

        public bool Cancel
        {
            get { return m_Cancel; }
        }

        public FrmProgress()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.buttonCancel.Enabled = false;
            this.buttonCancel.Text = "正在取消...";
        }

        private void FrmProgress_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If the user clicks the X or hit's Alt+F4 consider this a cancel
            // but don't let the form close, otherwise we can't check the Cancel
            // property because the form will be null.
            m_Cancel = true;
            e.Cancel = true;
        }

        public void SetLabelHintAndProgressBarValue(String LabelHintStr, int progressBarVal)
        {
            labelHint.Invoke(
                (MethodInvoker)delegate()
                {
                    labelHint.Text = LabelHintStr;
                    progressBar.Value = progressBarVal;
                }
            );
        }   // SetLabelHintAndProgressBarValue
    }
}
