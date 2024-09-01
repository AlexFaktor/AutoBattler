namespace GameLogic.Manager;

public class Player : IBattlePlayer
{
    public Guid Id { get; set; }
    public string Username { get; set; }

    public Player()
    { }
}

public interface IBattlePlayer
{
    public Guid Id { get; set; }
    public string Username { get; set; }
}
