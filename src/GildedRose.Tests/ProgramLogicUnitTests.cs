using FluentAssertions;
using GildedRose.Console;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xunit;

namespace GildedRose.Tests
{
    public class ProgramLogicUnitTests
    {
        [Fact]
        public void UpdateQuality_DegradesItemQualityByOne_WhenNoSpecialModifiersApply()
        {
            // Arrange
            var logic = new ProgramLogic(new List<Item>
            {
                new Item { Name = ItemNames.DexterityVestPlus5, SellIn = 10, Quality = 20 }
            });

            // Act
            logic.UpdateQuality();

            // Assert
            logic.Items[0].Quality.Should().Be(19); // Quality should reduce by 1 
        }

        [Fact]
        public void UpdateQuality_ReducesSellInForAllItems_WhenNoSpecialModifiersApply()
        {
            // Arrange
            var logic = new ProgramLogic(new List<Item>
            {
                new Item()
                {
                    Name = ItemNames.DexterityVestPlus5,
                    SellIn = 10,
                    Quality = 20
                }
            });

            // Act
            logic.UpdateQuality();

            // Assert
            logic.Items[0].SellIn.Should().Be(9);
        }

        [Fact]
        public void UpdateQuality_DegradesItemTwiceAsFast_WhenSellByDateHasPassed()
        {
            // Arrange
            var logic = new ProgramLogic(new List<Item>
            {
                new Item { Name = ItemNames.DexterityVestPlus5, SellIn = 0, Quality = 20 }
            });

            // Act
            logic.UpdateQuality();

            // Assert
            logic.Items[0].Quality.Should().Be(18); // Quality should reduce by 1 
        }

        [Fact]
        public void UpdateQuality_CannotReduceQualityBelowZero_WhenItemDegradeWouldGoBelowZero()
        {
            // Arrange
            var logic = new ProgramLogic(new List<Item>
            {
                new Item() { Name = ItemNames.DexterityVestPlus5, SellIn = 10, Quality = 0 }
            });

            // Act
            logic.UpdateQuality();

            // Assert
            logic.Items[0].Quality.Should().Be(0); // Quality should not go below 0
        }

        [Fact]
        public void UpdateQuality_CannotIncreaseItemQualityAbove50_WhenItemWouldIncreaseQuality()
        {
            // Arrange
            const int maxQuality = 50;

            var logic = new ProgramLogic(new List<Item>
            {
                new Item() { Name = ItemNames.AgedBrie, SellIn = 10, Quality = maxQuality }
            });

            // Act
            logic.UpdateQuality();

            // Assert
            logic.Items[0].Quality.Should().Be(maxQuality); // Quality should not exceed 50
        }

        [Fact]
        public void UpdateQuality_IncreasesQuality_WhenItemIsAgedBree()
        {
            // Arrange
            var logic = new ProgramLogic(new List<Item>
            {
                new Item() { Name = ItemNames.AgedBrie, SellIn = 10, Quality = 10 }
            });

            // Act
            logic.UpdateQuality();

            // Assert
            logic.Items[0].Quality.Should().Be(11); // Quality shoulld increase by 1
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
            const int initialQuality = 20;
            var expectedQuality = initialQuality + expectedIncrease;

            var logic = new ProgramLogic(new List<Item>
            {
                new Item()
                {
                    Name = ItemNames.BackstagePassesToATAFKAL80ETCConcert,
                    SellIn = sellIn,
                    Quality = initialQuality
                }
            });

            // Act
            logic.UpdateQuality();

            // Assert
            logic.Items[0].Quality.Should().Be(expectedQuality);
        }

        [Fact]
        public void UpdateQuality_SetsBackstagePassQualityToZero_WhenConcertHasAlreadyHappened()
        {
            // Arrange
            var logic = new ProgramLogic(new List<Item>
            {
                new Item()
                {
                    Name = ItemNames.BackstagePassesToATAFKAL80ETCConcert,
                    SellIn = 0,
                    Quality = 20
                }
            });

            // Act
            logic.UpdateQuality();

            // Assert
            logic.Items[0].Quality.Should().Be(0); // Quality should drop to 0 after the concert
        }


        [Fact]
        public void UpdateQuality_DoesNotReduceQuality_WhenItemIsLegendary()
        {
            // Arrange
            var logic = new ProgramLogic(new List<Item>
            {
                new Item()
                {
                    Name = ItemNames.SulfurasHandOfRagnaros,
                    SellIn = 10,
                    Quality = 80
                }
            });

            // Act
            logic.UpdateQuality();

            // Assert
            logic.Items[0].Quality.Should().Be(80); // Quality should remain constant
        }

        [Fact]
        public void UpdateQuality_DoesNotReduceSellIn_WhenItemIsLegendary()
        {
            // Arrange
            var logic = new ProgramLogic(new List<Item>
            {
                new Item()
                {
                    Name = ItemNames.SulfurasHandOfRagnaros,
                    SellIn = 10,
                    Quality = 80
                }
            });

            // Act
            logic.UpdateQuality();

            // Assert
            logic.Items[0].SellIn.Should().Be(10);
        }
    }
}