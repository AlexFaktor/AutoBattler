using App.GameCore.Battles.Manager;

namespace App.GameCore.Manager;

public class GameManager : IGameManager
{
    private GameConfig _config;

    public void MakeBattle(BattleConfiguration configuration, BattleRewards rewards)
    {
        throw new NotImplementedException();
    }

    public void MakeChoice()
    {
        throw new NotImplementedException();
    }

    public void MakeGacha()
    {
        throw new NotImplementedException();
    }

    public void SetSquad()
    {
        throw new NotImplementedException();
    }
}

public interface IGameManager
{
    public void MakeBattle(BattleConfiguration configuration, BattleRewards rewards);

    public void MakeChoice();

    public void SetSquad();

    public void MakeGacha();

}
