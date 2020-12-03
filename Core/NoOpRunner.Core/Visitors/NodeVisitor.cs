using NoOpRunner.Core.Shapes;

namespace NoOpRunner.Core.Visitors
{
    public interface INodeVisitor
    {
        void VisitStaticShape(StaticShape shape);
        void VisitEntityShape(EntityShape shape);
        void VisitMovingShape(MovingShape shape);
    }
}
