namespace Game.Core.DatabaseRecords.Users;

public class UserResources
{
    public Guid UserId { get; set; }
    public decimal RandomCoin { get; set; } = 0;
    public decimal Fackoins { get; set; } = 0;
    public int SoulValue { get; set; } = 0;

    public int Energy { get; set; } = 0;
}
