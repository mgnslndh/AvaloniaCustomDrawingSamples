using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Avalonia;
using Avalonia.Media;

namespace AvaloniaCustomDrawing.Controls
{
    public record class ViewportContext : IViewport
    {

        public Rect Bounds { get; init; }
        public double CenterX { get; init; }
        public double CenterY { get; init; }
        public double Width { get; init; }
        public double Height { get; init; }
        public double Scale { get; init; }
        public double Rotation { get; set; }
    }

    public interface IViewport
    {
        public Rect Bounds { get; }
        public double CenterX { get; }
        public double CenterY { get; }
        public double Width { get; }
        public double Height { get; }
        public double Scale { get; }
        public double Rotation { get; set; }
    }

    public interface IViewportDrawable
    {
        void Draw(DrawingContext context, IViewport viewport);
        Rect Bounds { get; }
    }
}
