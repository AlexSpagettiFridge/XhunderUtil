using System.Collections;
using System.Collections.Generic;

namespace XhunderUtil
{
    public struct PackEntryCollection<T> : IEnumerable<KeyValuePair<string, T>>
    {
        private Dictionary<string, T> entries;

        public void Register(string pack, string subkey, T entry, bool allowOverwrite = true)
        {
            Register(GetCompleteString(pack, subkey), entry, allowOverwrite);
        }

        public void Register(string completeKey, T entry, bool allowOverwrite = true)
        {
            if (entries == null)
            {
                entries = new Dictionary<string, T>();
            }
            if (entries.ContainsKey(completeKey))
            {
                if (!allowOverwrite)
                {
                    return;
                }
                entries.Remove(completeKey);
            }
            entries.Add(completeKey, entry);
        }

        public void Clear()
        {
            entries = new Dictionary<string, T>();
        }

        public T this[string key] => entries[key];
        public T this[string pack, string subkey] => entries[GetCompleteString(pack, subkey)];

        private string GetCompleteString(string pack, string subkey) => $"{pack}.{subkey}";

        public IEnumerator<KeyValuePair<string, T>> GetEnumerator() => entries.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
