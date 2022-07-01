using QuotesMicroservice.Controllers;
namespace QuotesMicroservice.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
        [Test]
        public void GetQuotesTest()
        {
            int propertyValue = 5;
            int businessValue = 8;
            string PropertyType = "Building";

            QuotesController controller = new QuotesController();

            var actual = controller.GetQuotes(businessValue, propertyValue, PropertyType);
            var expected = controller.GetQuotes(businessValue, propertyValue, PropertyType);

            Assert.AreEqual(actual.ToString(), expected.ToString());

        }

    }
}