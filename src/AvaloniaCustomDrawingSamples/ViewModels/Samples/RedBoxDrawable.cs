using Avalonia;
using Avalonia.Media;

using AvaloniaCustomDrawing.Controls;

namespace AvaloniaCustomDrawingSamples.ViewModels.Samples
{
    internal class RedBoxDrawable : IViewportDrawable
    {
        private readonly IPen _borderPen = new Pen(Brushes.DarkRed, 10);
        private readonly IBrush _backgroundBrush = Brushes.Red;
        private readonly Geometry _geometry = new RectangleGeometry(new Rect(0, 0, 100, 100));

        internal RedBoxDrawable()
        {
            Bounds = _geometry.GetRenderBounds(_borderPen);
        }

        public void Draw(DrawingContext context, IViewport viewport)
        {
            context.DrawGeometry(
                _backgroundBrush,
                _borderPen,
                _geometry
            );
        }

        public Rect Bounds { get; }
    }

}
