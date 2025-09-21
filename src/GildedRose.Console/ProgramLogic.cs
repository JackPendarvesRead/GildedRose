using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Console
{
    public class ProgramLogic
    {
        private IList<Item> items;
        private readonly Dictionary<string, ItemCategory> itemCategoryMap;

        public ProgramLogic(
            IList<Item> items, 
            Dictionary<string, ItemCategory> itemCategoryMap)
        {
            this.items = items;
            this.itemCategoryMap = itemCategoryMap;
        }


        public void UpdateQuality()
        {
            foreach (var item in items)
            {
                var category = itemCategoryMap[item.Name];
                var qualityFunction = category.GetQualityFunction();
                qualityFunction(item);
            }
        }
    }
}
