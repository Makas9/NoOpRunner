using NoOpRunner.Core.Shapes;
using System.Linq;

namespace NoOpRunner.Core.Visitors
{
    public class UnrenderedBlockCalculatingVisitor : INodeVisitor
    {
        public int UnrenderedShapeBlockCount { get; private set; }

        public void VisitEntityShape(EntityShape shape)
        {
            // Entities do not count towards blocks, so do nothing
        }

        public void VisitMovingShape(MovingShape shape)
        {
        }

        public void VisitStaticShape(StaticShape shape)
        {
            UnrenderedShapeBlockCount += shape.GetShapes().FirstOrDefault().Count(x => x.OffsetX >= GameSettings.HorizontalCellCount);
        }
    }
}
