using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EbayMaster
{
    public class ItemStockInNoteDAL
    {
        public static int GetItemStockInNoteCount()
        {
            int count = 0;

            String sql = "select count(*) from [ItemStockInNote]";
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql);

            try
            {
                object obj = dt.Rows[0][0];
                if (obj != null)
                    Int32.TryParse(obj.ToString(), out count);
            }
            catch (System.Exception)
            {

            }
            return count;
        }

        public static DataTable GetAllItemStockInNotes()
        {
            String sql = "select * from ItemStockInNote";
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql);
            return dt;
        }

        public static DataTable GetPagedItemStockInNotes(int pageNum, int pageSize)
        {
            String pagedFormatSql = "select * from [ItemStockInNote] where NoteId in (select top {0} sub.NoteId from ("
                + " select top {1} NoteId from [ItemStockInNote] order by NoteId desc) [sub] order by sub.NoteId) order by NoteId desc";
            String pagedSql = String.Format(pagedFormatSql, pageSize, pageNum * pageSize);
            DataTable dt = DataFactory.ExecuteSqlReturnTable(pagedSql);
            return dt;
        }

        private static ItemStockInNoteType GetItemStockInNoteFromDataRow(DataRow dr)
        {
            ItemStockInNoteType note = new ItemStockInNoteType();
            note.NoteId = StringUtil.GetSafeInt(dr["NoteId"]);
            note.ItemSKU = StringUtil.GetSafeString(dr["ItemSKU"]);
            note.ItemTitle = StringUtil.GetSafeString(dr["ItemTitle"]);
            note.SourcingNoteId = StringUtil.GetSafeString(dr["SourcingNoteId"]);
            note.StockInNum = StringUtil.GetSafeInt(dr["StockInNum"]);
            note.StockInDate = StringUtil.GetSafeDateTime(dr["StockInDate"]);
            note.Comment = StringUtil.GetSafeString(dr["Comment"]);
            return note;
        }

        public static ItemStockInNoteType GetOneItemStockInNote(String noteId)
        {
            String sql = String.Format("select * from [ItemStockInNote] where NoteId={0}", noteId);
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql);
            if (dt == null || dt.Rows.Count == 0)
                return null;

            return GetItemStockInNoteFromDataRow(dt.Rows[0]);
        }

        public static int InsertOneItemStockInNote(ItemStockInNoteType note)
        {
            IDbCommand cmd = DataFactory.CreateCommand(null);
            cmd.CommandText = @"Insert into [ItemStockInNote] (ItemSKU, ItemTitle, SourcingNoteId, StockInNum, StockInDate, Comment) values"
                    + "(@ItemSKU, @ItemTitle, @SourcingNoteId, @StockInNum, @StockInDate, @Comment)";

            DataFactory.AddCommandParam(cmd, "@ItemSKU", DbType.String, note.ItemSKU);
            DataFactory.AddCommandParam(cmd, "@ItemTitle", DbType.String, note.ItemTitle);
            DataFactory.AddCommandParam(cmd, "@SourcingNoteId", DbType.String, note.SourcingNoteId);
            DataFactory.AddCommandParam(cmd, "@StockInNum", DbType.Int32, note.StockInNum);
            DataFactory.AddCommandParam(cmd, "@StockInDate", DbType.DateTime, note.StockInDate.ToShortDateString());
            DataFactory.AddCommandParam(cmd, "@Comment", DbType.String, note.Comment);

            int newId = 0;

            try
            {
                if (DataFactory.DbConnection.State == ConnectionState.Closed)
                    DataFactory.DbConnection.Open();
                cmd.ExecuteNonQuery();

                IDbCommand cmdNewID = DataFactory.CreateCommand("SELECT @@IDENTITY");
                // Retrieve the Autonumber and store it in the CategoryID column.
                object obj = cmdNewID.ExecuteScalar();
                Int32.TryParse(obj.ToString(), out newId);
            }
            catch (DataException)
            {
                // Write to log here.
            }
            finally
            {
                if (DataFactory.DbConnection.State == ConnectionState.Open)
                    DataFactory.DbConnection.Close();
            }

            return newId;
        }

        public static bool ModifyOneItemStockInNote(ItemStockInNoteType note)
        {
            bool result = false;

            if (note == null || note.NoteId <= 0)
                return false;

            IDbCommand cmd = DataFactory.CreateCommand(null);
            cmd.CommandText = @"Update [ItemStockInNote] set ItemSKU=@ItemSKU, ItemTitle=@ItemTitle, SourcingNoteId=@SourcingNoteId, "
                    + "StockInNum=@StockInNum, StockInDate=@StockInDate, Comment=@Comment where NoteId=@NoteId";

            DataFactory.AddCommandParam(cmd, "@ItemSKU", DbType.String, note.ItemSKU);
            DataFactory.AddCommandParam(cmd, "@ItemTitle", DbType.String, note.ItemTitle);
            DataFactory.AddCommandParam(cmd, "@SourcingNoteId", DbType.String, note.SourcingNoteId);
            DataFactory.AddCommandParam(cmd, "@StockInNum", DbType.Int32, note.StockInNum);
            DataFactory.AddCommandParam(cmd, "@StockInDate", DbType.DateTime, note.StockInDate);
            DataFactory.AddCommandParam(cmd, "@Comment", DbType.String, note.Comment);

            DataFactory.AddCommandParam(cmd, "@NoteId", DbType.Int32, note.NoteId);

            try
            {
                if (DataFactory.DbConnection.State == ConnectionState.Closed)
                    DataFactory.DbConnection.Open();
                cmd.ExecuteNonQuery();
                result = true;

            }
            catch (DataException)
            {
                // Write to log here.
                result = false;
            }
            finally
            {
                if (DataFactory.DbConnection.State == ConnectionState.Open)
                    DataFactory.DbConnection.Close();
            }

            return result;
        }

        public static bool DeleteOneItemStockInNote(String noteId)
        {
            bool result = false;

            ItemStockInNoteType note = ItemStockInNoteDAL.GetOneItemStockInNote(noteId);
            if (note == null)
                return false;

            String sql = string.Format("delete from [ItemStockInNote] where NoteId={0}", noteId);
            DataFactory.ExecuteSql(sql);
            result = true;

            return result;
        }
    }
}
