using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;

namespace CollectionHubData
{
    public class KeyValuePair<K, V>
    {
        public K Key { get; set; }
        public V Value { get; set; }

        public KeyValuePair(){}
        public KeyValuePair(K key, V value)
        {
            this.Key = key;
            this.Value = value;
        }
    }

}
