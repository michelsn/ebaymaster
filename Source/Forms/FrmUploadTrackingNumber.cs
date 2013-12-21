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
    public partial class FrmUploadTrackingNumber : Form
    {
        public EbayTransactionType ebayTrans = null;
        public AccountType account = null;

        public FrmUploadTrackingNumber()
        {
            InitializeComponent();
        }

        private void buttonUploadTrackingNumber_Click(object sender, EventArgs e)
        {
            if (ebayTrans == null || account == null)
                return;

            string  sellerName = ebayTrans.SellerName;
            if (null == sellerName)
                return;

            string  shippingCarrier = textBoxCarrier.Text.Trim();
            string  shipmentTrackingNumber = textBoxTrackingNumber.Text.Trim();

            if (shippingCarrier == "" || shipmentTrackingNumber == "")
            {
                MessageBox.Show("输入错误!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            EbayTransactionBiz.UploadTrackingNumber(account, ebayTrans.ItemId, ebayTrans.EbayTransactionId, 
                shippingCarrier, shipmentTrackingNumber);
            MessageBox.Show("上传跟踪号成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

            EbayTransactionDAL.UpdateTransactionShippingTrackingNo(ebayTrans.TransactionId, shipmentTrackingNumber);
        }

        private void FrmUploadTrackingNumber_Load(object sender, EventArgs e)
        {
            if (ebayTrans!= null)
            {
                textBoxTrackingNumber.Text = ebayTrans.ShippingTrackingNo;
            }
        }

    }
}
