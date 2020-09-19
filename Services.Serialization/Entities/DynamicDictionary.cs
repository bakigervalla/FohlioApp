using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Fohlio.RevitReportsIntegration.Services.Serialization.Entities
{
    public class DynamicDictionary : DynamicObject
    {
        public DynamicDictionary()
        {
            Dictionary = new Dictionary<string, object>();
        }

        public Dictionary<string, object> Dictionary { get; internal set; }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            // Converting the property name to lowercase so that property names become case-insensitive.
            var name = binder.Name /*.ToLower ()*/;
            // If the property name is found in a dictionary, set the result parameter to the property value and return true.
            // Otherwise, return false.
            return (Dictionary.TryGetValue(name, out result));
        }

        // If you try to set a value of a property that is not defined in the class, this method is called.
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            // Converting the property name to lowercase so that property names become case-insensitive.
            Dictionary[binder.Name /*.ToLower ()*/] = value;
            // You can always add a value to a dictionary, so this method always returns true.
            return (true);
        }

        public override bool TryDeleteMember(DeleteMemberBinder binder)
        {
            if (Dictionary.ContainsKey(binder.Name /*.ToLower ()*/))
                Dictionary.Remove(binder.Name /*.ToLower ()*/);
            return (true);
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            if (IsInteger(indexes[0]))
            {
                var index = (int)indexes[0];
                // If the property name is found in a dictionary, set the result parameter to the property value and return true.
                // Otherwise, return false.
                return (Dictionary.TryGetValue(index.ToString(), out result));
            } //else
            return (Dictionary.TryGetValue(indexes[0].ToString(), out result));
        }

        public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
        {
            if (IsInteger(indexes[0]))
            {
                var index = (int)indexes[0];
                // If a corresponding property already exists, set the value.
                if (Dictionary.ContainsKey(index.ToString()))
                    Dictionary[index.ToString()] = value;
                else
                    // If a corresponding property does not exist, create it.
                    Dictionary.Add(index.ToString(), value);
            }
            else
            {
                if (Dictionary.ContainsKey(indexes[0].ToString()))
                    Dictionary[indexes[0].ToString()] = value;
                else
                    // If a corresponding property does not exist, create it.
                    Dictionary.Add(indexes[0].ToString(), value);
            }
            return (true);
        }

        public override bool TryDeleteIndex(DeleteIndexBinder binder, object[] indexes)
        {
            if (IsInteger(indexes[0]))
            {
                var index = (int)indexes[0];
                if (Dictionary.ContainsKey(index.ToString()))
                    Dictionary.Remove(index.ToString());
            }
            else
            {
                if (Dictionary.ContainsKey(indexes[0].ToString()))
                    Dictionary.Remove(indexes[0].ToString());
            }
            return (true);
        }

        public int Count => Dictionary.Count;

        public override IEnumerable<string> GetDynamicMemberNames() => Dictionary.Keys;

        public virtual IEnumerable<KeyValuePair<string, object>> Items() => (Dictionary);

        private static bool IsInteger(object value)
        {
            return value is sbyte
                   || value is byte
                   || value is short
                   || value is ushort
                   || value is int
                   || value is uint
                   || value is long
                   || value is ulong;
        }

        public override string ToString()
        {
            return (ToString(Formatting.None));
        }

        public string ToString(Formatting formatting)
        {
            var obj = ToJson();
            return (obj.ToString(formatting));
        }

        internal void _ArrayDetection(ref JContainer obj)
        {
            var bArray = true;

            foreach (var prop in (obj as JObject)?.Properties() ?? Enumerable.Empty<JProperty>())
            {
                int val;
                if (int.TryParse(prop.Name, out val) != true)
                {
                    bArray = false;
                    break;
                }
            }
            if (bArray)
            {
                var jarr = new JArray();
                foreach (var item in obj.OfType<JProperty>())
                    jarr.Add(item.Value);

                obj = jarr;
            }

            var nb = obj.Count;
            for (var i = 0; i < nb; i++)
            {
                JObject sub = null;
                var prop = obj.ElementAtOrDefault(i) as JProperty;
                if (prop != null)
                {
                    if (prop.Value.GetType() == typeof(JObject))
                        sub = prop.Value.ToObject<JObject>();
                }
                else
                {
                    sub = obj.ElementAtOrDefault(i) as JObject;
                }
                if (sub != null)
                {
                    JContainer container = sub;
                    _ArrayDetection(ref container);
                    if (prop != null)
                        prop.Value.Replace(container);
                    else
                        sub.Replace(container);
                }
            }
        }

        public JObject ToJson()
        {
            var obj = (JObject)JToken.FromObject(this);
            JContainer container = obj;
            _ArrayDetection(ref container);
            return (obj);
        }
    }
}