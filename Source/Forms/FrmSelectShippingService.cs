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
    public partial class FrmSelectShippingService : Form
    {
        public KeyValuePair<string, string> SelectedShippingService;

        public FrmSelectShippingService()
        {
            InitializeComponent();
        }

        private void FrmSelectShippingService_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> ShippingService = EbayConstants.Instance.ShippingServices;
            foreach (KeyValuePair<string, string> pair in ShippingService)
            {
                this.listBoxShippingService.Items.Add(pair.Value);
            }
        }

        private void listBoxShippingService_SelectedIndexChanged(object sender, EventArgs e)
        {
            string shippingService = this.listBoxShippingService.SelectedItem.ToString();
        }

        private void buttonSelectShippingService_Click(object sender, EventArgs e)
        {
            string shippingService = this.listBoxShippingService.SelectedItem.ToString();
            foreach (KeyValuePair<string, string> pair in EbayConstants.Instance.ShippingServices)
            {
                if (pair.Value == shippingService)
                {
                    this.SelectedShippingService = pair;
                    break;
                }
            }

            this.Close();
        }

        private void listBoxShippingService_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.listBoxShippingService.SelectedItem != null)
            {
                string shippingService = this.listBoxShippingService.SelectedItem.ToString();
                foreach (KeyValuePair<string, string> pair in EbayConstants.Instance.ShippingServices)
                {
                    if (pair.Value == shippingService)
                    {
                        this.SelectedShippingService = pair;
                        break;
                    }
                }

                this.Close();
            }
        }
    }
}
