using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Console
{
    public class SerializableItem
    {
        public SerializableItem() 
        { 
        }

        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }

        public ItemCategory Category { get; set; }

        public Item ToItem()
        {
            return new Item() { Name = Name, SellIn = SellIn, Quality = Quality };
        }
    }
}
