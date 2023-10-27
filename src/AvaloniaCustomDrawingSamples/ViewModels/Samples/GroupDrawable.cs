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
    internal class GroupDrawable : IViewportDrawable
    {
        private IViewportDrawable[] _drawables;
        public GroupDrawable(IEnumerable<IViewportDrawable> drawables)
        {
            _drawables = drawables.ToArray();
            Bounds = _drawables.Select(d => d.Bounds).Aggregate((a,b) => a.Union(b));
        }

        public void Draw(DrawingContext context, IViewport viewport)
        {
            foreach (var drawable in _drawables)
            {
                drawable.Draw(context, viewport);
            }
        }

        public Rect Bounds { get; private set; }
    }
   
}
