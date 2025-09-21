using FluentAssertions;
using GildedRose.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GildedRose.Tests
{
    public class IntegrationTests
    {
        [Theory]
        [MemberData(nameof(GetSimulationTestCases))]
        public void FullRunSimulationTests(
            List<Item> initialItems,
            int daysToSimulate,
            List<Item> expectedState)
        {
            var app = new ProgramLogic(initialItems, Map);
            for(var day = 0; day < daysToSimulate; day++)
            {
                app.UpdateQuality();
            }
            app.Items.Should().BeEquivalentTo(expectedState);
        }

        public static IEnumerable<object[]> GetSimulationTestCases()
        {
            yield return new object[]
            {
                InitialItems,
                1,
                new List<Item>
                {
                    new Item { Name = "+5 Dexterity Vest", SellIn = 9, Quality = 19 },
                    new Item { Name = "Aged Brie", SellIn = 1, Quality = 1 },
                    new Item { Name = "Elixir of the Mongoose", SellIn = 4, Quality = 6 },
                    new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
                    new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 14, Quality = 21 },
                    new Item { Name = "Conjured Mana Cake", SellIn = 2, Quality = 4} // Quality should be 4 when conjured implemented
                }
            };

            yield return new object[]
            {
                InitialItems,
                2,
                new List<Item>
                {
                    new Item { Name = "+5 Dexterity Vest", SellIn = 8, Quality = 18 },
                    new Item { Name = "Aged Brie", SellIn = 0, Quality = 2 },
                    new Item { Name = "Elixir of the Mongoose", SellIn = 3, Quality = 5 },
                    new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
                    new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 13, Quality = 22 },
                    new Item { Name = "Conjured Mana Cake", SellIn = 1, Quality = 2} // Quality should be 4 when conjured implemented
                }
            };

            yield return new object[]
            {
                InitialItems,
                5,
                new List<Item>
                {
                    new Item { Name = "+5 Dexterity Vest", SellIn = 5, Quality = 15 },
                    new Item { Name = "Aged Brie", SellIn = -3, Quality = 8 },
                    new Item { Name = "Elixir of the Mongoose", SellIn = 0, Quality = 2 },
                    new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
                    new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 25 },
                    new Item { Name = "Conjured Mana Cake", SellIn = -2, Quality = 0} // Quality should be 0 when conjured implemented
                }
            };

            yield return new object[]
            {
                InitialItems,
                10,
                new List<Item>
                {
                    new Item { Name = "+5 Dexterity Vest", SellIn = 0, Quality = 10 },
                    new Item { Name = "Aged Brie", SellIn = -8, Quality = 18 },
                    new Item { Name = "Elixir of the Mongoose", SellIn = -5, Quality = 0 },
                    new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
                    new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 35 },
                    new Item { Name = "Conjured Mana Cake", SellIn = -7, Quality = 0} // Quality should be 0 when conjured implemented
                }
            };

            yield return new object[]
            {
                InitialItems,
                12,
                new List<Item>
                {
                    new Item { Name = "+5 Dexterity Vest", SellIn = -2, Quality = 6 },
                    new Item { Name = "Aged Brie", SellIn = -10, Quality = 22 },
                    new Item { Name = "Elixir of the Mongoose", SellIn = -7, Quality = 0 },
                    new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
                    new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 3, Quality = 41 },
                    new Item { Name = "Conjured Mana Cake", SellIn = -9, Quality = 0} // Quality should be 0 when conjured implemented
                }
            };

        }

        private static List<Item> InitialItems => new List<Item>
        {
            new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
            new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 },
            new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
            new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 },
            new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
        };

        private static Dictionary<string, ItemCategory> Map = new Dictionary<string, ItemCategory>
        {
            { "+5 Dexterity Vest", ItemCategory.Normal },
            { "Aged Brie", ItemCategory.Aged },
            { "Elixir of the Mongoose", ItemCategory.Normal },
            { "Sulfuras, Hand of Ragnaros", ItemCategory.Legendary },
            { "Backstage passes to a TAFKAL80ETC concert", ItemCategory.BackstagePass },
            { "Conjured Mana Cake", ItemCategory.Conjured }
        };
    }
}
