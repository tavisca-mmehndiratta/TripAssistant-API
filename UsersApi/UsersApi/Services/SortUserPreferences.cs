using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersApi.Services
{
    public class SortUserPreferences
    {
        public Dictionary<string, int> UpdateRecord(SortedDictionary<string, int> preferences)
        {

            Dictionary<string, int> updatePreferences = new Dictionary<string, int>();
            List<string> keys = preferences.Keys.ToList();
            List<int> value = preferences.Values.ToList();
            List<int> val = new List<int>();
            foreach (int vals in value)
            {
                val.Add(vals);
            }
            value.Sort();
            value.Reverse();
            for (int i = 0; i < value.Count; i++)
            {
                int index = val.IndexOf(value[i]);
                updatePreferences.Add(keys[index], value[i]);
                keys.RemoveAt(index);
                val.RemoveAt(index);
            }
            return updatePreferences;
        }
    }
}