using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Features
{
    public class Feature
    {
        public Guid Id {get; private set;}
        public string Name { get; private set; }
        public List<Condition> Conditions {get; private set;}

        public Feature(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
            Conditions = new List<Condition>();
        }
    }
}
