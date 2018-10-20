using System.Data;
using Neetsonic.Tool.Extensions;

namespace KCSpy.Model
{
    public sealed class Record
    {
        public int ID { get; set; }
        public int MemberID { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int Exp { get; set; }
        public long RecTime { get; set; }
        public short? Senka { get; set; }
        public short? EOSenka { get; set; }

        public Record FromDataRow(DataRow row) => new Record
        {
            ID = row.FieldInt(nameof(ID)),
            MemberID = row.FieldInt(nameof(MemberID)),
            Name = row.FieldString(nameof(Name)),
            Comment = row.FieldString(nameof(Comment)),
            Exp = row.FieldInt(nameof(Exp)),
            RecTime = row.FieldLong(nameof(RecTime)),
            Senka = row.FieldNullableShort(nameof(Senka)),
            EOSenka = row.FieldNullableShort(nameof(EOSenka))
        };
    }
}