using System;
using Avalonia;
using Avalonia.Media;

namespace AvaloniaCustomDrawing.Controls
{
    internal readonly record struct ViewportWorldTransformScope : IDisposable
    {
        private readonly IDisposable _mapPositionModifier;
        private readonly IDisposable _scaleModifier;
        private readonly IDisposable _rotationModifier;
        private readonly IDisposable _translateModifier;

        public ViewportWorldTransformScope(
            IDisposable mapPositionModifier,
            IDisposable scaleModifier,
            IDisposable rotationModifier,
            IDisposable translateModifier
        )
        {
            _mapPositionModifier = mapPositionModifier;
            _scaleModifier = scaleModifier;
            _rotationModifier = rotationModifier;
            _translateModifier = translateModifier;
        }

        public void Dispose()
        {
            _mapPositionModifier.Dispose();
            _scaleModifier.Dispose();
            _rotationModifier.Dispose();
            _translateModifier.Dispose();
        }

        public static IDisposable Create(DrawingContext context, Rect bounds, double scale, double rotation, double viewportCenterX, double viewportCenterY)
        {
            var halfMax = Math.Max(bounds.Width / 2.0d, bounds.Height / 2.0d) * Math.Sqrt(2.0d);
            var halfMin = Math.Min(bounds.Width / 2.0d, bounds.Height / 2.0d) / 1.3d;
            var halfWidth = bounds.Width / 2.0d;
            var halfHeight = bounds.Height / 2.0d;

            // 0,0 refers to the top-left of the control now. It is not prime time to draw gui stuff because it'll be under the world

            var translateModifier = context.PushTransform(
                Matrix.CreateTranslation(new Vector(halfWidth, halfHeight))
            );

            // now 0,0 refers to the ViewportCenter(X,Y).
            var rotationMatrix = Matrix.CreateRotation(rotation);
            var rotationModifier = context.PushTransform(rotationMatrix);

            // everything is rotated but not scaled

            var scaleModifier = context.PushTransform(Matrix.CreateScale(scale, -scale));

            var mapPositionModifier = context.PushTransform(
                Matrix.CreateTranslation(new Vector(-viewportCenterX, -viewportCenterY))
            );

            return new ViewportWorldTransformScope(mapPositionModifier, scaleModifier, rotationModifier, translateModifier);
        }
    }

}
