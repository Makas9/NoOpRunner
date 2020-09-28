using NoOpRunner.Core.Entities;

namespace NoOpRunner.Core.ShapeFactory
{
    public class StaticShapeFactory : BaseShapeFactory
    {
        //TODO: static obstacle or platform cell factory 
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