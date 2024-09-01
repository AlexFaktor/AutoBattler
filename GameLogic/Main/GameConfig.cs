using GameLogic.Tools.ShellImporters.ConfigReaders;

namespace GameLogic.Main;

public class GameConfig
{
    public CharacterConfigReader CharacterConfigReader { get; set; }

    public GameConfig(CharacterConfigReader characterConfigReader)
    {
        CharacterConfigReader = characterConfigReader;
    }
}