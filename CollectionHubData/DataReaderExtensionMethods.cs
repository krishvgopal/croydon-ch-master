using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHubData
{
    public static class DataReaderExtensions
    {
        public static int? ReadNullableInt32(this IDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? (int?)null : reader.GetInt32(index);
        }
        public static long? ReadNullableInt64(this IDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? (long?)null : reader.GetInt64(index);
        }
        public static double? ReadNullableDouble(this IDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? (double?)null : reader.GetDouble(index);
        }
        public static string ReadNullableString(this IDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? null : reader.GetString(index);
        }
    }
}
