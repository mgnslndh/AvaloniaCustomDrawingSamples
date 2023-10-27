using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Avalonia;

namespace AvaloniaCustomDrawing.Controls
{
    public partial class ViewportControl
    {
        public void ZoomToScene()
        {
            ZoomTo(Drawable?.Bounds ?? default);
        }

        public void ZoomTo(Rect area)
        {
            var panelWidth = Bounds.Width;
            var panelHeight = Bounds.Height;
            var elementWidth = area.Width;
            var elementHeight = area.Height;

            var zx = panelWidth / elementWidth;
            var zy = panelHeight / elementHeight;
            var cx = elementWidth / 2.0;
            var cy = elementHeight / 2.0;

            var zoom = Math.Min(zx, zy);

            Scale = zoom;
            Rotation = 0;

            var zoomArea = area * zoom;
            var center = zoomArea.Center;

            var left = area.X + area.Width / 2.0;
            var top = area.Y + area.Height / 2.0;

            ViewportCenterX = left;
            ViewportCenterY = top;
        }
    }
}
