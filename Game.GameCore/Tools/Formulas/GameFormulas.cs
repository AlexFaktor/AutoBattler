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

}
