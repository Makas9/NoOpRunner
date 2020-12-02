using NoOpRunner.Core.Shapes;

namespace NoOpRunner.Core.Visitors
{
    public class EntityCalculatingVisitor : INodeVisitor
    {
        public int EntityCount { get; private set; }

        public void VisitEntityShape(EntityShape shape)
        {
            Logging.Instance.Write($"[Visitor] Entity calculating visitor visited an entity at {shape.CenterPosX}, {shape.CenterPosY}", LoggingLevel.Visitor);
            EntityCount += 1;
        }

        public void VisitMovingShape(MovingShape shape)
        {
            Logging.Instance.Write($"[Visitor] Entity calculating visitor visited a moving shape at {shape.CenterPosX}, {shape.CenterPosY}", LoggingLevel.Visitor);
            EntityCount += 1; // Moving shapes (rockets, player etc) also count as entities in this context
        }

        public void VisitStaticShape(StaticShape shape)
        {
            Logging.Instance.Write($"[Visitor] Entity calculating visitor visited a static shape with center coords {shape.CenterPosX}, {shape.CenterPosY}", LoggingLevel.Visitor);
        }
    }
}
