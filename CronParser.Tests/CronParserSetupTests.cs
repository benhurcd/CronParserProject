using Deliveroo.CronParser;
using Deliveroo.CronParser.Displayable;
using Deliveroo.CronParser.Expander;
using Deliveroo.CronParser.Setup;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CronParser.Tests
{
    [TestClass]
    public class CronParserSetupTests
    {
        [TestMethod]
        public void Setup_ShouldReturnServiceProvider()
        {
            // Act
            var serviceProvider = CronParserSetup.Setup();

            // Assert
            Assert.IsNotNull(serviceProvider);
            Assert.IsInstanceOfType(serviceProvider, typeof(ServiceProvider));
        }

        [TestMethod]
        public void Setup_ShouldResolveServices()
        {
            // Arrange
            var serviceProvider = CronParserSetup.Setup();

            // Act & Assert
            var cronDisplayable = serviceProvider.GetService<ICronDisplayable>();
            Assert.IsNotNull(cronDisplayable);
            Assert.IsInstanceOfType(cronDisplayable, typeof(CronConsoleDisplayable));

            var subCronExpander = serviceProvider.GetService<ISubCronExpander>();
            Assert.IsNotNull(subCronExpander);
            Assert.IsInstanceOfType(subCronExpander, typeof(GenericSubCronNumericExpander));

            var cronParser = serviceProvider.GetService<ICronParser>();
            Assert.IsNotNull(cronParser);
            Assert.IsInstanceOfType(cronParser, typeof(CronConcreteParser));
        }
    }
}
