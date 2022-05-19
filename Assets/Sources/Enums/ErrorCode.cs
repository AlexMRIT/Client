namespace Client.Enums
{
    public enum ErrorCode : byte
    {
        None = 0x00,
        MaxLength = 0x01,
        MinLength = 0x02
    }

    public enum ErrorCodeAuthetication : byte
    {
        ReasonUserOrPassWrong = 0x00,
        ReasonAccountInUse = 0x01
    }
}