using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using KCSpy.Model;
using Neetsonic.Tool.Database;
using Neetsonic.Tool.Extensions;

namespace KCSpy.Util
{
    public static class DataAccess
    {
        private const int BulkSize = 200000;
        private const int BulkTimeout = 300;
        private const string ConnectionString = @"Data Source=NEETSONIC\MSSQLSERVER2;database=senka;user=sa;pwd=qjsj8888";

        public static void NewMember(IEnumerable<Member> members)
        {
            using(DataTable datas = members.ToDataTable())
            {
                using(SqlBulkCopy sqlBC = new SqlBulkCopy(ConnectionString))
                {
                    sqlBC.BatchSize = BulkSize;
                    sqlBC.BulkCopyTimeout = BulkTimeout;
                    sqlBC.DestinationTableName = "dbo.Member";
                    sqlBC.ColumnMappings.Add("MemberID", "MemberID");
                    sqlBC.ColumnMappings.Add("Name", "Name");
                    sqlBC.ColumnMappings.Add("ServerID", "ServerID");
                    sqlBC.ColumnMappings.Add("CurrentExp", "CurrentExp");
                    sqlBC.ColumnMappings.Add("YearBeginExp", "YearBeginExp");
                    sqlBC.ColumnMappings.Add("MonthBeginExp", "MonthBeginExp");
                    sqlBC.ColumnMappings.Add("CurrentComment", "CurrentComment");
                    sqlBC.ColumnMappings.Add("LastMonthBonus", "LastMonthBonus");
                    sqlBC.ColumnMappings.Add("CurrSeasonBonusTotal", "CurrSeasonBonusTotal");
                    sqlBC.ColumnMappings.Add("UpdateTimeStamp", "UpdateTimeStamp");
                    sqlBC.WriteToServer(datas);
                }
            }
        }
        public static void UpdateMemberCurr(IEnumerable<MemberTemp> members)
        {
            ExecuteNonQuery(@"DELETE FROM MemberTemp");
            using(DataTable datas = members.ToDataTable())
            {
                using(SqlBulkCopy sqlBC = new SqlBulkCopy(ConnectionString))
                {
                    sqlBC.BatchSize = BulkSize;
                    sqlBC.BulkCopyTimeout = BulkTimeout;
                    sqlBC.DestinationTableName = "dbo.MemberTemp";
                    sqlBC.ColumnMappings.Add("MemberID", "MemberID");
                    sqlBC.ColumnMappings.Add("Name", "Name");
                    sqlBC.ColumnMappings.Add("CurrentExp", "CurrentExp");
                    sqlBC.ColumnMappings.Add("CurrentComment", "CurrentComment");
                    sqlBC.ColumnMappings.Add("UpdateTimeStamp", "UpdateTimeStamp");
                    sqlBC.WriteToServer(datas);
                }
            }
            ExecuteNonQuery(@"UPDATE Member SET Name = t.Name, CurrentExp = t.CurrentExp, CurrentComment = t.CurrentComment, UpdateTimeStamp = t.UpdateTimeStamp FROM MemberTemp t WHERE t.MemberID = Member.MemberID");
        }
        public static List<Member> SearchMember(string name, int serverID) =>ExecuteQuery(@"SELECT * FROM Member WHERE Name = @Name AND ServerID = @ServerID", new[]
                             {
                                     new SqlParameter(@"@Name", SqlDbType.NVarChar) {Value = name},
                                     new SqlParameter(@"@ServerID", SqlDbType.Int) {Value = serverID}
                             }).Tables[0].ToModel<Member>();

        public static void NewMember(Member member)=>ExecuteNonQuery(@"INSERT INTO Member (MemberID,[Name],[ServerID],[CurrentExp],[YearBeginExp],[MonthBeginExp] ,[CurrentComment],[LastMonthBonus],[CurrSeasonBonusTotal],[UpdateTimeStamp])
VALUES (@MemberID,@Name,@ServerID,@CurrentExp,@YearBeginExp,@MonthBeginExp,@CurrentComment,@LastMonthBonus,@CurrSeasonBonusTotal,@UpdateTimeStamp)", new[]
            {
                    new SqlParameter(@"@MemberID", SqlDbType.Int) {Value = member.MemberID},
                    new SqlParameter(@"@Name", SqlDbType.NVarChar) {Value = member.Name},
                    new SqlParameter(@"@ServerID", SqlDbType.Int) {Value = member.ServerID},
                    new SqlParameter(@"@CurrentExp", SqlDbType.Int) {Value = member.CurrentExp},
                    new SqlParameter(@"@YearBeginExp", SqlDbType.Int) {Value = member.YearBeginExp},
                    new SqlParameter(@"@MonthBeginExp", SqlDbType.Int) {Value = member.MonthBeginExp},
                    new SqlParameter(@"@CurrentComment", SqlDbType.NVarChar) {Value = member.CurrentComment},
                    new SqlParameter(@"@LastMonthBonus", SqlDbType.Int) {Value = member.LastMonthBonus},
                    new SqlParameter(@"@CurrSeasonBonusTotal", SqlDbType.Int) {Value = member.CurrSeasonBonusTotal},
                    new SqlParameter(@"@UpdateTimeStamp", SqlDbType.BigInt) {Value = member.UpdateTimeStamp},
            });
        

        private static void ExecuteNonQuery(string sql, SqlParameter[] parameters = null)
        {
            SqlExecutor.ExecuteNonQuery(ConnectionString, sql, parameters);
        }
        private static DataSet ExecuteQuery(string sql, SqlParameter[] parameters = null) => SqlExecutor.ExecuteQuery(ConnectionString, sql, parameters);
        private static object ExecuteScalar(string sql, SqlParameter[] parameters = null) => SqlExecutor.ExecuteScalar(ConnectionString, sql, parameters);
    }
}