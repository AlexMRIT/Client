namespace Client.Contracts
{
    public sealed class ServerRoom
    {
        public readonly int Id;
        public readonly string Name;
        public readonly string Description;
        public readonly int CurrentPlayer;
        public readonly int MaxPlayer;

        public ServerRoom(int id, string name, string description, int currPlayer, int maxPlayer)
        {
            Id = id;
            Name = name;
            Description = description;
            CurrentPlayer = currPlayer;
            MaxPlayer = maxPlayer;
        }
    }
}