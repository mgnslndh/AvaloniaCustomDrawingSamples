using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace AvaloniaCustomDrawing.Controls
{
    public partial class ViewportControl : Control
    {
        static ViewportControl()
        {
            ClipToBoundsProperty.OverrideDefaultValue<ViewportControl>(true);

            AffectsRender<ViewportControl>(
                ScaleProperty,
                RotationProperty,
                ViewportCenterXProperty,
                ViewportCenterYProperty,
                ViewportHeightProperty,
                ViewportWidthProperty
            );

            AffectsMeasure<ViewportControl>(DrawableProperty);
        }

        protected override void OnSizeChanged(SizeChangedEventArgs e)
        {
            CalculateViewportBounds();
            base.OnSizeChanged(e);
        }

        private void CalculateViewportBounds()
        {
            var bottomLeft = ScreenToWorld(new Point(0, 0), ViewportCenterX, ViewportCenterY, Scale, Rotation);
            var topRight = ScreenToWorld(new Point(Bounds.Width, Bounds.Height), ViewportCenterX, ViewportCenterY, Scale, Rotation);

            ViewportLeft = bottomLeft.X;
            ViewportTop = topRight.Y;
            ViewportRight = topRight.X;
            ViewportBottom = bottomLeft.Y;

            ViewportWidth = topRight.X - bottomLeft.X;

            // TODO: This flippin' of the axis should be done in ScreenToWorld and WorldToScreen?
            ViewportHeight = bottomLeft.Y - topRight.Y; // we have flipped Y axis
            ViewportBounds = new Rect(bottomLeft.X, bottomLeft.Y, ViewportWidth, ViewportHeight);
        }

        public Rect ViewportBounds { get; set; }

        public override void Render(DrawingContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            IDisposable? clipState = null;

            var drawingBounds = new Rect(new Size(this.Bounds.Width, this.Bounds.Height));

            if (ClipToBounds)
            {
                clipState = context.PushClip(drawingBounds);
            }

            context.DrawRectangle(Brushes.Yellow, null, drawingBounds);

            using (var scope = ViewportWorldTransformScope.Create(context, Bounds, Scale, Rotation, ViewportCenterX, ViewportCenterY))
            {

                var viewport = new ViewportContext()
                {
                    Bounds = ViewportBounds,
                    CenterX = ViewportCenterX,
                    CenterY = ViewportCenterY,
                    Width = ViewportWidth,
                    Height = ViewportHeight,
                    Scale = Scale,
                    Rotation = Rotation
                };

                Drawable?.Draw(context, viewport);
            }

            clipState?.Dispose();
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);
            if (change.Property == ViewportCenterXProperty || change.Property == ViewportCenterYProperty || change.Property == ScaleProperty ||
                change.Property == RotationProperty)
            {
                CalculateViewportBounds();
            }
        }


        private Point ScreenToWorld(Point inPoint, double viewportCenterX, double viewportCenterY, double scale, double rotation)
        {
            Point workingPoint = new Point(inPoint.X, -inPoint.Y);
            workingPoint += new Vector(-this.Bounds.Width / 2.0d, this.Bounds.Height / 2.0d);
            workingPoint /= scale;

            workingPoint = Matrix.CreateRotation(rotation).Transform(workingPoint);

            workingPoint += new Vector(viewportCenterX, viewportCenterY);

            return workingPoint;
        }

        private Point WorldToScreen(Point inPoint, double viewportCenterX, double viewportCenterY, double scale, double rotation)
        {
            Point workingPoint = new Point(inPoint.X, inPoint.Y);

            workingPoint -= new Vector(viewportCenterX, viewportCenterY);
            // undo rotation
            workingPoint = Matrix.CreateRotation(-rotation).Transform(workingPoint);
            workingPoint *= scale;
            workingPoint -= new Vector(-this.Bounds.Width / 2.0d, this.Bounds.Height / 2.0d);
            workingPoint = new Point(workingPoint.X, -workingPoint.Y);

            return workingPoint;
        }

        public void PanTo(Rect rect)
        {
            var centerX = rect.X + rect.Width / 2.0d;
            var centerY = rect.Y + rect.Height / 2.0d;

            ViewportCenterX = centerX;
            ViewportCenterY = centerY;
        }

        public void PanTo(Point point)
        {
            ViewportCenterX = point.X;
            ViewportCenterY = point.Y;
        }

        public void PanTo(double x, double y)
        {
            ViewportCenterX = x;
            ViewportCenterY = y;
        }

        public void PanToOrigin()
        {
            ViewportCenterX = 0.0d;
            ViewportCenterY = 0.0d;
        }
    }
}
