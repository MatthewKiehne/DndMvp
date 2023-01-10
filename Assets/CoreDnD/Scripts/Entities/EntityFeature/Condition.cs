namespace Entities.Features
{
    public class Condition
    {
        public string Name { get; private set; }
        public int Value { get; private set; }

        public Condition()
        {
            
        }

        public Condition(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public Condition Clone()
        {
            return new Condition(Name, Value);
        }
    }
}

