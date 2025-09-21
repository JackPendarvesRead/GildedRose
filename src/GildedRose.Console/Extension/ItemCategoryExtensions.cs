using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Console
{
    public static class ItemCategoryExtensions
    {
        public static Action<Item> GetQualityFunction(this ItemCategory category)
        {
            switch (category)
            {
                case ItemCategory.Normal:
                    return NormalAction;
                case ItemCategory.Aged:
                    return AgedAction;
                case ItemCategory.BackstagePass:
                    return BackstagePassAction;
                case ItemCategory.Legendary:
                    return LegendaryAction;
                case ItemCategory.Conjured:
                    return ConjuredAction;
                default:
                    throw new ArgumentOutOfRangeException(nameof(category), category, null);
            };
        }

        private static Action<Item> NormalAction =>
            (item) =>
            {
                if (item.Quality > 0)
                {
                    item.Quality -= 1;
                }

                item.SellIn -= 1;
                if (item.SellIn < 0 && item.Quality > 0)
                {
                    item.Quality -= 1;
                }
            };

        private static Action<Item> AgedAction =>
            (item) =>
            {
                if (item.Quality < GildedRoseConstants.MaxQuality)
                {
                    item.Quality += 1;
                }

                item.SellIn -= 1;
                if (item.SellIn < 0 && item.Quality < GildedRoseConstants.MaxQuality)
                {
                    item.Quality += 1;
                }
            };

        private static Action<Item> BackstagePassAction =>
            (item) =>
            {
                if (item.Quality < GildedRoseConstants.MaxQuality)
                {
                    item.Quality += 1;
                    if (item.SellIn < 11 && item.Quality < GildedRoseConstants.MaxQuality)
                    {
                        item.Quality += 1;
                    }
                    if (item.SellIn < 6 && item.Quality < GildedRoseConstants.MaxQuality)
                    {
                        item.Quality += 1;
                    }
                }
                item.SellIn -= 1;
                if (item.SellIn < 0)
                {
                    item.Quality = 0;
                }
            };

        private static Action<Item> LegendaryAction =>
            (item) =>
            {
                // Legendary items do not change in Quality or SellIn
            };

        private static Action<Item> ConjuredAction =>
            NormalAction; // Return normal action until conjured action is defined.
    }
}
