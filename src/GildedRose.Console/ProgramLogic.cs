using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Console
{
    public class ProgramLogic
    {
        private readonly Dictionary<string, ItemCategory> itemCategoryMap;

        public ProgramLogic(
            IList<Item> items, 
            Dictionary<string, ItemCategory> itemCategoryMap)
        {
            this.Items = items;
            this.itemCategoryMap = itemCategoryMap;
        }

        public IList<Item> Items { get; private set; }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                var category = itemCategoryMap[item.Name];
                var qualityFunction = category.GetQualityFunction();
                qualityFunction(item);
            }
        }
    }
}
