using FluentAssertions;
using FluentAssertions.Formatting;
using GildedRose.Console;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xunit;

namespace GildedRose.Tests
{
    public class ProgramLogicUnitTests
    {
        [Fact]
        public void UpdateQuality_ReducesSellInAndItemQualityByOne_WhenNoSpecialModifiersApply()
        {
            // Arrange
            const string ItemName = "Test Item";
            const int initialQuality = 20;
            const int expectedQuality = 19;
            const int initialSellIn = 5;
            const int expectedSellIn = 4;

            var items = new List<Item>()
            {
                new Item() { Name = ItemName, Quality = initialQuality, SellIn = initialSellIn }
            };
            var dic = new Dictionary<string, ItemCategory>()
            {
                { ItemName, ItemCategory.Normal }
            };

            // Act
            var logic = new ProgramLogic(items, dic);
            logic.UpdateQuality();

            // Assert
            items[0].Quality.Should().Be(expectedQuality);
            items[0].SellIn.Should().Be(expectedSellIn);
        }        

        [Fact]
        public void UpdateQuality_DegradesItemTwiceAsFast_WhenSellByDateHasPassed()
        {
            // Arrange
            const string ItemName = "Test Item";
            const int initialQuality = 20;
            const int expectedQuality = 18;
            const int initialSellIn = 0;
            const int expectedSellIn = -1;

            var items = new List<Item>()
            {
                new Item() { Name = ItemName, Quality = initialQuality, SellIn = initialSellIn }
            };
            var dic = new Dictionary<string, ItemCategory>()
            {
                { ItemName, ItemCategory.Normal }
            };

            // Act
            var logic = new ProgramLogic(items, dic);
            logic.UpdateQuality();

            // Assert
            items[0].Quality.Should().Be(18); // Quality should reduce by 1 
        }

        [Fact]
        public void UpdateQuality_CannotReduceQualityBelowZero_WhenItemDegradeWouldGoBelowZero()
        {
            // Arrange
            const string ItemName = "Test Item";
            const int initialQuality = 0;
            const int expectedQuality = 0;
            const int initialSellIn = 5;
            const int expectedSellIn = 4;

            var items = new List<Item>()
            {
                new Item() { Name = ItemName, Quality = initialQuality, SellIn = initialSellIn }
            };
            var dic = new Dictionary<string, ItemCategory>()
            {
                { ItemName, ItemCategory.Normal }
            };

            // Act
            var logic = new ProgramLogic(items, dic);
            logic.UpdateQuality();

            // Assert
            items[0].Quality.Should().Be(0); // Quality should not go below 0
        }

        [Fact]
        public void UpdateQuality_CannotIncreaseItemQualityAbove50_WhenItemWouldIncreaseQuality()
        {
            // Arrange
            const int maxQuality = GildedRoseConstants.MaxQuality;
            const string ItemName = "Test Item";
            const int initialQuality = maxQuality;
            const int expectedQuality = maxQuality;
            const int initialSellIn = 5;
            const int expectedSellIn = 4;

            var items = new List<Item>()
            {
                new Item() { Name = ItemName, Quality = initialQuality, SellIn = initialSellIn }
            };
            var dic = new Dictionary<string, ItemCategory>()
            {
                { ItemName, ItemCategory.Aged }
            };

            // Act
            var logic = new ProgramLogic(items, dic);
            logic.UpdateQuality();

            // Assert
            items[0].Quality.Should().Be(maxQuality); // Quality should not exceed 50
        }

        [Theory]
        [InlineData(20, 21, 5, 4)]
        [InlineData(35, 37, 0, -1)]
        public void UpdateQuality_IncreasesQuality_WhenItemIsAged(int initialQuality, int expectedQuality, int initialSellIn, int expectedSellIn)
        {
            // Arrange
            const string ItemName = "Test Item";

            var items = new List<Item>()
            {
                new Item() { Name = ItemName, Quality = initialQuality, SellIn = initialSellIn }
            };
            var dic = new Dictionary<string, ItemCategory>()
            {
                { ItemName, ItemCategory.Aged }
            };

            // Act
            var logic = new ProgramLogic(items, dic);
            logic.UpdateQuality();

            // Assert
            items[0].Quality.Should().Be(expectedQuality); // Quality shoulld increase by 1
            items[0].SellIn.Should().Be(expectedSellIn); // Quality shoulld increase by 1
        }

