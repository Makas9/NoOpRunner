using NoOpRunner.Core.Shapes;
using System.Linq;

namespace NoOpRunner.Core.Visitors
{
    public class VisibleBlockCalculatingVisitor : INodeVisitor
    {
        public int VisibleShapeBlockCount { get; private set; } = 0;

        public void VisitEntityShape(EntityShape shape)
        {
            return; // Entity shapes do not calculate towards visible blocks
        }

        public void VisitMovingShape(MovingShape shape)
        {
            return;
        }

        public void VisitStaticShape(StaticShape shape)
        {
            VisibleShapeBlockCount += shape.GetShapes().FirstOrDefault().Count(x => x.OffsetX < GameSettings.HorizontalCellCount);
        }
    }
}
