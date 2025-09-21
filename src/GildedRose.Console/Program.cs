using System.Collections.Generic;

namespace GildedRose.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var logic = new ProgramLogic(InnVentory.GetAllItems());

            logic.UpdateQuality();

            System.Console.ReadKey();
        }        
    }
}
