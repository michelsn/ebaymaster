using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EbayMaster
{
    //
    // DataGridView control with paging functionality. 
    //
    public partial class PagedDataGridView : UserControl
    {
        // Delegate to load paged data.
        //  Only the concrete user of the paged DataGridView will know how to load data.
        public delegate DataTable GetPagedData(int pageNum, int pageSize);
        public GetPagedData getPagedData = null;

        // Data table to store the paged data.
        public DataTable dataTable = null;

        public delegate int GetRecordCount();
        public GetRecordCount getRecordCount = null;

        // Delegate to initialize the DataGridView columns.
        public delegate void InitDgvColumns();
        public InitDgvColumns initDgvColumns = null;

        public delegate void OnDgvDataBindCompleted();
        public OnDgvDataBindCompleted onDgvDataBindCompleted = null;

        private int CurrentPage = 1;
        private int PageSize = 15;

        public DataGridView DgvData
        {
            get { return dgvData;  }
        }

        public void AdjustControlsPosSize()
        {
            const int marginX = 20;
            const int marginY = 30;

            // Adjust label position
            this.labelPaging.Left = (this.Size.Width - this.labelPaging.Width) / 2;

            this.btnPrevPage.Left = this.labelPaging.Left - marginX - this.btnPrevPage.Width;
            this.btnFirstPage.Left = this.btnPrevPage.Left - marginX - this.btnFirstPage.Width;

            this.btnNextPage.Left = this.labelPaging.Left + this.labelPaging.Width + marginX;
            this.btnLastPage.Left = this.btnNextPage.Left + this.btnLastPage.Width + marginX;

            this.labelPaging.Top = this.btnLastPage.Top = this.Size.Height - marginY + 5;

            this.btnFirstPage.Top 
                = this.btnPrevPage.Top 
                = this.btnNextPage.Top 
                = this.btnLastPage.Top 
                = this.Size.Height - marginY;

            this.dgvData.Height = this.Size.Height - marginY-30;
        }

        public PagedDataGridView()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            if (getPagedData != null && getRecordCount != null)
            {
                int recordCount = getRecordCount != null ? getRecordCount() : 0;
                int pageCount = recordCount / PageSize + 1;

                if (CurrentPage < 1)
                    CurrentPage = 1;
                else if (CurrentPage > pageCount)
                    CurrentPage = pageCount;

                this.btnFirstPage.Enabled = true;
                this.btnPrevPage.Enabled = true;
                this.btnNextPage.Enabled = true;
                this.btnLastPage.Enabled = true;

                if (CurrentPage == 1)
                {
                    this.btnFirstPage.Enabled = false;
                    this.btnPrevPage.Enabled = false;
                }
                if (CurrentPage == pageCount)
                {
                    this.btnNextPage.Enabled = false;
                    this.btnLastPage.Enabled = false;
                }

                this.labelPaging.Text = String.Format("{0} / {1}", CurrentPage, pageCount);

                dataTable = getPagedData(CurrentPage, PageSize);
                this.dgvData.DataSource = dataTable;


            }
        }

        private void PagedDataGridView_Load(object sender, EventArgs e)
        {
            AdjustControlsPosSize();

            if (initDgvColumns != null)
                initDgvColumns();

            LoadData();
        }

        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            CurrentPage = 1;
            LoadData();
        }

        private void btnPrevPage_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            LoadData();
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            LoadData();
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            int recordCount = getRecordCount != null ? getRecordCount() : 0;
            CurrentPage = recordCount / PageSize + 1;
            LoadData();
        }

        private void dgvData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
                return;

            if (onDgvDataBindCompleted != null)
            {
                onDgvDataBindCompleted();
            }
        }


    }
}
