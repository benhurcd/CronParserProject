using Deliveroo.CronParser.Displayable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliveroo.CronParser.Displayable
{
    public class CronConsoleDisplayable : ICronDisplayable
    {
        private Dictionary<SubExpressionType, string>? values;

        private string? command;

        public void Display()
        {
            if(values == null || command == null)
            {
                throw new InvalidOperationException("Displayable not setup");
            }

            foreach (var value in Enum.GetValues(typeof(SubExpressionType)).Cast<SubExpressionType>())
            {
                if(values.ContainsKey(value))
                    Console.WriteLine($"{value} {new string(' ', 15-value.ToString().Length)} {values[value]}");
            }

            Console.WriteLine($"Command          {command}");
        }

        public void Setup(Dictionary<SubExpressionType, string> values, string command)
        {
            this.values = values;
            this.command = command;
        }
    }
}
