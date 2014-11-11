using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHubData
{
	public class DataMergeSource
	{
		public string ViewUserName      { get; set; }
		public string ViewDescription   { get; set; }
		public string ViewName          { get; set; }

		public DataMergeSource() { }
		public DataMergeSource(System.Data.SqlClient.SqlDataReader value)
		{
			ViewUserName        = Convert.ToString(value["ViewUserName"]);
			ViewDescription     = Convert.ToString(value["ViewDescription"]);
			ViewName            = Convert.ToString(value["ViewName"]);   
		}
	}
	public class DataMergeFields
	{
		public int      ColId       { get; set; }
		public int      FieldCode   { get; set; }
		public int      Length      { get; set; }
		public string   Fieldname   { get; set; }
		public string   FieldType   { get; set; }
		public string   Info        { get; set; }

		public DataMergeFields() {}
		public DataMergeFields(System.Data.SqlClient.SqlDataReader value)
		{
			ColId       = Convert.ToInt32(value["ColID"]);
			Fieldname   = Convert.ToString(value["Fieldname"]);
			FieldCode   = Convert.ToInt32(value["FieldCode"]);
			FieldType   = Convert.ToString(value["FieldType"]);
			Length      = Convert.ToInt32(value["Length"]);
			Info        = Convert.ToString(value["Info"]);
		}
	}
}
