namespace Client.Utilite
{
    public static class OpcodeExtension
    {
        public static readonly byte OpcodeAuthentication = 0x00;
        public static readonly byte OpcodeGetRoomsWindow = 0x01;
        public static readonly byte OpcodeMovementAsync = 0x02;
        public static readonly byte OpcodeAttackHandle = 0x03;
        public static readonly byte OpcodeEnterRoom = 0x04;
        public static readonly byte OpcodeMovementStopAsync = 0x05;
    }
}