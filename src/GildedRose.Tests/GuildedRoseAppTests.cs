using NUnit.Framework;

namespace GildedRose.Tests
{
    //TODO:
    //Once the sell by date has passed, Quality degrades twice as fast
    //The Quality of an item is never negative
    //"Aged Brie" actually increases in Quality the older it gets
    //The Quality of an item is never more than 50 (however "Sulfuras" is a legendary item and as such its Quality is 80 and it never alters.)
    //"Sulfuras", being a legendary item, never has to be sold or decreases in Quality
    //"Backstage passes", like aged brie, increases in Quality as it's SellIn value approaches; Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but Quality drops to 0 after the concert

    public class GuildedRoseAppTests
    {
        [Test]
        public void TestTheTruth()
        {
            
            Assert.True(true);
        }
    }
}