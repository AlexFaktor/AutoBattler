using App.GameCore.Battles.Manager;

namespace App.GameCore.Manager;

public class GameManager : IGameManager
{
    private GameConfig _config;
}

public interface IGameManager
{
    public void MakeBattle(BattleConfiguration configuration, BattleRewards rewards);

    public void MakeChoice();

    public void SetSquad();

    public void MakeGacha();

}
