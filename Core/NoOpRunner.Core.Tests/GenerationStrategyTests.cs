using NoOpRunner.Core.Shapes;
using NoOpRunner.Core.Shapes.GenerationStrategies;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace NoOpRunner.Core.Tests
{
    public class GenerationStrategyTests
    {
        [Fact]
        public void AddShapeBlock_WhenCalled_ShouldAddBlockWithCorrectCoordinatesToList()
        {
            // Arrange
            var blocks = new List<ShapeBlock>();
            var offsetX = 3;
            var offsetY = 4;

            // Act
            GenerationStrategy.AddShapeBlock(blocks, offsetX, offsetY);

            // Assert
            blocks.Count.ShouldBe(1);
            blocks[0].OffsetX.ShouldBe(offsetX);
            blocks[0].OffsetY.ShouldBe(offsetY);
        }

        [Fact]
        public void MapShapeX_WhenCalled_ShouldAddHorizontalShapeOfExpectedLength()
        {
            // Arrange
            var blocks = new List<ShapeBlock>();
            var offsetX = 3;
            var offsetY = 4;
            var len = 3;

            // Act
            GenerationStrategy.MapShapeX(blocks, offsetX, offsetY, len);

            // Assert
            blocks.Count.ShouldBe(len);
        }

        [Fact]
        public void MapShapeX_WhenShapeIsMapped_ShouldStartMappingAtProvidedOffset()
        {
            // Arrange
            var blocks = new List<ShapeBlock>();
            var offsetX = 3;
            var offsetY = 4;
            var len = 3;

            // Act
            GenerationStrategy.MapShapeX(blocks, offsetX, offsetY, len);

            // Assert
            blocks.TrueForAll(x => x.OffsetY == offsetY).ShouldBeTrue();
            blocks[0].OffsetX.ShouldBe(offsetX);
            blocks[1].OffsetX.ShouldBe(offsetX + 1);
        }

        [Fact]
        public void MapShapeY_WhenCalled_ShouldAddVerticalShapeOfExpectedLength()
        {
            // Arrange
            var blocks = new List<ShapeBlock>();
            var offsetX = 2;
            var offsetY = 5;
            var len = 4;

            // Act
            GenerationStrategy.MapShapeY(blocks, offsetX, offsetY, len);

            // Assert
            blocks.Count.ShouldBe(len);
        }

        [Fact]
        public void MapShapeY_WhenShapeIsMapped_ShouldStartMappingAtProvidedOffset()
        {
            // Arrange
            var blocks = new List<ShapeBlock>();
            var offsetX = 2;
            var offsetY = 4;
            var len = 3;

            // Act
            GenerationStrategy.MapShapeY(blocks, offsetX, offsetY, len);

            // Assert
            blocks.TrueForAll(x => x.OffsetX == offsetX).ShouldBeTrue();
            blocks[0].OffsetY.ShouldBe(offsetY);
            blocks[1].OffsetY.ShouldBe(offsetY + 1);
        }

        [Fact]
        public void MakeRelative_WhenCalled_ShouldCreateNewBlockListWithGivenCoordinatesSubstractedFromOffset()
        {
            // Arrange
            var blocks = new List<ShapeBlock>()
            {
                new ShapeBlock() { OffsetX = 6, OffsetY = 7 },
                new ShapeBlock() { OffsetX = 10, OffsetY = 10 }
            };
            var expected = new List<ShapeBlock>() 
            { 
                new ShapeBlock() { OffsetX = 2, OffsetY = 4 },
                new ShapeBlock() { OffsetX = 6, OffsetY = 7 } 
            };

            // Act
            var rez = GenerationStrategy.MakeRelative(blocks, 4, 3);

            // Assert
            rez.ShouldBeEquivalentTo(expected);
        }
    }
}
