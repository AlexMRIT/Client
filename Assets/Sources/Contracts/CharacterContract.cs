namespace Client.Contracts
{
    public sealed class CharacterContract
    {
        public readonly int Id;
        public readonly int Score;
        public readonly string Name;
        public readonly float AttackSpeed;
        public readonly float MoveSpeed;
        public readonly int PhysicsAttackMin;
        public readonly int PhysicsAttackMax;

        public CharacterContract(int id, int score, string name, float attackSpeed, 
            float moveSpeed, int physicsAttackMin, int physicsAttackMax)
        {
            Id = id;
            Score = score;
            Name = name;
            AttackSpeed = attackSpeed;
            MoveSpeed = moveSpeed;
            PhysicsAttackMin = physicsAttackMin;
            PhysicsAttackMax = physicsAttackMax;
        }
    }
}