using System.Collections;
using System.Collections.Generic;

namespace Fohlio.RevitReportsIntegration.Services.Serialization.Entities
{
    public class DynamicDictionaryItems : IEnumerable<KeyValuePair<string, object>>
    {
        private readonly DynamicDictionary dictionary;

        public DynamicDictionaryItems(DynamicDictionary dict)
        {
            dictionary = dict;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => dictionary.Dictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => dictionary.Dictionary.GetEnumerator();
    }
}