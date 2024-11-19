using Deliveroo.CronParser.Expander;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CronParser.Tests
{
    [TestClass]
    public class GenericSubCronNumericExpanderTests
    {
        [TestMethod]
        public void Expand_ShouldReturnExpectedValues()
        {
            // Arrange
            var options = new ExpanderOptions(14);
            var expander = new GenericSubCronNumericExpander(options);
            var subCronExpression = "1-5";

            // Act
            var result = expander.Expand(subCronExpression, Deliveroo.CronParser.SubExpressionType.Hour);

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(new[] { "1", "2", "3", "4", "5" }, result.ToArray());
        }
    }
}
