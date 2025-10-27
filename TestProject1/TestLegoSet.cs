using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace TestProject1
{
    public class TestLegoSet
    {

        List<LegoSet> setsfortesting = new();
            public TestLegoSet()
        {
            setsfortesting.Add(new LegoSet("whyistheidastring", "macaroniandcheese", 1999, 5, 2, 5, "checkoutmypatreonat", 555));
            setsfortesting.Add(new LegoSet("whyistheidastring2", "macaroniandcheese2", 1999, 5, 2, 5, "checkoutmypatreonat2", 555));

        }


        [Theory]
        [InlineData(22.0)]
        [InlineData(22.5)]
        [InlineData(null)]
        public void TestPriceValid(double? nambazu)
        {
            LegoSet s = setsfortesting[0];
            s.RetailPrice = nambazu;
            Assert.Equal(nambazu, s.RetailPrice);
            
        }

        [Theory]
        [InlineData(0.0)]
        [InlineData(-8.0)]
        [InlineData(-22.6)]
        public void TestPriceInvalid(double? nambazu)
        {
            LegoSet s = setsfortesting[0];
            Assert.Throws<LegoException>(() => s.RetailPrice = nambazu);

        }

        [Theory]
        [InlineData(22)]
        [InlineData(null)]
        public void TestMinAgeValid(int? nambazu)
        {
            LegoSet s = setsfortesting[0];
            s.MinAge = nambazu;
            Assert.Equal(nambazu, s.MinAge);

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-8)]
        public void TestMinAgeInvalid(int? nambazu)
        {
            LegoSet s = setsfortesting[0];
            Assert.Throws<LegoException>(() => s.MinAge = nambazu);

        }

        [Fact]
        public void TestAddLegoSetValid()
        {
            LegoSet s = setsfortesting[0];
            LegoSet s2 = setsfortesting[1];

            LegoTheme ye = new LegoTheme("ye");

            ye.AddLegoSet(s);
            ye.AddLegoSet(s2);
            Assert.Equal((setsfortesting[0]), ye.LegoSets[0]);
            Assert.Equal((setsfortesting[1]), ye.LegoSets[1]);
        }

        [Fact]
        public void TestAddLegoSetInvalid()
        {
            LegoSet s = setsfortesting[0];

            LegoTheme ye = new LegoTheme("ye");

            ye.AddLegoSet(s);
            Assert.Throws<LegoException>(() => ye.AddLegoSet(s));
        }

    }
}
