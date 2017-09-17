using System.Linq;
using GildedRose.Console;
using NUnit.Framework;

namespace GildedRose.Tests
{
    public class GuildedRoseAppTests
    {
        private Item _dexterityVest;
        private Item _agedBrie;
        private Item _mongooseElexir;
        private Item _sulfuras;
        private Item _backstagePasses;
        private Item _conjuredManaCake;
        private GuildedRoseApp _app;

        [SetUp]
        public void Setup()
        {
            _app = new GuildedRoseApp();
            _dexterityVest = _app.Items.Single(x => x.Name == "+5 Dexterity Vest");
            _agedBrie = _app.Items.Single(x => x.Name == "Aged Brie");
            _mongooseElexir = _app.Items.Single(x => x.Name == "Elixir of the Mongoose");
            _sulfuras = _app.Items.Single(x => x.Name == "Sulfuras, Hand of Ragnaros");
            _backstagePasses = _app.Items.Single(x => x.Name == "Backstage passes to a TAFKAL80ETC concert");
            _conjuredManaCake = _app.Items.Single(x => x.Name == "Conjured Mana Cake");
        }

        [Test]
        public void SellIn_and_quality_of_each_item_are_updated_correctly_after_one_day()
        {
            _app.UpdateQuality();

            Assert.That(_dexterityVest.Quality, Is.EqualTo(19), "Dexterity Vest's Quality should degrade by 1");
            Assert.That(_dexterityVest.SellIn, Is.EqualTo(9),   "Dexterity Vest's SellIn should degrade by 1");

            Assert.That(_agedBrie.Quality, Is.EqualTo(1), "Aged Brie's Quality should increase by 1");
            Assert.That(_agedBrie.SellIn, Is.EqualTo(1),  "Aged Brie's SellIn should degrade by 1");

            Assert.That(_mongooseElexir.Quality, Is.EqualTo(6), "Elixir of the Mongoose's Quality should degrade by 1");
            Assert.That(_mongooseElexir.SellIn, Is.EqualTo(4),  "Elixir of the Mongoose's SellIn should degrade by 1");

            Assert.That(_sulfuras.Quality, Is.EqualTo(80), "Sulfuras's Quality never degrades");
            Assert.That(_sulfuras.SellIn, Is.EqualTo(0),   "Sulfuras's SellIn should remain at 0");

            Assert.That(_backstagePasses.Quality, Is.EqualTo(21), "Backstage Passes' Quality should increase by 1");
            Assert.That(_backstagePasses.SellIn, Is.EqualTo(14),  "Backstage Passes' SellIn should decrease by 1");

            Assert.That(_conjuredManaCake.Quality, Is.EqualTo(5), "Conjured Mana Cake' Quality should decrease by 1");
            Assert.That(_conjuredManaCake.SellIn, Is.EqualTo(2),  "Conjured Mana Cake' SellIn should decrease by 1");
        }

        /*TODO:
        (1) Once the sell by date has passed, Quality degrades twice as fast
        (2) The Quality of an item is never negative
        (3) "Aged Brie" actually increases in Quality the older it gets
        (4) The Quality of an item is never more than 50 (except Sulfuras)
        (5) "Sulfuras" is a legendary item and as such its Quality is 80 and it never alters.
        (6) "Backstage passes", like aged brie, increases in Quality as it's SellIn value
            approaches; Quality increases by 2 when there are 10 days or less and by 3 when 
            there are 5 days or less but Quality drops to 0 after the concert
        */
    }
}
 