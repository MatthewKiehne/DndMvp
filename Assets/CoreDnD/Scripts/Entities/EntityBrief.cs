using System;
using System.Collections.Generic;
using System.Linq;
using DndCore.Ability;
using Entities.Features;

namespace Entities
{
    public class EntityBrief
    {
        public Guid Id;
        public List<Feature> Features;
        public List<AbilityBrief> Abilities;
        public Dictionary<string, int> DiscreteValues;

        public EntityBrief()
        {
            Features = new List<Feature>();
            Abilities = new List<AbilityBrief>();
            DiscreteValues = new Dictionary<string, int>();
        }

        public EntityBrief(Entity entity)
        {
            Id = entity.Id;
            Features = entity.Features;
            Abilities = entity.Abilities.Select(ability => new AbilityBrief(ability)).ToList();
            DiscreteValues = entity.DiscreteValues;
        }
    }
}
