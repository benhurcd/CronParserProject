namespace Deliveroo.CronParser.Expander
{
    public class ExpanderOptions
    {
        public int MaxValuesPerField 
        {
            get;private set; 
        }
        public ExpanderOptions(int maxValuesPerField)
        {
            this.MaxValuesPerField = maxValuesPerField;
        }
    }
}