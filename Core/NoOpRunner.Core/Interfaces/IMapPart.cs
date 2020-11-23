using NoOpRunner.Core.Shapes;
using System.Collections.Generic;

namespace NoOpRunner.Core.Interfaces
{
    public interface IMapPart
    {
        void AddMapPart(IMapPart mapPart);

        WindowPixel[,] RenderPixels(bool ignoreCollision = false);

        List<WindowPixel> Render();

        bool IsAtPos(int centerPosX, int centerPosY);

        void ShiftShapes();

        List<List<ShapeBlock>> GetNextBlocks();

        List<T> GetOfType<T>() where T : IMapPart;
    }
}
