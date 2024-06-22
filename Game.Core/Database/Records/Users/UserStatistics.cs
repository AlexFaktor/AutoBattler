namespace Game.Core.Database.Records.Users;

public class UserStatistics
{
    public Guid UserId { get; set; }
    public DateTime DateOfUserRegistration { get; set; } = DateTime.Now;

    public int NumberInteractionsWithBot { get; set; } = 0;
}
