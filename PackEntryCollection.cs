using System.Collections.Generic;

namespace XhunderUtil
{
    public struct PackEntryCollecion<T>
    {
        private Dictionary<string, T> entries;
        public string CurrentPack;

        public PackEntryCollecion(string currentPack = "base") : this()
        {
            CurrentPack = currentPack;
            entries = new Dictionary<string, T>();
        }

        public void Register(string pack, string subkey, T entry, bool allowOverwrite = true)
        {
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
