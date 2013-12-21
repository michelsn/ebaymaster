using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;

namespace EbayMaster
{
    partial class FrmMain
    {
        #region Setup active listing data grid columns

        private const int ActiveListing_ItemIdColIdx = 0;
        private const int ActiveListing_ListingTypeColIdx = 4;
        private const int ActiveListing_BidCntColIdx = 7;

        private void SetupActiveListingDataGridViewColumns()
        {
            this.dataGridViewActiveListing.AutoGenerateColumns = false;
            this.dataGridViewActiveListing.AllowUserToAddRows = false;

            // ItemID
            this.dataGridViewActiveListing.Columns.Add(
               DgvUtil.createDgvTextBoxColumn("ItemID", "ItemID", typeof(String), 10, false));

            // SellerName
            this.dataGridViewActiveListing.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("SellerName", "卖家ID", typeof(String), 70, true));

            // GalleryURL
            this.dataGridViewActiveListing.Columns.Add(
                DgvUtil.createDgvImageColumn("Img", "图片", 60, true));

            // Title
            this.dataGridViewActiveListing.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("Title", "物品名称", typeof(String), 320, true));

            // ListingType
            this.dataGridViewActiveListing.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("ListingType", "上架类型", typeof(String), 80, true));

            // Quantity
            this.dataGridViewActiveListing.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("Quantity", "总数", typeof(String), 40, true));

            // QuantityAvailable
            this.dataGridViewActiveListing.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("QuantityAvailable", "剩余数", typeof(String), 70, true));

            // QuantityBid
            this.dataGridViewActiveListing.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("BidCount", "竞拍数", typeof(String), 70, true));

            // StartPrice
            this.dataGridViewActiveListing.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("StartPrice", "起始价", typeof(String), 70, true));

            // CurrentPrice
            this.dataGridViewActiveListing.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("CurrentPrice", "当前价", typeof(String), 70, true));

            // TimeLeft
            this.dataGridViewActiveListing.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("TimeLeft", "剩余时间", typeof(String), 100, true));

            // WatchCount
            this.dataGridViewActiveListing.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("WatchCount", "关注度", typeof(String), 70, true));

            this.dataGridViewActiveListing.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewActiveListing.MultiSelect = true;
            this.dataGridViewActiveListing.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        #endregion

        #region FrmMain active listing related event handlers

        #endregion

        private void SyncEbayData_ActiveListingTab(object sender, EventArgs e)
        {
            this.buttonSyncEbayData.Enabled = false;

            //
            // For simplicity, we will delete all active listings before updating.
            //
            EbayListingDAL.DeleteAllListings();

            List<AccountType>   allAccounts = AccountUtil.GetAllAccounts();
            foreach (AccountType account in allAccounts)
            {
                List<EbayActiveListingType> activeListings = EbaySellingBiz.GetMyActiveListing(account);

                if (activeListings == null)
                    continue;

                foreach (EbayActiveListingType activeListing in activeListings)
                {
                    EbayListingDAL.InsertOrUpdateOneActiveListing(activeListing);
                }
            }

            this.buttonSyncEbayData.Enabled = true;
        }

        private void LoadListingImageData()
        {
            DataTable dtActiveSelling = AllListingCacheTable; 
            {
                dtActiveSelling.Columns.Add(new DataColumn("Img", typeof(Bitmap)));
                Bitmap bitmap = new Bitmap(64, 42);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.DrawString("Loading...", this.Font, new SolidBrush(Color.Black), 0f, 0f);
                }
                foreach (DataRow dr in dtActiveSelling.Rows)
                {
                    dr["Img"] = bitmap;
                }

            }
            this.dataGridViewActiveListing.DataSource = dtActiveSelling;
            ThreadPool.QueueUserWorkItem(delegate
            {
                foreach (DataRow row in dtActiveSelling.Rows)
                {
                    String imagePath = StringUtil.GetSafeString(row["ImagePath"]);

                    // Check whether image really exists.
                     if (imagePath != "" && System.IO.File.Exists(imagePath))
                    {
                        System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(imagePath);
                        row["Img"] = bmp;
                    }
                    else
                    {
                        HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(row["GalleryURL"].ToString());
                        myRequest.Method = "GET";
                        HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                        System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(myResponse.GetResponseStream());
                        myResponse.Close();

                        String listingId = StringUtil.GetSafeString(row["ListId"]);
                        String itemID = StringUtil.GetSafeString(row["ItemID"]);
                        String folderPath = Application.StartupPath + String.Format("\\listings");
                        if (!System.IO.Directory.Exists(folderPath))
                            System.IO.Directory.CreateDirectory(folderPath);
                        String filePath = Application.StartupPath + String.Format("\\listings\\{0}.jpg", itemID);

                        if (!System.IO.File.Exists(filePath))
                            bmp.Save(filePath);

                        EbayListingDAL.UpdateActingListImagePath(listingId, filePath);

                        row["Img"] = bmp;
                    }
                }
            });
        }

        private void dataGridViewActiveListing_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
                return;

            for (int rowIdx = 0; rowIdx < this.dataGridViewActiveListing.Rows.Count; ++rowIdx)
            {
                DataGridViewCell listingTypeCell = this.dataGridViewActiveListing.Rows[rowIdx].Cells[ActiveListing_ListingTypeColIdx];
                if (listingTypeCell.Value == null)
                    continue;

                String listingType = listingTypeCell.Value.ToString();
                if (listingType != "Auction")
                    continue;

                DataGridViewCell bidCntCell = this.dataGridViewActiveListing.Rows[rowIdx].Cells[ActiveListing_BidCntColIdx];
                if (bidCntCell.Value == null)
                    continue;

                int bidCnt = 0;
                if (!Int32.TryParse(bidCntCell.Value.ToString(), out bidCnt))
                    continue;

                if (bidCnt > 0)
                    this.dataGridViewActiveListing.Rows[rowIdx].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#90EE90");
            }
        }

        private void buttonListingFirstPage_Click(object sender, EventArgs e)
        {
            CurrentListingPage = 1;
            LoadActiveListingData();
        }

        private void buttonListingPrevPage_Click(object sender, EventArgs e)
        {
            CurrentListingPage -= 1;
            LoadActiveListingData();
        }

        private void buttonListingNextPage_Click(object sender, EventArgs e)
        {
            CurrentListingPage += 1;
            LoadActiveListingData();
        }

        private void buttonListingLastPage_Click(object sender, EventArgs e)
        {
            CurrentListingPage = ListingCount / ListingPageSize + 1;
            LoadActiveListingData();
        }
    }
}