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

            // Read Items.json and deserialize to List<Item>
            var json = File.ReadAllText("Items.json");
            var items = JsonSerializer.Deserialize<List<SerializableItem>>(json)
                .Select(x => x.ToItem())
                .ToList();

            var logic = new ProgramLogic(items);

            logic.UpdateQuality();

            System.Console.ReadKey();
        }        
    }
}
