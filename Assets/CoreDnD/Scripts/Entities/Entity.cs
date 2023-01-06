using System;
using System.Collections.Generic;
using DndCore.Ability;
using Entities.Features;

namespace Entities
{
    public class Entity
    {
        public Guid Id {get; private set;}
        public List<Feature> Features { get; set; }
        public List<Ability> Abilities {get; private set;}

        public Entity() : this(Guid.NewGuid()) {}

        public Entity (Guid id)
        {
            Id = id;
            Features = new List<Feature>();
            Abilities = new List<Ability>();
        }
    }
}

