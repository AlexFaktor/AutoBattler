namespace App.GameCore.Battles.System;

public class Player : IPlayer
{
    public object Value { get; set; }
    public Guid Id { get; set; }
    public string Username { get; set; }

    public Player(object value)
    {
        Value = value;
    }
}

public interface IPlayer
{
    public Guid Id { get; set; }
    public string Username { get; set; }

}
