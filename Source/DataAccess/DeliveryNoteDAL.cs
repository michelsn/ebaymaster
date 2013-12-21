using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EbayMaster
{
    public class DeliveryNoteDAL
    {
        public static int GetDeliveryNoteCount()
        {
            int count = 0;

            String sql = "select count(*) from [DeliveryNote]";
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

        public static DataTable GetAllDeliveryNotes()
        {
            String sql = "select * from DeliveryNote";
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql);
            return dt;
        }

        public static DataTable GetPagedDeliveryNotes(int pageNum, int pageSize)
        {
            String pagedFormatSql = "select * from [DeliveryNote] where DeliveryNoteId in (select top {0} sub.DeliveryNoteId from ("
                + " select top {1} DeliveryNoteId from [DeliveryNote] order by DeliveryNoteId desc) [sub] order by sub.DeliveryNoteId) order by DeliveryNoteId desc";
            String pagedSql = String.Format(pagedFormatSql, pageSize, pageNum * pageSize);
            DataTable dt = DataFactory.ExecuteSqlReturnTable(pagedSql);
            return dt;
        }

        private static DeliveryNoteType GetDeliveryNoteFromDataRow(DataRow dr)
        {
            DeliveryNoteType note = new DeliveryNoteType();
            note.DeliveryNoteId = StringUtil.GetSafeInt(dr["DeliveryNoteId"]);
            note.DeliveryDate = StringUtil.GetSafeDateTime(dr["DeliveryDate"]);
            note.DeliveryOrderIds = StringUtil.GetSafeString(dr["DeliveryOrderIds"]);
            note.DeliveryUser = StringUtil.GetSafeString(dr["DeliveryUser"]);
            note.DeliveryFee = StringUtil.GetSafeDouble(dr["DeliveryFee"]);
            note.DeliveryExtraFee = StringUtil.GetSafeDouble(dr["DeliveryExtraFee"]);
            note.DeliveryComment = StringUtil.GetSafeString(dr["DeliveryComment"]);

            return note;

        }
        public static DeliveryNoteType GetOneDeliveryNote(String id)
        {
            String sql = String.Format("select * from [DeliveryNote] where DeliveryNoteId={0}", id);
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql);
            if (dt.Rows.Count == 0)
                return null;

            return GetDeliveryNoteFromDataRow(dt.Rows[0]);

        }

        public static DeliveryNoteType GetDeliveryNoteContainsTransaction(string tranId)
        {
            String sql = String.Format("select * from [DeliveryNote] where DeliveryOrderIds like '*,{0},*' or DeliveryOrderIds like '{0}'", tranId);
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql);
            if (dt.Rows.Count == 0)
                return null;

            return GetDeliveryNoteFromDataRow(dt.Rows[0]);
        }

        public static int InsertOneDeliveryNote(DeliveryNoteType di)
        {
            IDbCommand cmd = DataFactory.CreateCommand(null);
            cmd.CommandText = @"Insert into [DeliveryNote] (DeliveryDate, DeliveryOrderIds, DeliveryUser,"
                + "DeliveryFee, DeliveryExtraFee, DeliveryComment) values"
                + "(@DeliveryDate, @DeliveryOrderIds, @DeliveryUser,"
                + "@DeliveryFee, @DeliveryExtraFee, @DeliveryComment)";

            DataFactory.AddCommandParam(cmd, "@DeliveryDate", DbType.DateTime, di.DeliveryDate.ToShortDateString());
            DataFactory.AddCommandParam(cmd, "@DeliveryOrderIds", DbType.String, di.DeliveryOrderIds);
            DataFactory.AddCommandParam(cmd, "@DeliveryUser", DbType.String, di.DeliveryUser);
            DataFactory.AddCommandParam(cmd, "@DeliveryFee", DbType.Double, di.DeliveryFee);
            DataFactory.AddCommandParam(cmd, "@DeliveryExtraFee", DbType.Double, di.DeliveryExtraFee);
            DataFactory.AddCommandParam(cmd, "@DeliveryComment", DbType.String, di.DeliveryComment);

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

        public static bool ModifyOneDeliveryNote(DeliveryNoteType note)
        {
            bool result = false;

            if (note == null || note.DeliveryNoteId <= 0)
                return false;

            IDbCommand cmd = DataFactory.CreateCommand(null);
            cmd.CommandText = @"Update [DeliveryNote] set DeliveryDate=@DeliveryDate, DeliveryOrderIds=@DeliveryOrderIds,"
                + "DeliveryUser=@DeliveryUser, DeliveryFee=@DeliveryFee, DeliveryExtraFee=@DeliveryExtraFee, DeliveryComment=@DeliveryComment"
                + " where DeliveryNoteId=@DeliveryNoteId";

            DataFactory.AddCommandParam(cmd, "@DeliveryDate", DbType.DateTime, StringUtil.GetSafeDateTime(note.DeliveryDate));
            DataFactory.AddCommandParam(cmd, "@DeliveryOrderIds", DbType.String, StringUtil.GetSafeString(note.DeliveryOrderIds));
            DataFactory.AddCommandParam(cmd, "@DeliveryUser", DbType.String, StringUtil.GetSafeString(note.DeliveryUser));
            DataFactory.AddCommandParam(cmd, "@DeliveryFee", DbType.Double, StringUtil.GetSafeDouble(note.DeliveryFee));
            DataFactory.AddCommandParam(cmd, "@DeliveryExtraFee", DbType.Double, StringUtil.GetSafeDouble(note.DeliveryExtraFee));
            DataFactory.AddCommandParam(cmd, "@DeliveryComment", DbType.String, StringUtil.GetSafeString(note.DeliveryComment));

            DataFactory.AddCommandParam(cmd, "@DeliveryNoteId", DbType.Int32, note.DeliveryNoteId);

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

        public static bool DeleteOneDeliveryNote(String noteId)
        {
            bool result = false;

            DeliveryNoteType deliveryNote = DeliveryNoteDAL.GetOneDeliveryNote(noteId);
            if (deliveryNote == null)
                return false;

            String sql = string.Format("delete from [DeliveryNote] where DeliveryNoteId={0}", noteId);
            DataFactory.ExecuteSql(sql);
            result = true;

            return result;
        }
    }
}
