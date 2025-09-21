using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Console
{
    public static class InnVentory
    {
        public static Item DexterityVestPlus5 = new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 };
        public static Item AgedBrie = new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 };
        public static Item ElixirOfTheMongoose = new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 };
        public static Item SulfurasHandOfRagnaros = new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 };
        public static Item BackstagePassesToATAFKAL80ETCConcert = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 };
        public static Item ConjuredManaCake = new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 };
    
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
    }
}
