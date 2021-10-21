using System;
using System.Collections;
using System.Collections.Generic;

namespace XhunderUtil
{
    public class EnumDictionary<TEnum, TValue> : IDictionary<TEnum, TValue>
    {
        private Dictionary<TEnum, TValue> internalCollection;
        private TValue defaultValue;

        public EnumDictionary(TValue defaultValue)
        {
            this.defaultValue = defaultValue;
            internalCollection = new Dictionary<TEnum, TValue>();
        }

        public TValue this[TEnum key]
        {
            get
            {
                if (!internalCollection.ContainsKey(key))
                {
                    internalCollection.Add(key, defaultValue);
                }
                return internalCollection[key];
            }

            set
            {
                if (!internalCollection.ContainsKey(key))
                {
                    internalCollection.Add(key, value);
                }
                internalCollection[key] = value;
            }
        }

        public int Count => internalCollection.Count;

        public ICollection<TEnum> Keys => internalCollection.Keys;

        public ICollection<TValue> Values => internalCollection.Values;

        public bool IsReadOnly => false;

        public void Add(TEnum key, TValue value)
        {
            throw new NotImplementedException();
        }

        public void Add(KeyValuePair<TEnum, TValue> item)
        {
            this[item.Key] = item.Value;
        }

        public void Clear()
        {
            internalCollection.Clear();
        }

        public bool Contains(KeyValuePair<TEnum, TValue> item)
        {
            return internalCollection.ContainsKey(item.Key);
        }

        public bool ContainsKey(TEnum key) => internalCollection.ContainsKey(key);

        public EnumDictionary<TEnum, TValue> Copy()
        {
            EnumDictionary<TEnum, TValue> copy = new EnumDictionary<TEnum, TValue>(defaultValue);
            foreach (TEnum key in internalCollection.Keys)
            {
                copy[key] = this[key];
            }
            return copy;
        }

        public void CopyTo(KeyValuePair<TEnum, TValue>[] array, int arrayIndex)
        {
            EnumDictionary<TEnum, TValue> copy = new EnumDictionary<TEnum, TValue>(defaultValue);
            foreach (TEnum key in internalCollection.Keys)
            {
                copy[key] = internalCollection[key];
            }
        }

        public IEnumerator<KeyValuePair<TEnum, TValue>> GetEnumerator() => new EnumDictionaryPairEnumerator<TEnum, TValue>(this);

        public bool Remove(TEnum key)
        {
            this[key] = defaultValue;
            return true;
        }

        public bool Remove(KeyValuePair<TEnum, TValue> item)
        {
            this[item.Key] = defaultValue;
            return true;
        }

        public bool TryGetValue(TEnum key, out TValue value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() => new EnumDictionaryEnumerator<TEnum, TValue>();
    }
}