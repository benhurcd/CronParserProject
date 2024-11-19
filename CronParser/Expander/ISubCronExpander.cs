namespace Deliveroo.CronParser.Expander
{
    public interface ISubCronExpander
    {
        List<string> Expand(string cronSubExpression, SubExpressionType type);

        void Validate(string cronSubExpression, SubExpressionType type);

        void SetExpanderOptions(ExpanderOptions expanderOptions);
    }
}