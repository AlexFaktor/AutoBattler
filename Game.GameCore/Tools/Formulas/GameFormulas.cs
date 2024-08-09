using System;

namespace App.GameCore.Tools.Formulas;

public static class GameFormulas
{
    /// <summary>
    /// Example:
    /// 1 / 200 * 300 = 1.5
    /// </summary>
    /// <param name="currentNumber"></param>
    /// <param name="forCoefNumber"></param>
    /// <returns></returns>
    public static double GetRatio(double currentNumber, double forCoefNumber) 
        => 1 / currentNumber * forCoefNumber;


    /// <summary>
    /// Example:
    /// 10000 = 0.95;
    /// 5000 = 0.5;
    /// 2758 = 0.2758;
    /// 333 = 0.03;
    /// 0 = 0;
    /// </summary>
    public static float PercentageOfArmorImpact(double armor)
    {
        const double MAX_ARMOR = 10000;
        const double MIN_ARMOR = 0;
        const double BALANCE_COEF = 500;

        if (armor > MAX_ARMOR - BALANCE_COEF)
            return 0.95f;
        else if (armor < MIN_ARMOR)
            return 0f;
        else
            return (float)(armor / MAX_ARMOR);
    }


    /// <summary>
    /// 
    /// </summary>
    public static double DamageWithArmorAndIgnoringArmorAndPiercingArmor
        (double damage,
        double armor, 
        float piercingArmor, 
        float ignoringArmor)
    {
        var totalArmorCeof = PercentageOfArmorImpact((armor * (1 - piercingArmor)) - ignoringArmor);
        return damage - (damage * totalArmorCeof);
    }

    public static T SelectRandomlyWithPriorities<T>(Dictionary<T, float> valuePairs, int seed)
    {
        float totalWeight = valuePairs.Values.Sum();

        Random random = new(seed);
        float randomNumber = (float)(random.NextDouble() * totalWeight);

        // Проходимо по словнику і шукаємо елемент, на який припадає випадкове число
        foreach (var pair in valuePairs)
        {
            if (randomNumber < pair.Value)
            {
                return pair.Key;
            }
            randomNumber -= pair.Value;
        }
        throw new InvalidOperationException("The object could not be selected. The dictionary may be empty or contain incorrect data.");
    }
}
