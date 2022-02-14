using System.Collections.Generic;

namespace XhunderUtil
{
    public struct PackEntryCollection<T>
    {
        private Dictionary<string, T> entries;

        public void Register(string pack, string subkey, T entry, bool allowOverwrite = true)
        {
            if (entries == null)
            {
                entries = new Dictionary<string, T>();
            }
            Register(GetCompleteString(pack, subkey), entry, allowOverwrite);
        }

        public void Register(string completeKey, T entry, bool allowOverwrite = true)
        {
            if (entries.ContainsKey(completeKey))
            {
                if (!allowOverwrite)
                {
                    return;
                }
                entries.Remove(completeKey);
            }
        }

        public T this[string key] => entries[key];
        public T this[string pack, string subkey] => entries[GetCompleteString(pack, subkey)];

        private string GetCompleteString(string pack, string subkey) => $"{pack}.{subkey}";
    }
}
