using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

using Newtonsoft.Json.Linq;


/// <summary>
/// Summary description for DataTableParameters
/// </summary>
public class DataTableParameters
{
	//Paging first record indicator. This is the start point in the current data set (0 index based - i.e. 0 is the first record).
	public int Start { get; set; }
	// global search keyword
	public string Search { get; set; }
	// Number of records that should be shown in table
	public int Length { get; set; }
	//represent the index of column which is to ordered 
	public int OrderByCol { get; set; }
	//order direction (asc or desc)
	public string OrderDirection { get; set; }
	public object[] Columns { get; set; }

	public DataTableParameters(string value)
	{

		//var jsSettings = new Newtonsoft.Json.JsonSerializerSettings();
		
		//jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;


	    //string JSarray_1 = value ; //@"[[""string 1"", 2013, ""string 2""], ""string 3"", [""string 4"", , ""string 5""]]";
        
        //JObject j = JObject.Parse("{\"j\":" + JSarray_1 + "}");


        var serializer = new JavaScriptSerializer();
        var json = serializer.Serialize(value);



		//var deserializedModel = JsonConvert.DeserializeObject<DataTableParameters>(value, jsSettings);

		var bb = json;
		////now deserializedModel is of type MyComplexType

	}
}