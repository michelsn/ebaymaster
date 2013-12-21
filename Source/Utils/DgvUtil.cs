using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EbayMaster
{
    public class DgvUtil
    {
        public static DataGridViewTextBoxColumn createDgvTextBoxColumn(String dataPropertyName, String headerText,
                Type valueType, int width, bool visible)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = dataPropertyName;
            column.HeaderText = headerText;
            column.ValueType = valueType;
            column.Width = width;
            //column.Frozen = true;
            column.Visible = visible;
            return column;
        }

        public static DataGridViewImageColumn createDgvImageColumn(String dataPropertyName, String headerText, int width, bool visible)
        {
            DataGridViewImageColumn column = new DataGridViewImageColumn();
            column.DataPropertyName = dataPropertyName;
            column.HeaderText = headerText;
            column.Width = width;
            column.ImageLayout = DataGridViewImageCellLayout.Normal;
            column.Visible = visible;
            return column;
        }

        public static DataGridViewCheckBoxColumn createDgvCheckBoxColumn(String dataPropertyName, String headerText,
                Type valueType, int width, bool visible)
        {
            DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
            column.DataPropertyName = dataPropertyName;
            column.HeaderText = headerText;
            column.ValueType = valueType;
            column.Width = width;
            //column.Frozen = true;
            column.Visible = visible;
            column.ReadOnly = true;
            return column;
        }

        public static DataGridViewLinkColumn createDgvLinkColumn(String dataPropertyName, String headerText,
                Type valueType, int width, bool visible)
        {
            DataGridViewLinkColumn column = new DataGridViewLinkColumn();
            column.DataPropertyName = dataPropertyName;
            column.Name = dataPropertyName;
            //column.UseColumnTextForLinkValue = true;
            //column.HeaderText = headerText;
            //column.ValueType = valueType;
            column.Width = width;
            //column.Frozen = true;
            column.Visible = visible;
            column.ReadOnly = true;
            return column;
        }
    }
}
