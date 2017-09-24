using System.Collections.Generic;

namespace GildedRose.Console
{
	public class GuildedRoseApp
	{
		public List<Item> Items = new List<Item>
		{
			new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
			new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
			new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
			new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
			new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20},
			new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
		};

		public void UpdateQuality()
		{
			for (var i = 0; i < Items.Count; i++)
			{
				if (Items[i].Name == "Sulfuras, Hand of Ragnaros")
				{
					continue;
				}
				if (Items[i].Name == "Aged Brie")
				{
					IncreaseQualityBy(1, i);
				}
				else if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
				{
					if (Items[i].Quality < 50)
					{
						Items[i].Quality += 1;

						if (Items[i].SellIn < 11)
						{
							IncreaseQualityBy(1, i);
						}
						if (Items[i].SellIn < 6)
						{
							IncreaseQualityBy(1, i);
						}
					}
				}
				else
				{
					DecreaseQualityBy(1, i);
				}

				Items[i].SellIn -= 1;

				if (Items[i].SellIn >= 0) continue;
				if (Items[i].Name != "Aged Brie")
				{
					if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
					{
						if (Items[i].Quality > 0)
						{
							Items[i].Quality -= 1;
						}
					}
					else
					{
						Items[i].Quality = 0;
					}
				}
				else
				{
					IncreaseQualityBy(1, i);
				}
			}
		}

		private void IncreaseQualityBy(int additionalQuality, int i)
		{
			if (Items[i].Quality < 50)
			{
				Items[i].Quality += additionalQuality;
			}
		}

		private void DecreaseQualityBy(int subtractingQuality, int i)
		{
			if (Items[i].Quality > 0)
			{
				Items[i].Quality -= subtractingQuality;
			}
		}
	}
}