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
			Assert.That(_dexterityVest.SellIn, Is.EqualTo(9), "Dexterity Vest's SellIn should degrade by 1");

			Assert.That(_agedBrie.Quality, Is.EqualTo(1), "Aged Brie's Quality should increase by 1");
			Assert.That(_agedBrie.SellIn, Is.EqualTo(1), "Aged Brie's SellIn should degrade by 1");

			Assert.That(_mongooseElexir.Quality, Is.EqualTo(6), "Elixir of the Mongoose's Quality should degrade by 1");
			Assert.That(_mongooseElexir.SellIn, Is.EqualTo(4), "Elixir of the Mongoose's SellIn should degrade by 1");

			Assert.That(_sulfuras.Quality, Is.EqualTo(80), "Sulfuras's Quality never degrades and should remain at 80");
			Assert.That(_sulfuras.SellIn, Is.EqualTo(0), "Sulfuras's SellIn should remain at 0");

			Assert.That(_backstagePasses.Quality, Is.EqualTo(21), "Backstage Passes' Quality should increase by 1");
			Assert.That(_backstagePasses.SellIn, Is.EqualTo(14), "Backstage Passes' SellIn should decrease by 1");

			Assert.That(_conjuredManaCake.Quality, Is.EqualTo(5), "Conjured Mana Cake' Quality should decrease by 1");
			Assert.That(_conjuredManaCake.SellIn, Is.EqualTo(2), "Conjured Mana Cake' SellIn should decrease by 1");
		}

		[TestCase(2, 2)]
		[TestCase(5, 8)]
		[TestCase(10, 18)]
		[TestCase(25, 48)]
		public void Aged_Brie_keeps_increasing_in_quality(int numberOfDays, int expectedQuality)
		{
			for (var i = 0; i < numberOfDays; i++)
			{
				_app.UpdateQuality();
			}

			Assert.That(_agedBrie.Quality, Is.EqualTo(expectedQuality));
		}

		[TestCase(26)]
		[TestCase(30)]
		[TestCase(100)]
		public void Aged_Brie_has_max_quality_of_50(int numberOfDays)
		{
			for (var i = 0; i < numberOfDays; i++)
			{
				_app.UpdateQuality();
			}

			Assert.That(_agedBrie.Quality, Is.EqualTo(50));
		}

		[TestCase(2, 22)]
		[TestCase(5, 25)]
		[TestCase(6, 27)] //Quality increases by 2 per day when there are 10 days or less
		[TestCase(10, 35)] //Quality increases by 2 per day when there are 10 days or less
		[TestCase(11, 38)] //Quality increases by 3 per day when there are 5 days or less
		[TestCase(15, 50)] //Quality increases by 3 per day when there are 5 days or less
		[TestCase(16, 0)] //Quality drops to 0 on day of concert
		public void Backstage_passes_increase_in_quality_before_concert_but_then_drop_to_0(int numberOfDays, int newQuality)
		{
			for (var i = 0; i < numberOfDays; i++)
			{
				_app.UpdateQuality();
			}

			Assert.That(_backstagePasses.Quality, Is.EqualTo(newQuality));
		}

		[Test]
		public void Dexterity_vests_quality_degrades_by_2_if_sellIn_date_is_less_than_0()
		{
			while (_dexterityVest.SellIn > 0)
			{
				_app.UpdateQuality();
			}
			Assert.That(_dexterityVest.Quality, Is.EqualTo(10), "Dexterity Vest's Quality should degrade by 1 up until this point");

			_app.UpdateQuality(); //update after sell-by date has passed
			Assert.That(_dexterityVest.Quality, Is.EqualTo(8), "Dexterity Vest's Quality should degrade by 2 from this point");
		}

		[Test]
		public void Mongoose_elexirs_quality_degrades_by_2_if_sellIn_date_is_less_than_0()
		{
			while (_mongooseElexir.SellIn > 0)
			{
				_app.UpdateQuality();
			}
			Assert.That(_mongooseElexir.Quality, Is.EqualTo(2), "Mongoose Elexir's Quality should degrade by 1 up until this point");

			_app.UpdateQuality(); //update after sell-by date has passed
			Assert.That(_mongooseElexir.Quality, Is.EqualTo(0), "Mongoose Elexir's Quality should degrade by 2 from this point");
		}

		[Test]
		public void Mana_cakes_quality_degrades_by_2_if_sellIn_date_is_less_than_0()
		{
			while (_conjuredManaCake.SellIn > 0)
			{
				_app.UpdateQuality();
			}
			Assert.That(_conjuredManaCake.Quality, Is.EqualTo(3), "Conjured Mana Cake's Quality should degrade by 1 up until this point");

			_app.UpdateQuality(); //update after sell-by date has passed
			Assert.That(_conjuredManaCake.Quality, Is.EqualTo(1), "Conjured Mana Cake's Quality should degrade by 2 from this point");
		}

		[Test]
		public void The_quality_of_degrading_items_is_never_negative()
		{
			for (var i = 0; i < 100; i++)
			{
				_app.UpdateQuality();
			}

			Assert.That(_dexterityVest.Quality, Is.EqualTo(0), "Expected Dexterity Vest's Quality to remain 0 after 100 runs");
			Assert.That(_mongooseElexir.Quality, Is.EqualTo(0), "Expected Mongoose Elexir's Quality to remain 0 after 100 runs");
			Assert.That(_conjuredManaCake.Quality, Is.EqualTo(0), "Expected Conjured Mana Cake's Quality to remain 0 after 100 runs");
		}

		[TestCase(2)]
		[TestCase(5)]
		[TestCase(10)]
		[TestCase(25)]
		[TestCase(100)]
		public void Sulfuras_always_has_quality_of_80(int numberOfDays)
		{
			for (var i = 0; i < numberOfDays; i++)
			{
				_app.UpdateQuality();
			}

			Assert.That(_sulfuras.Quality, Is.EqualTo(80));
		}
	}
}