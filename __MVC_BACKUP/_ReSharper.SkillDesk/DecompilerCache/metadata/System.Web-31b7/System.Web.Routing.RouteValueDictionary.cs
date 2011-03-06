// Type: System.Web.Routing.RouteValueDictionary
// Assembly: System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Web.dll

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
    [TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
    public class RouteValueDictionary : IDictionary<string, object>, ICollection<KeyValuePair<string, object>>,
                                        IEnumerable<KeyValuePair<string, object>>, IEnumerable
    {
        public RouteValueDictionary();
        public RouteValueDictionary(object values);
        public RouteValueDictionary(IDictionary<string, object> dictionary);
        public Dictionary<string, object>.KeyCollection Keys { get; }
        public Dictionary<string, object>.ValueCollection Values { get; }

        #region IDictionary<string,object> Members

        public void Add(string key, object value);
        public void Clear();
        public bool ContainsKey(string key);
        public bool Remove(string key);
        public bool TryGetValue(string key, out object value);
        void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item);
        bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item);
        void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex);
        bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item);
        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator();
        public int Count { get; }
        public object this[string key] { get; set; }
        ICollection<string> IDictionary<string, object>.Keys { get; }
        ICollection<object> IDictionary<string, object>.Values { get; }
        bool ICollection<KeyValuePair<string, object>>.IsReadOnly { get; }

        #endregion

        public bool ContainsValue(object value);
        public Dictionary<string, object>.Enumerator GetEnumerator();
    }
}
