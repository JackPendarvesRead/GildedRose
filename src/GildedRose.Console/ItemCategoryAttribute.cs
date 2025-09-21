using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Console
{
    internal class ItemCategoryAttribute : Attribute
    {
        public ItemCategoryAttribute(ItemCategory category)
        {
            Category = category;
        }

        public ItemCategory Category { get; }
    }
}
