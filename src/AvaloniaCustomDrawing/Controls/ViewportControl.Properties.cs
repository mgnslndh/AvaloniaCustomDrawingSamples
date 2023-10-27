using System;
using Avalonia;
using Avalonia.Collections;

namespace AvaloniaCustomDrawing.Controls
{
    public partial class ViewportControl
    {
        public static readonly StyledProperty<double> ScaleProperty = AvaloniaProperty.Register<
            ViewportControl,
            double
        >(nameof(Scale), 1.0d);

        public double Scale
        {
            get => GetValue(ScaleProperty);
            set => SetValue(ScaleProperty, value);
        }

        public static readonly StyledProperty<double> RotationProperty = AvaloniaProperty.Register<
            ViewportControl,
            double
        >(nameof(Rotation), coerce: (_, val) => val % (Math.PI * 2));

        /// <summary>
        /// Rotation, measured in Radians!
        /// </summary>
        public double Rotation
        {
            get => GetValue(RotationProperty);
            set => SetValue(RotationProperty, value);
        }

        public static readonly StyledProperty<Point?> ViewportPointerLocationProperty =
            AvaloniaProperty.Register<ViewportControl, Point?>(
                nameof(ViewportPointerLocation)
            );
        public Point? ViewportPointerLocation
        {
            get => GetValue(ViewportPointerLocationProperty);
            set => SetValue(ViewportPointerLocationProperty, value);
        }

        public static readonly StyledProperty<double> ViewportPointerYProperty =
            AvaloniaProperty.Register<ViewportControl, double>(
                nameof(ViewportPointerY),
                0.0d
            );
        public double ViewportPointerY
        {
            get => GetValue(ViewportPointerYProperty);
            set => SetValue(ViewportPointerYProperty, value);
        }

        public static readonly StyledProperty<double> ViewportPointerXProperty =
            AvaloniaProperty.Register<ViewportControl, double>(
                nameof(ViewportPointerX),
                0.0d
            );
        public double ViewportPointerX
        {
            get => GetValue(ViewportPointerXProperty);
            set => SetValue(ViewportPointerXProperty, value);
        }

        public static readonly StyledProperty<double> ViewportCenterYProperty =
            AvaloniaProperty.Register<ViewportControl, double>(
                nameof(ViewportCenterY),
                0.0d
            );
        public double ViewportCenterY
        {
            get => GetValue(ViewportCenterYProperty);
            set => SetValue(ViewportCenterYProperty, value);
        }

        public static readonly StyledProperty<double> ViewportCenterXProperty =
            AvaloniaProperty.Register<ViewportControl, double>(
                nameof(ViewportCenterX),
                0.0d
            );
        public double ViewportCenterX
        {
            get => GetValue(ViewportCenterXProperty);
            set => SetValue(ViewportCenterXProperty, value);
        }

        public static readonly StyledProperty<double> ViewportHeightProperty =
            AvaloniaProperty.Register<ViewportControl, double>(
                nameof(ViewportHeight),
                0.0d
            );
        public double ViewportHeight
        {
            get => GetValue(ViewportHeightProperty);
            set => SetValue(ViewportHeightProperty, value);
        }

        public static readonly StyledProperty<double> ViewportWidthProperty =
            AvaloniaProperty.Register<ViewportControl, double>(
                nameof(ViewportWidth),
                0.0d
            );
        public double ViewportWidth
        {
            get => GetValue(ViewportWidthProperty);
            set => SetValue(ViewportWidthProperty, value);
        }

        public static readonly StyledProperty<double> ViewportLeftProperty =
            AvaloniaProperty.Register<ViewportControl, double>(
                nameof(ViewportLeft),
                0.0d
            );
        public double ViewportLeft
        {
            get => GetValue(ViewportLeftProperty);
            set => SetValue(ViewportLeftProperty, value);
        }

        public static readonly StyledProperty<double> ViewportRightProperty =
            AvaloniaProperty.Register<ViewportControl, double>(
                nameof(ViewportRight),
                0.0d
            );
        public double ViewportRight
        {
            get => GetValue(ViewportRightProperty);
            set => SetValue(ViewportRightProperty, value);
        }

        public static readonly StyledProperty<double> ViewportTopProperty =
            AvaloniaProperty.Register<ViewportControl, double>(
                nameof(ViewportTop),
                0.0d
            );
        public double ViewportTop
        {
            get => GetValue(ViewportTopProperty);
            set => SetValue(ViewportTopProperty, value);
        }

        public static readonly StyledProperty<double> ViewportBottomProperty =
            AvaloniaProperty.Register<ViewportControl, double>(
                nameof(ViewportBottom),
                0.0d
            );
        public double ViewportBottom
        {
            get => GetValue(ViewportBottomProperty);
            set => SetValue(ViewportBottomProperty, value);
        }

        public static readonly DirectProperty<ViewportControl,IViewportDrawable?> DrawableProperty =
            AvaloniaProperty.RegisterDirect<ViewportControl, IViewportDrawable?>(
                nameof(Drawable),
                o => o.Drawable,
                (o, v) => o.Drawable = v
            );

        private IViewportDrawable? _drawable;

        public IViewportDrawable? Drawable
        {
            get { return _drawable; }
            set { SetAndRaise(DrawableProperty, ref _drawable, value); }
        }
    }
}
