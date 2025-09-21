using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Console
{
    // INTERNAL USE ONLY
    // This is just to make calling the initial set of items easier
    internal static class InnVentory
    {
        public static IList<Item> GetAllItems()
        {
            return new List<Item>
            {
                DexterityVestPlus5,
                AgedBrie,
                ElixirOfTheMongoose,
                SulfurasHandOfRagnaros,
                BackstagePassesToATAFKAL80ETCConcert,
                ConjuredManaCake
            };
        }

        private static Item DexterityVestPlus5 = new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 };
        private static Item AgedBrie = new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 };
        private static Item ElixirOfTheMongoose = new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 };
        private static Item SulfurasHandOfRagnaros = new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 };
        private static Item BackstagePassesToATAFKAL80ETCConcert = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 };
        private static Item ConjuredManaCake = new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 };
    }
}
