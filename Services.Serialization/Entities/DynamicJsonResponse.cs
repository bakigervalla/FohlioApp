using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace Fohlio.RevitReportsIntegration.Services.Serialization.Entities
{
    public class DynamicJsonResponse : DynamicDictionary
    {
        public T ToObject<T>()
        {
            return (T)ToJson().ToObject(typeof(T));
        }

        public DynamicJsonResponse()
        {
        }

        public DynamicJsonResponse(XDocument doc, string key = "Response")
        {
            ProcessElement(doc.Element(key), this);
        }

        public DynamicJsonResponse(XElement elt)
        {
            ProcessElement(elt, this);
        }

        public DynamicJsonResponse(JObject obj)
        {
            ProcessObject(obj, this);
        }

        public DynamicJsonResponse(JArray obj)
        {
            ProcessArray(obj, this);
        }

        internal static void ProcessElement(XElement obj, DynamicDictionary dict)
        {
            var elList = from el in obj.Elements() select el;
            foreach (var elt in elList)
            {
                if (elt.HasElements == false)
                {
                    dict.Dictionary[elt.Name.LocalName] = (string)elt;
                }
                else
                {
                    // Careful, might be an array
                    var isArray = (elt.Elements().Count() != elt.Elements().Select(el => el.Name).Distinct().Count());
                    var subDict = new DynamicDictionary();
                    if (isArray)
                        ProcessElementArray(elt, subDict);
                    else
                        ProcessElement(elt, subDict);
                    dict.Dictionary[elt.Name.LocalName] = subDict;
                }
            }
        }

        internal static void ProcessElementArray(XElement obj, DynamicDictionary dict)
        {
            var elList = from el in obj.Elements() select el;
            var i = 0;
            foreach (var elt in elList)
            {
                if (elt.HasElements == false)
                {
                    dict.Dictionary[i.ToString()] = (string)elt;
                }
                else
                {
                    // Careful, might be an array
                    var isArray = (elt.Elements().Count() != elt.Elements().Select(el => el.Name).Distinct().Count());
                    var subDict = new DynamicDictionary();
                    ProcessElement(elt, subDict);
                    if (isArray)
                        ProcessElementArray(elt, subDict);
                    else
                        ProcessElement(elt, subDict);
                    dict.Dictionary[i.ToString()] = subDict;
                }
                i++;
            }
        }

        internal static void ProcessObject(JObject obj, DynamicDictionary dict)
        {
            foreach (var pair in obj)
            {
                if (pair.Value.GetType() == typeof(JValue))
                {
                    dict.Dictionary[pair.Key] = ((JValue)pair.Value).Value;
                }
                else if (pair.Value.GetType() == typeof(JObject))
                {
                    var subDict = new DynamicDictionary();
                    ProcessObject((JObject)(pair.Value), subDict);
                    dict.Dictionary[pair.Key] = subDict;
                }
                else if (pair.Value.GetType() == typeof(JArray))
                {
                    var subDict = new DynamicDictionary();
                    ProcessArray((JArray)(pair.Value), subDict);
                    dict.Dictionary[pair.Key] = subDict;
                }
            }
        }

        internal static void ProcessArray(JArray obj, DynamicDictionary dict)
        {
            var i = 0;
            foreach (var item in obj)
            {
                if (item.GetType() == typeof(JValue))
                {
                    dict.Dictionary[i.ToString()] = ((JValue)item).Value;
                }
                else if (item.GetType() == typeof(JObject))
                {
                    var subDict = new DynamicDictionary();
                    ProcessObject((JObject)(item), subDict);
                    dict.Dictionary[i.ToString()] = subDict;
                }
                else if (item.GetType() == typeof(JArray))
                {
                    var subDict = new DynamicDictionary();
                    ProcessArray((JArray)item, subDict);
                    dict.Dictionary[i.ToString()] = subDict;
                }
                i++;
            }
        }
    }
}