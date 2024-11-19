using Deliveroo.CronParser;
using Deliveroo.CronParser.Displayable;
using Deliveroo.CronParser.Expander;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliveroo.CronParser.Setup
{
    public class CronParserSetup
    {
        public static ServiceProvider Setup()
        {
            // Set up dependency injection
            var services = new ServiceCollection();
            services.AddSingleton<ICronDisplayable, CronConsoleDisplayable>();
            GenericSubCronNumericExpander expander = new GenericSubCronNumericExpander(new ExpanderOptions(14));
            services.AddSingleton<ISubCronExpander>(expander);
            services.AddSingleton<ICronParser, CronConcreteParser>();
            

            // Build the service provider
            return services.BuildServiceProvider();
        }
    }
}
