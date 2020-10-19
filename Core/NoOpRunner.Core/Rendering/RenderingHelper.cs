using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace NoOpRunner.Core.Rendering
{
    public static class RenderingHelper
    {
        public static void AnimateUiElementMove(UIElement uiElement, double newPosX, double newPosY)
        {
            var lastPositionY = (double) uiElement.GetValue(Canvas.BottomProperty);
            var lastPositionX = (double) uiElement.GetValue(Canvas.LeftProperty);

            var animationDuration = GameSettings.MoveAnimationDuration;

            var moveAnimX =
                new DoubleAnimation(double.IsNaN(lastPositionX) ? newPosX : lastPositionX, newPosX, animationDuration);
            var moveAnimY =
                new DoubleAnimation(double.IsNaN(lastPositionY) ? newPosY : lastPositionY, newPosY, animationDuration);

            uiElement.BeginAnimation(Canvas.LeftProperty, moveAnimX);
            uiElement.BeginAnimation(Canvas.BottomProperty, moveAnimY);
        }
    }
}