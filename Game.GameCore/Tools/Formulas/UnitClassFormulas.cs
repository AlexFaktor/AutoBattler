using App.GameCore.Units;
using App.GameCore.Units.Enums;

namespace App.GameCore.Tools.Formulas;

internal class UnitClassFormulas
{
    /// <summary>
    /// Отримати пріорітети атаки кожного класу
    /// </summary>
    public static Dictionary<EUnitClass, float> WeightToSelect(EUnitClass unitClass1, EUnitClass unitClass2)
    {
        const float COEF_POWER_OF_SUB_CLASS = 0.5f;

        var dictionary = new Dictionary<EUnitClass, float>
        {
            { EUnitClass.None, 1f },
            { EUnitClass.Assassin, 1.5f },
            { EUnitClass.Buffer, 0.75f },
            { EUnitClass.Controller, 1f },
            { EUnitClass.Healer, 1f },
            { EUnitClass.Mage, 1f },
            { EUnitClass.Marksman, 1f },
            { EUnitClass.Support, 0.5f },
            { EUnitClass.Tank, 5f },
            { EUnitClass.Warrior, 2.5f },
            { EUnitClass.Universal, 1f }
        };
        ConsiderСlass(dictionary, unitClass1);
        ConsiderСlass(dictionary, unitClass2, COEF_POWER_OF_SUB_CLASS);
        return dictionary;

        static void ConsiderСlass(Dictionary<EUnitClass, float> dictionary, EUnitClass unitClass, float powerCoef = 1f)
        {
            switch (unitClass)
            {
                case EUnitClass.Assassin:
                    dictionary[EUnitClass.Healer] *= 2 * powerCoef;
                    dictionary[EUnitClass.Marksman] *= 2 * powerCoef;
                    dictionary[EUnitClass.Support] *= 2 * powerCoef; 
                    dictionary[EUnitClass.Tank] /= 2 * powerCoef;
                    dictionary[EUnitClass.Warrior] /= 2 * powerCoef; 
                    break;
                case EUnitClass.Marksman:
                    dictionary[EUnitClass.Assassin] *= 2 * powerCoef;
                    dictionary[EUnitClass.Tank] /= 5 * powerCoef;
                    dictionary[EUnitClass.Warrior] /= 2.5f * powerCoef;
                    break;
                case EUnitClass.Controller:
                    dictionary[EUnitClass.Assassin] *= 2 * powerCoef;
                    dictionary[EUnitClass.Healer] *= 2 * powerCoef;
                    dictionary[EUnitClass.Support] *= 2 * powerCoef;
                    break;
                case EUnitClass.Tank:
                    dictionary[EUnitClass.Assassin] *= 3 * powerCoef;
                    dictionary[EUnitClass.Tank] *= 3 * powerCoef;
                    dictionary[EUnitClass.Warrior] *= 3 * powerCoef;
                    break;

                case EUnitClass.Universal:
                case EUnitClass.Warrior:
                case EUnitClass.Mage:
                case EUnitClass.Buffer:
                case EUnitClass.Support:
                case EUnitClass.Healer:
                    break;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
