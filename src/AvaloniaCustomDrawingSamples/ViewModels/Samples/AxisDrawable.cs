using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Avalonia;
using Avalonia.Media;

using AvaloniaCustomDrawing.Controls;

namespace AvaloniaCustomDrawingSamples.ViewModels.Samples
{
    internal class AxisDrawable : IViewportDrawable
    {
        Pen _pen = new Pen(new SolidColorBrush(Colors.Black, 0.75d), 1);
        public void Draw(DrawingContext context, IViewport viewport)
        {
            Bounds = viewport.Bounds;
            _pen.Thickness = 1 / viewport.Scale;
            context.DrawLine(_pen, new Point(0, Bounds.Top), new Point(0, -Bounds.Bottom));
            context.DrawLine(_pen, new Point(Bounds.Left, 0), new Point(Bounds.Right, 0));
            context.DrawEllipse(null, _pen, new Point(0, 0), 5, 5);
        }

        public Rect Bounds { get; private set; }
    }
}
