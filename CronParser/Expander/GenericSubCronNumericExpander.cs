using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliveroo.CronParser.Expander
{
    public class GenericSubCronNumericExpander: ISubCronExpander
    {
        public IEnumerable<SubExpressionType> supportedTypes = Enum.GetValues(typeof(SubExpressionType)).Cast<SubExpressionType>();

        public ExpanderOptions expanderOptions;

        public GenericSubCronNumericExpander(ExpanderOptions options)
        {
            this.expanderOptions = options;
        }

        public void SetExpanderOptions(ExpanderOptions expanderOptions)
        {
            this.expanderOptions = expanderOptions;
        }

        public void Validate(string cronSubExpression, SubExpressionType type)
        {
            if (!supportedTypes.Contains(type))
            {
                throw new ArgumentException("Unsupported sub-expression type");
            }

            if (cronSubExpression.Contains("-"))
            {
                var range = cronSubExpression.Split('-');
                if (range.Length != 2)
                {
                    throw new ArgumentException($"Invalid expression for {type}");
                }
                var rangeStart = range[0];
                var rangeEnd = range[1];

                Validate(rangeStart, type);
                Validate(rangeEnd, type);

                if (int.Parse(rangeEnd) < int.Parse(rangeStart))
                {
                    throw new ArgumentException($"Invalid range for {type}");
                }
                
            } else if(cronSubExpression.Contains("/"))
            {
                if(cronSubExpression.Split('/').Length != 2)
                {
                    throw new ArgumentException($"Invalid expression for {type}");
                }
                var step = int.Parse(cronSubExpression.Split('/')[1]);
                if (step < 1 || step > SubExpressionTypeUtils.subExpressionTypeRange[type][1])
                {
                    throw new ArgumentException($"Invalid step value {step} for {type}");
                }
            } 
            else if(cronSubExpression.Contains(","))
            {
                var values = cronSubExpression.Split(',');
                foreach (var value in values)
                {
                    Validate(value, type);
                }
            }
            else if (cronSubExpression != "*")
            {
                var value = int.Parse(cronSubExpression);
                var start = SubExpressionTypeUtils.subExpressionTypeRange[type][0];
                var end = SubExpressionTypeUtils.subExpressionTypeRange[type][1];
                if (value < start || value > end)
                {
                    throw new ArgumentException($"Invalid value {value} for {type}");
                }
            }
        }

        public List<string> Expand(string cronSubExpression, SubExpressionType type)
        {
            switch (type)
            {
                case SubExpressionType.Minute:
                case SubExpressionType.Hour:
                case SubExpressionType.DayOfMonth:
                case SubExpressionType.Month:
                case SubExpressionType.DayOfWeek:
                    return Expand(cronSubExpression, SubExpressionTypeUtils.subExpressionTypeRange[type][0], SubExpressionTypeUtils.subExpressionTypeRange[type][1]);
                default:
                    throw new ArgumentException("Invalid sub-expression type");
            }
        }

        private List<string> Expand(string cronSubExpression, int start, int end)
        {
            int maxCount = expanderOptions.MaxValuesPerField;
            if (cronSubExpression == "*")
            {
                maxCount = (maxCount < end - start + 1) ? maxCount : end - start + 1;
                return Enumerable.Range(start, maxCount).Select(x => x.ToString()).ToList();
            }
            else if(cronSubExpression.Contains("-"))
            {
                var range = cronSubExpression.Split('-');
                var rangeStart = int.Parse(range[0]);
                var rangeEnd = int.Parse(range[1]);
                maxCount = (maxCount < rangeEnd - rangeStart + 1) ? maxCount : rangeEnd - rangeStart + 1;

                return Enumerable.Range(rangeStart, maxCount).Select(x => x.ToString()).ToList();
            }
            else if (cronSubExpression.Contains("/"))
            {
                var step = int.Parse(cronSubExpression.Split('/')[1]);
                return Enumerable.Range(start, end - start + 1).Where(x => (x - start) % step == 0).Select(x => x.ToString()).Take(maxCount).ToList();
            }
            else if (cronSubExpression.Contains(","))
            {
                var values = cronSubExpression.Split(',');
                if (values.Length < maxCount) values = values.Take(maxCount).ToArray();
                var result = new List<string>();
                foreach (var value in values)
                {
                    result.AddRange(Expand(value, start, end));
                }
                return result;
            }
            else
            {
                return new List<string> { cronSubExpression };
            }
        }
    }
}
