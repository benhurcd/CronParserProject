namespace Deliveroo.CronParser.Displayable
{
    public interface ICronDisplayable
    {
        void Setup(Dictionary<SubExpressionType, string> values, string command);
        void Display();
    }
}