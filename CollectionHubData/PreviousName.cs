using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectionHubData
{
   
    public class PreviousName : Attribute
    {
        public string OtherName { get; set; }

        PreviousName(string name)
        {
            this.OtherName = name;
        }
    }
}
