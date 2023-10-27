using System.Security.Cryptography;

using Avalonia;
using Avalonia.Input;

namespace AvaloniaCustomDrawing.Controls
{
    public partial class ViewportControl
    {
        // TODO: rename to _pointerPoint?
        private Point _cursorPoint;
        private bool _isPointerCaptured;

        protected override void OnPointerExited(PointerEventArgs e)
        {
            ViewportPointerLocation = null;
            base.OnPointerExited(e);
        }

        protected override void OnPointerMoved(PointerEventArgs e)
        {
            base.OnPointerMoved(e);

            Point previousPoint = _cursorPoint;

            _cursorPoint = e.GetPosition(this);

            if (_isPointerCaptured)
            {
                Point oldWorldPoint = ScreenToWorld(previousPoint, ViewportCenterX, ViewportCenterY, Scale, Rotation);
                Point newWorldPoint = ScreenToWorld(_cursorPoint, ViewportCenterX, ViewportCenterY, Scale, Rotation);

                Vector diff = newWorldPoint - oldWorldPoint;

                ViewportCenterX -= diff.X;
                ViewportCenterY -= diff.Y;
            }

            // Update pointer location after we have calculated the new viewport center
            ViewportPointerLocation = ScreenToWorld(_cursorPoint, ViewportCenterX, ViewportCenterY, Scale, Rotation);
        }

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            e.Handled = true;
            e.Pointer.Capture(this);
            _isPointerCaptured = true;
            base.OnPointerPressed(e);
        }

        protected override void OnPointerWheelChanged(PointerWheelEventArgs e)
        {
            base.OnPointerWheelChanged(e);
            var oldScale = Scale;
            Scale *= (1.0d + e.Delta.Y / 12.0d);

            Point oldWorldPoint = ScreenToWorld(_cursorPoint, ViewportCenterX, ViewportCenterY, oldScale, Rotation);
            Point newWorldPoint = ScreenToWorld(_cursorPoint, ViewportCenterX, ViewportCenterY, Scale, Rotation);

            Vector diff = newWorldPoint - oldWorldPoint;

            ViewportCenterX -= diff.X;
            ViewportCenterY -= diff.Y;

            // TODO: Replace with AffectsRender
            InvalidateVisual();
        }

        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            e.Pointer.Capture(null);
            _isPointerCaptured = false;
            base.OnPointerReleased(e);
        }
    }
}
