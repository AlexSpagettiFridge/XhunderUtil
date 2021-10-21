using System;
using System.Collections;
using System.Collections.Generic;

namespace XhunderUtil
{
    internal class EnumDictionaryEnumerator<TEnum, TValue> : IEnumerator<TEnum>
    {
        Queue<TEnum> keyQueue;

        public EnumDictionaryEnumerator()
        {
            Reset();
        }
        public object Current => keyQueue.Peek();

        TEnum IEnumerator<TEnum>.Current => (TEnum)Current;

        public void Dispose()
        {
            keyQueue.Clear();
        }

        public bool MoveNext()
        {
            return (keyQueue.Dequeue() != null);
        }

        public void Reset()
        {
            keyQueue = new Queue<TEnum>();
            foreach (TEnum key in Enum.GetValues(typeof(TEnum)))
            {
                keyQueue.Enqueue(key);
            }
        }
    }

    internal class EnumDictionaryPairEnumerator<TEnum, TValue> : IEnumerator<KeyValuePair<TEnum, TValue>>
    {
        Queue<TEnum> keyQueue;
        EnumDictionary<TEnum, TValue> enumDictionary;

        public EnumDictionaryPairEnumerator(EnumDictionary<TEnum, TValue> enumDictionary)
        {
            this.enumDictionary = enumDictionary;
            Reset();
        }
        public object Current
        {
            get
            {
                TEnum key = keyQueue.Peek();
                return new KeyValuePair<TEnum, TValue>(key, enumDictionary[key]);
            }
        }

        KeyValuePair<TEnum, TValue> IEnumerator<KeyValuePair<TEnum, TValue>>.Current => (KeyValuePair<TEnum, TValue>)Current;

        public void Dispose()
        {
            keyQueue.Clear();
        }

        public bool MoveNext()
        {
            return (keyQueue.Dequeue() != null);
        }

        public void Reset()
        {
            keyQueue = new Queue<TEnum>();
            foreach (TEnum key in Enum.GetValues(typeof(TEnum)))
            {
                keyQueue.Enqueue(key);
            }
        }
    }
}