using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace GildedRose.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var json = File.ReadAllText("Items.json");
            var items = JsonSerializer.Deserialize<List<SerializableItem>>(json);
            var map = items.ToDictionary(serializedItem => serializedItem.Name, serializedItem => serializedItem.Category);
            var itemsList = items.Select(x => x.ToItem()).ToList();
            
            var logic = new ProgramLogic(itemsList, map);
            logic.UpdateQuality();

            System.Console.ReadKey();
        }        
    }
}
