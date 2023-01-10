using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Features
{
    public class Feature
    {
        public Guid Id;
        public string Name;
        public List<Condition> Conditions;

        public Feature()
        {
            
        }

        public Feature(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
            Conditions = new List<Condition>();
        }
    }
}
