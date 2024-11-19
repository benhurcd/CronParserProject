using Deliveroo.CronParser.Displayable;
using Deliveroo.CronParser.Expander;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliveroo.CronParser
{
    public interface ICronParser
    {
        void ParseAndDisplay(string cronExpression, string command);

        void SetSubCronExpanderBehavior(ISubCronExpander expander);

        void SetDisplayableBehavior(ICronDisplayable displayable);
    }
}
