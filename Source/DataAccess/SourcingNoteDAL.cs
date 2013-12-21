using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EbayMaster
{
    public class SourcingNoteDAL
    {
        public static int GetSourceNoteCount()
        {
            int count = 0;

            String sql = "select count(*) from [SourcingNote]";
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

        public static DataTable GetAllSourcingNotes()
        {
            String sql = "select * from [SourcingNote]";
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql);
            return dt;
        }

        public static DataTable GetPagedSourcingNotes(int pageNum, int pageSize)
        {
            String pagedFormatSql = "select * from [SourcingNote] where SourcingId in (select top {0} sub.SourcingId from ("
                + " select top {1} SourcingId from [SourcingNote] order by SourcingId desc) [sub] order by sub.SourcingId) order by SourcingId desc";
            String pagedSql = String.Format(pagedFormatSql, pageSize, pageNum * pageSize);
            DataTable dt = DataFactory.ExecuteSqlReturnTable(pagedSql);
            return dt;
        }

        private static SourcingNoteType GetSourcingNoteFromDataRow(DataRow dr)
        {
            SourcingNoteType note = new SourcingNoteType();

            note.SourcingId = StringUtil.GetSafeInt(dr["SourcingId"]);
            note.SupplierId = StringUtil.GetSafeInt(dr["SupplierId"]);
            note.ItemSkuList = StringUtil.GetSafeString(dr["ItemSkuList"]);
            note.ItemNumList = StringUtil.GetSafeString(dr["ItemNumList"]);
            note.ItemPriceList = StringUtil.GetSafeString(dr["ItemPriceList"]);
            note.ExtraFee = StringUtil.GetSafeDouble(dr["ExtraFee"]);
            note.ShippingFee = StringUtil.GetSafeDouble(dr["ShippingFee"]);
            note.TotalFee = StringUtil.GetSafeDouble(dr["TotalFee"]);
            note.Comment = StringUtil.GetSafeString(dr["Comment"]);
            note.SourcingDate = StringUtil.GetSafeDateTime(dr["SourcingDate"]);
            return note;
        }

        public static SourcingNoteType GetSourcingNoteById(int id)
        {
            String sql = String.Format("select * from [SourcingNote] where SourcingId={0}", id);
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql);

            if (dt == null || dt.Rows.Count == 0)
                return null;

            return GetSourcingNoteFromDataRow(dt.Rows[0]);
        }

        public static int InsertOneSourcingNote(SourcingNoteType note)
        {
            IDbCommand cmd = DataFactory.CreateCommand(null);
            cmd.CommandText = @"Insert into [SourcingNote] (SupplierId, ItemSkuList, ItemNumList,"
                + "ItemPriceList, ExtraFee, ShippingFee, TotalFee, Comment, SourcingDate) values"
                + "(@SupplierId, @ItemSkuList, @ItemNumList,"
                + "@ItemPriceList, @ExtraFee, @ShippingFee, @TotalFee, @Comment, @SourcingDate)";

            DataFactory.AddCommandParam(cmd, "@SupplierId", DbType.Int32, note.SupplierId);
            DataFactory.AddCommandParam(cmd, "@ItemSkuList", DbType.String, note.ItemSkuList);
            DataFactory.AddCommandParam(cmd, "@ItemNumList", DbType.String, note.ItemNumList);
            DataFactory.AddCommandParam(cmd, "@ItemPriceList", DbType.String, note.ItemPriceList);
            DataFactory.AddCommandParam(cmd, "@ExtraFee", DbType.Double, note.ExtraFee);
            DataFactory.AddCommandParam(cmd, "@ShippingFee", DbType.Double, note.ShippingFee);
            DataFactory.AddCommandParam(cmd, "@TotalFee", DbType.Double, note.TotalFee);
            DataFactory.AddCommandParam(cmd, "@Comment", DbType.String, note.Comment);
            // CAUTION: we need to convert note.SourcingDate to string otherwise we will meet with failure.
            DataFactory.AddCommandParam(cmd, "@SourcingDate", DbType.DateTime, note.SourcingDate.ToShortDateString());

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

        public static bool ModifyOneSourcingNote(SourcingNoteType note)
        {
            bool result = false;

            if (note == null || note.SourcingId <= 0)
                return false;

            IDbCommand cmd = DataFactory.CreateCommand(null);
            cmd.CommandText = @"Update [SourcingNote] set SupplierId=@SupplierId, ItemSkuList=@ItemSkuList,"
                + "ItemNumList=@ItemNumList, ItemPriceList=@ItemPriceList, ExtraFee=@ExtraFee,"
                + "ShippingFee=@ShippingFee, TotalFee=@TotalFee, Comment=@Comment, SourcingDate=@SourcingDate where SourcingId=@SourcingId";

            DataFactory.AddCommandParam(cmd, "@SupplierId", DbType.Int32, note.SupplierId);
            DataFactory.AddCommandParam(cmd, "@ItemSkuList", DbType.String, note.ItemSkuList);
            DataFactory.AddCommandParam(cmd, "@ItemNumList", DbType.String, note.ItemNumList);
            DataFactory.AddCommandParam(cmd, "@ItemPriceList", DbType.String, note.ItemPriceList);
            DataFactory.AddCommandParam(cmd, "@ExtraFee", DbType.Double, note.ExtraFee);
            DataFactory.AddCommandParam(cmd, "@ShippingFee", DbType.Double, note.ShippingFee);
            DataFactory.AddCommandParam(cmd, "@TotalFee", DbType.Double, note.TotalFee);
            DataFactory.AddCommandParam(cmd, "@Comment", DbType.String, note.Comment);
            DataFactory.AddCommandParam(cmd, "@SourcingDate", DbType.DateTime, note.SourcingDate);

            DataFactory.AddCommandParam(cmd, "@SourcingId", DbType.Int32, note.SourcingId);

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


        public static bool DeleteOneSourcingNote(int noteId)
        {
            bool result = false;

            SourcingNoteType note = GetSourcingNoteById(noteId);
            if (note == null)
                return false;

            String sql = string.Format("delete from [SourcingNote] where SourcingId={0}", noteId);
            DataFactory.ExecuteSql(sql);
            result = true;

            return result;
        }
    }
}
