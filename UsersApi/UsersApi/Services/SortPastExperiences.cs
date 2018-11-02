using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersApi.Services
{
    public class SortPastExperiences
    {
        public Dictionary<string, int> FilterPastExpereince(Dictionary<string, int> pastExperience)
        {
            Dictionary<string, int> updatedPastExperience = new Dictionary<string, int>();
            List<string> keys = pastExperience.Keys.ToList();
            List<int> value = pastExperience.Values.ToList();
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
                updatedPastExperience.Add(keys[index], value[i]);
                keys.RemoveAt(index);
                val.RemoveAt(index);
            }
            return updatedPastExperience;
        }
    }
}
