using System.Collections.Generic;
using System.Linq;
using Entities.Features;

namespace Entities.Search
{
    public class EntitySearchResult : Dictionary<string, List<Condition>>
    {
        public EntitySearchResult()
        {
            // does nothing
        }

        public EntitySearchResult(Dictionary<string, List<Condition>> dictionary)
        {
            foreach (var item in dictionary)
            {
                Add(item.Key, item.Value);
            }
        }

        public Dictionary<string, List<Condition>> ToDictionary()
        {
            return this;
        }
    }
}