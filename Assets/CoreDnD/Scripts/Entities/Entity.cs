using System;
using System.Collections.Generic;
using Entities.Features;

namespace Entities
{
    public class Entity
    {
        public Guid Id {get; private set;}
        public List<Feature> Features { get; set; }

        public Entity()
        {
            Features = new List<Feature>();
            Id = Guid.NewGuid();
        }
    }
}

