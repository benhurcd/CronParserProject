using Deliveroo.CronParser;
using Deliveroo.CronParser.Displayable;
using Deliveroo.CronParser.Expander;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliveroo.CronParser
{
    public class CronConcreteParser : ICronParser
    {
        private ISubCronExpander expander;

        private ICronDisplayable displayable;
        public CronConcreteParser(ISubCronExpander expander, ICronDisplayable displayable)
        {
            this.displayable = displayable;
            this.expander = expander;
        }
        public void ParseAndDisplay(string cronExpression, string command)
        {
            //Parse the cron expression
            Dictionary<SubExpressionType, string> subExpressions = ValidateAndSplit(cronExpression);
            Dictionary<SubExpressionType, string> expandedSubExpressions = new Dictionary<SubExpressionType, string>();
            foreach (var subExpression in subExpressions)
            {
                expander.Validate(subExpression.Value, subExpression.Key);
                expandedSubExpressions[subExpression.Key] = string.Join(" ", expander.Expand(subExpression.Value, subExpression.Key));
            }

            //Display the cron expression
            displayable.Setup(expandedSubExpressions, command);
            displayable.Display();
        }

        public void SetDisplayableBehavior(ICronDisplayable displayable)
        {
            this.displayable = displayable;
        }

        public void SetSubCronExpanderBehavior(ISubCronExpander expander)
        {
            this.expander = expander;
        }

        private Dictionary<SubExpressionType, string> ValidateAndSplit(string cronExpression)
        {
            if(char.IsWhiteSpace(cronExpression[0]))
            {
                throw new ArgumentException("Cron expression cannot start with a space");
            }
            var parts = cronExpression.Split(' ');
            if (parts.Length != 5)
            {
                throw new ArgumentException("Invalid number of parts in cron expression");
            }
            Dictionary<SubExpressionType, string> subExpressions = new Dictionary<SubExpressionType, string>();
            subExpressions[SubExpressionType.Minute] = parts[0];
            subExpressions[SubExpressionType.Hour] = parts[1];
            subExpressions[SubExpressionType.DayOfMonth] = parts[2];
            subExpressions[SubExpressionType.Month] = parts[3];
            subExpressions[SubExpressionType.DayOfWeek] = parts[4];
            return subExpressions;
        }
    }
}
