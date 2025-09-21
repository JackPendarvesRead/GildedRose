using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Console
{
    public class ProgramLogic
    {
        public ProgramLogic(IList<Item> items)
        {
            this.Items = items;
        }

        public IList<Item> Items { get; private set; }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i].Name != ItemNames.AgedBrie 
                    && Items[i].Name != ItemNames.BackstagePassesToATAFKAL80ETCConcert)
                {
                    if (Items[i].Quality > 0)
                    {
                        if (Items[i].Name != ItemNames.SulfurasHandOfRagnaros)
                        {
                            Items[i].Quality = Items[i].Quality - 1;
                        }
                    }
                }
                else
                {
                    if (Items[i].Quality < 50)
                    {
                        Items[i].Quality = Items[i].Quality + 1;

                        if (Items[i].Name == ItemNames.BackstagePassesToATAFKAL80ETCConcert)
                        {
                            if (Items[i].SellIn < 11)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }

                            if (Items[i].SellIn < 6)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }
                        }
                    }
                }

                if (Items[i].Name != ItemNames.SulfurasHandOfRagnaros)
                {
                    Items[i].SellIn = Items[i].SellIn - 1;
                }

                if (Items[i].SellIn < 0)
                {
                    if (Items[i].Name != ItemNames.AgedBrie)
                    {
                        if (Items[i].Name != ItemNames.BackstagePassesToATAFKAL80ETCConcert)
                        {
                            if (Items[i].Quality > 0)
                            {
                                if (Items[i].Name != ItemNames.SulfurasHandOfRagnaros)
                                {
                                    Items[i].Quality = Items[i].Quality - 1;
                                }
                            }
                        }
                        else
                        {
                            Items[i].Quality = Items[i].Quality - Items[i].Quality;
                        }
                    }
                    else
                    {
                        if (Items[i].Quality < 50)
                        {
                            Items[i].Quality = Items[i].Quality + 1;
                        }
                    }
                }
            }
        }
    }
}
