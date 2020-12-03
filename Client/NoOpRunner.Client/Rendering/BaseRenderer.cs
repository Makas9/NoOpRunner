using System.Windows.Controls;
using NoOpRunner.Core;
using NoOpRunner.Core.Interfaces;

namespace NoOpRunner.Client.Rendering
{
    public abstract class BaseRenderer : IVisualElement
    {
        protected ShapesContainer ShapesContainer { get; set; }

        protected BaseRenderer(ShapesContainer shapesContainer)
        {
            ShapesContainer = shapesContainer;
        }
        
        public abstract void RenderContainerElements(Canvas canvas, out int canvasChildIndex, out int canvasChildCount);
        
        public void Display(Canvas canvas)
        {
            RenderContainerElements(canvas, out var canvasChildIndex, out var canvasChildCount);
            
            if (canvasChildIndex >= canvasChildCount) 
                
                return;
            
            for (int i = canvasChildIndex; i < canvas.Children.Count-canvasChildIndex; i++)
            {
                DisposedObjectsPool.Push(canvas.Children[i]);
            }
            
            canvas.Children.RemoveRange(canvasChildIndex, canvas.Children.Count-canvasChildIndex);
        }
    }
}