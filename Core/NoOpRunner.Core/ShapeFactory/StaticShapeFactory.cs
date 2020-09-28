using NoOpRunner.Core.Entities;

namespace NoOpRunner.Core.ShapeFactory
{
    public class StaticShapeFactory : BaseShapeFactory
    {
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