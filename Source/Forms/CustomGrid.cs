using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EbayMaster
{
    public class CustomGrid : System.Windows.Forms.DataGridView
    {
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                EndEdit();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}
