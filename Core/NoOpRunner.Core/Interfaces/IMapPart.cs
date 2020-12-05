using NoOpRunner.Core.Iterators;
using NoOpRunner.Core.Shapes;
using NoOpRunner.Core.Visitors;
using System.Collections.Generic;

namespace NoOpRunner.Core.Interfaces
{
    public interface IMapPart
    {
        void AddMapPart(IMapPart mapPart);

        WindowPixel[,] RenderPixels(bool ignoreCollision = false);

        WindowPixelCollection Render();

        bool IsAtPos(int centerPosX, int centerPosY);

        void ShiftShapes();

        List<List<ShapeBlock>> GetNextBlocks();

        List<T> GetOfType<T>() where T : IMapPart;

        void Accept(INodeVisitor visitor);
    }
}
