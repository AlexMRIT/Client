namespace Client.Enums
{
    public enum Opcodes : byte
    {
        ServerLoginFail = 0x00,
        ServerLoginSuccess = 0x01,
        ServerSendRoom = 0x02,
        ServerDamageResult = 0x03,
        ServerCharacterStopMove = 0x04,
        ServerCharacterMoveToLocation = 0x05,
        ServerCharacterUpdate = 0x06,
        ServerCharacterDeath = 0x07,
        ServerExitAllCharacterForRoom = 0x08,
        ServerDeleteMe = 0x09,
        ServerAddMe = 0x0A,
        ServerEnterRoom = 0x0B
    }
}