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
    /// 
    /// </summary>
    public static float PercentageOfArmorImpact(double armor)
    {
        const double MAX_ARMOR = 10000;
        const double MIN_ARMOR = 0;
        const double BALANCE_COEF = 500;

        if (armor > MAX_ARMOR - BALANCE_COEF)
            return 0.05f;
        else if (armor < MIN_ARMOR)
            return 0f;
        else
        {
            var formuleArmor = MAX_ARMOR - BALANCE_COEF;
            return (float)(armor / formuleArmor);
        }
    }

    public static double DamageWithArmorAndIgnoringArmorAndPiercingArmor
        (double damage,
        double armor, 
        float piercingArmor, 
        float ignoringArmor)
    {

    }

}
