using Deliveroo.CronParser;
using Deliveroo.CronParser.Displayable;
using Deliveroo.CronParser.Expander;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CronParser.Tests
{
    [TestClass]
    public class CronConcreteParserTests
    {
        private CronConcreteParser parser;

        public CronConcreteParserTests()
        {
            //Use moq to setup dependencies
            var mockExpander = new Mock<ISubCronExpander>();
            mockExpander.Setup(x => x.Validate(It.IsAny<string>(), It.IsAny<SubExpressionType>()));
            mockExpander.Setup(x => x.Expand(It.IsAny<string>(), It.IsAny<SubExpressionType>())).Returns(new List<string> { "1", "1", "1", "1", "1" });

            var mockDisplayable = new Mock<ICronDisplayable>();
            mockDisplayable.Setup(x => x.Setup(It.IsAny<Dictionary<SubExpressionType, string>>(), It.IsAny<string>()));
            mockDisplayable.Setup(x => x.Display()).Callback(()=> { Console.WriteLine("1 1 1 1 1"); });

            // Arrange
            parser = new CronConcreteParser(mockExpander.Object, mockDisplayable.Object);

        }
        [TestMethod]
        public void Parse_ShouldReturnExpectedResult()
        {
            var cronExpression = "1 1 1 1 1";
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            // Act
            parser.ParseAndDisplay(cronExpression, "abc");
            Assert.AreEqual(cronExpression, stringWriter.ToString().Trim());

            Console.SetOut(Console.Out);

        }
    }
}
