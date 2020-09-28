using NoOpRunner.Core.Entities;

namespace NoOpRunner.Core.ShapeFactory
{
    public class MovingShapeFactory : BaseShapeFactory
    {
        //TODO: player or trap moving factory
        public override BaseShape CreateTrap()
        {
            throw new System.NotImplementedException();
        }

        public override BaseShape CreatePlatform()
        {
            throw new System.NotImplementedException();
        }
    }
}