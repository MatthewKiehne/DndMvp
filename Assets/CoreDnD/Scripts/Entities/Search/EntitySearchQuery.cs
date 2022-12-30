using System.Collections.Generic;

namespace Entities.Search
{
    public class EntitySearchQuery
    {
        public List<string> SearchTargets { get; private set; }
        public HashSet<string> ConditionNames { get; private set; }

        public EntitySearchQuery(List<string> searchTargets, ICollection<string> conditionNames)
        {
            SearchTargets = searchTargets;
            ConditionNames = new HashSet<string>(conditionNames);
        }
    }
}
