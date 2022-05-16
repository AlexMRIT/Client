namespace Client.Models
{
    public sealed class CharacterSpecification
    {
        public CharacterSpecification(int id)
            : this(id, strength: 0, dextity: 0, endurance: 0, baseHealth: 100)
        {
            ObjectId = id;
        }

        public CharacterSpecification(int id, int strength, int dextity, int endurance, int baseHealth)
        {
            ObjectId = id;
            Strength = strength;
            Dextity = dextity;
            Endurance = endurance;
            BaseHealth = baseHealth;
        }

        public readonly int ObjectId;

        public int Strength { get; private set; }
        public int Dextity { get; private set; }
        public int Endurance { get; private set; }
        public int BaseHealth { get; private set; }
    }
}