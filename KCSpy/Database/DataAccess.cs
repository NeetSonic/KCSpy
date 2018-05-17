using System;
using System.Data;
using System.Data.SQLite;
using KCSpy.Model;
using Neetsonic.Tool;
using Neetsonic.Tool.Database;

namespace KCSpy.Database
{
    public static class DataAccess
    {
        private static void ExecuteNonQuery(string sql, SQLiteParameter[] parameters)
        {
          SQLiteExecutor.ExecuteNonQuery(string.Format($"datasource={Global.Config.DBPath}"), sql, parameters);  
        }
        public static void SaveUserInfo(Record record)
        {
            ExecuteNonQuery(@"INSERT INTO Record(MemberID, Name, Comment, Exp, RecTime) VALUES(@MemberID, @Name, @Comment, @Exp, @RecTime)", new []
            {
                new SQLiteParameter(DbType.Int32,@"MemberID"){Value = record.MemberID},
                new SQLiteParameter(DbType.String, @"Name"){Value =  record.Name},
                new SQLiteParameter(DbType.String, @"Comment"){Value = record.Comment},
                new SQLiteParameter(DbType.Int32, @"Exp"){Value = record.Exp},
                new SQLiteParameter(DbType.Int64, @"RecTime"){Value = TimeTool.NowUnixStamp()}
            });
        }
    }
}