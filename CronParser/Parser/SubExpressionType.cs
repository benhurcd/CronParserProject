namespace Deliveroo.CronParser
{
    public class SubExpressionTypeUtils
    {
        public static Dictionary<SubExpressionType, int[]> subExpressionTypeRange = new Dictionary<SubExpressionType, int[]>
            {
                { SubExpressionType.Minute, new int[] { 0, 59 } },
                { SubExpressionType.Hour, new int[] { 0, 23 } },
                { SubExpressionType.DayOfMonth, new int[] { 1, 31 } },
                { SubExpressionType.Month, new int[] { 1, 12 } },
                { SubExpressionType.DayOfWeek, new int[] { 1, 7 } }
            };
    }

    /*
     * Enum for all supported SUb expressions for the project sorted by the granularity/sequence of appearance
     */
    public enum SubExpressionType
    {
        Minute = 1,
        Hour = 2,
        DayOfMonth = 3,
        Month = 4,
        DayOfWeek = 5
    }
}
