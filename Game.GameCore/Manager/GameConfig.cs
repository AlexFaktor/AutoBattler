using App.GameCore.Tools.ShellImporters.ConfigReaders;

namespace App.GameCore.Manager;

public class GameConfig
{
    public CharacterConfigReader CharacterConfigReader { get; set; }

    public GameConfig(CharacterConfigReader characterConfigReader)
    {
        CharacterConfigReader = characterConfigReader;
    }
}