using App.GameCore.Units;
using App.GameCore.Units.Enums;

namespace App.GameCore.Tools.Formulas;

internal class UnitClassFormulas
{
    /// <summary>
    /// Отримати пріорітети атаки кожного класу
    /// </summary>
    public static Dictionary<UnitClass, float> WeightToSelect(UnitClass unitClass1, UnitClass unitClass2)
    {
        const float COEF_POWER_OF_SUB_CLASS = 0.5f;

        var dictionary = new Dictionary<UnitClass, float>
        {
            { UnitClass.None, 1f },
            { UnitClass.Assassin, 1.5f },
            { UnitClass.Buffer, 0.75f },
            { UnitClass.Controller, 1f },
            { UnitClass.Healer, 1f },
            { UnitClass.Mage, 1f },
            { UnitClass.Marksman, 1f },
            { UnitClass.Support, 0.5f },
            { UnitClass.Tank, 5f },
            { UnitClass.Warrior, 2.5f },
            { UnitClass.Universal, 1f }
        };
        ConsiderСlass(dictionary, unitClass1);
        ConsiderСlass(dictionary, unitClass2, COEF_POWER_OF_SUB_CLASS);
        return dictionary;

        static void ConsiderСlass(Dictionary<UnitClass, float> dictionary, UnitClass unitClass, float powerCoef = 1f)
        {
            switch (unitClass)
            {
                case UnitClass.Assassin:
                    dictionary[UnitClass.Healer] *= 2 * powerCoef;
                    dictionary[UnitClass.Marksman] *= 2 * powerCoef;
                    dictionary[UnitClass.Support] *= 2 * powerCoef; 
                    dictionary[UnitClass.Tank] /= 2 * powerCoef;
                    dictionary[UnitClass.Warrior] /= 2 * powerCoef; 
                    break;
                case UnitClass.Marksman:
                    dictionary[UnitClass.Assassin] *= 2 * powerCoef;
                    dictionary[UnitClass.Tank] /= 5 * powerCoef;
                    dictionary[UnitClass.Warrior] /= 2.5f * powerCoef;
                    break;
                case UnitClass.Controller:
                    dictionary[UnitClass.Assassin] *= 2 * powerCoef;
                    dictionary[UnitClass.Healer] *= 2 * powerCoef;
                    dictionary[UnitClass.Support] *= 2 * powerCoef;
                    break;
                case UnitClass.Tank:
                    dictionary[UnitClass.Assassin] *= 3 * powerCoef;
                    dictionary[UnitClass.Tank] *= 3 * powerCoef;
                    dictionary[UnitClass.Warrior] *= 3 * powerCoef;
                    break;

                case UnitClass.Universal:
                case UnitClass.Warrior:
                case UnitClass.Mage:
                case UnitClass.Buffer:
                case UnitClass.Support:
                case UnitClass.Healer:
                    break;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
