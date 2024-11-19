using Deliveroo.CronParser;
using Deliveroo.CronParser.Displayable;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CronParser.Tests
{
    [TestClass]
    public class CronConsoleDisplayableTests
    {
        [TestMethod]
        public void Display_ShouldOutputCorrectFormat()
        {

            // Arrange
            var displayable = new CronConsoleDisplayable();
            var cronExpression = "* * * * *";
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            displayable.Setup(new Dictionary<SubExpressionType, string>(), "abc");

            // Act
            displayable.Display();

            // Assert
            Assert.AreEqual(stringWriter.ToString().Trim(), "Command          abc");
            Console.SetOut(Console.Out);
        }
    }
}