        [Theory]
        [InlineData(20, 18, 5, 4)]
        [InlineData(35, 31, 0, -1)]
        public void UpdateQuality_DegradsAtTwiceTheRate_WhenItemIsConjured(int initialQuality, int expectedQuality, int initialSellIn, int expectedSellIn)
        {
            // Arrange
            const string ItemName = "Conjured Item";

            var items = new List<Item>()
            {
                new Item() { Name = ItemName, Quality = initialQuality, SellIn = initialSellIn }
            };
            var dic = new Dictionary<string, ItemCategory>()
            {
                { ItemName, ItemCategory.Conjured }
            };

            // Act
            var logic = new ProgramLogic(items, dic);
            logic.UpdateQuality();

            // Assert
            items[0].Quality.Should().Be(expectedQuality);
            items[0].SellIn.Should().Be(expectedSellIn); 
        }

        [Theory]
        [InlineData(100, 1)]
        [InlineData(50, 1)]
        [InlineData(12, 1)]
        [InlineData(11, 1)]
        [InlineData(10, 2)]
        [InlineData(9, 2)]
        [InlineData(8, 2)]
        [InlineData(7, 2)]
        [InlineData(6, 2)]
        [InlineData(5, 3)]
        [InlineData(4, 3)]
        [InlineData(3, 3)]
        [InlineData(2, 3)]
        [InlineData(1, 3)]
        public void UpdateQuality_IncreasesBackstagePassQualityDynamically_BasedOnSellInValue(int sellIn, int expectedIncrease)
        {
            // Arrange
            const string concertName = "Super cool concert";
            const int initialQuality = 20;
            var expectedQuality = initialQuality + expectedIncrease;

            var items = new List<Item>
            {
                new Item()
                {
                    Name = concertName,
                    SellIn = sellIn,
                    Quality = initialQuality
                }
            };
            var map = new Dictionary<string, ItemCategory>
            {
                { concertName, ItemCategory.BackstagePass }
            };

            // Act
            var logic = new ProgramLogic(items, map);
            logic.UpdateQuality();

            // Assert
            items[0].Quality.Should().Be(expectedQuality);
        }

        [Fact]
        public void UpdateQuality_SetsBackstagePassQualityToZero_WhenConcertHasAlreadyHappened()
        {
            // Arrange
            const string concertName = "The Retinal Circus";
            var items = new List<Item>
            {
                new Item()
                {
                    Name = concertName,
                    SellIn = 0,
                    Quality = 20
                }
            };
            var map = new Dictionary<string, ItemCategory>
            {
                { concertName, ItemCategory.BackstagePass }
            };

            // Act
            var logic = new ProgramLogic(items, map);
            logic.UpdateQuality();

            // Assert
            items[0].Quality.Should().Be(0); // Quality should drop to 0 after the concert
        }


        [Fact]
        public void UpdateQuality_DoesNotReduceQuality_WhenItemIsLegendary()
        {
            // Arrange
            const string legendaryName = "Frostmourne";
            var items = new List<Item>
            {
                new Item()
                {
                    Name = legendaryName,
                    SellIn = 10,
                    Quality = 80
                }
            };
            var map = new Dictionary<string, ItemCategory>
            {
                { legendaryName, ItemCategory.Legendary }
            };

            // Act
            var logic = new ProgramLogic(items, map);
            logic.UpdateQuality();

            // Assert
            items[0].Quality.Should().Be(80); // Quality should remain constant
        }

        [Fact]
        public void UpdateQuality_DoesNotReduceSellIn_WhenItemIsLegendary()
        {
            // Arrange
            const string legendaryName = "Frostmourne";
            var items = new List<Item>
            {
                new Item()
                {
                    Name = legendaryName,
                    SellIn = 10,
                    Quality = 80
                }
            };
            var map = new Dictionary<string, ItemCategory>
            {
                { legendaryName, ItemCategory.Legendary }
            };

            // Act
            var logic = new ProgramLogic(items, map);
            logic.UpdateQuality();

            // Assert
            items[0].SellIn.Should().Be(10);
        }
    }
}