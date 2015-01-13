using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CollectionHubData
{
    public class ActionStatus
    {
        public int      ActionStatusId      { get; set; }
        public string   ActionStatusText    { get; set; }

        public ActionStatus() { }
        public ActionStatus(IDataReader value)
        {
            ActionStatusId = Convert.ToInt32(value["ActionStatusId"]);
            ActionStatusText = value["ActionStatusText"].ToString();
        }
    }
}
