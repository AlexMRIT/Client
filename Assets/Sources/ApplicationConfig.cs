namespace Client
{
    public static class ApplicationConfig
    {
        public static readonly int Port = 27015;
        public static readonly int MaxLoginLength = 15;
        public static readonly int MaxPasswordLength = 15;
        public static readonly int MaxServerNameLength = 15;
        public static readonly int MaxServerDescription = 30;
        public static readonly int MinLoginLength = 4;
        public static readonly int MinPasswordLength = 4;
        public static readonly int MinServerNameLength = 4;
        public static readonly int MinServerDescriptionLength = 4;
        public static readonly int MaxPlayersInRoom = 2;
    }
}