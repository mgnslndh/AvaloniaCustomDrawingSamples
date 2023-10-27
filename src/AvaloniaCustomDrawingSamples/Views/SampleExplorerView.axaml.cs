using System.Collections.Generic;

using Avalonia;
using Avalonia.Controls;

using AvaloniaCustomDrawing.Controls;

using AvaloniaCustomDrawingSamples.ViewModels;

namespace AvaloniaCustomDrawingSamples.Views
{
    public record class PropertyTransform
    {
        public string Name { get; init; } = string.Empty;
        public string? Format { get; init; }
        public string? DisplayName { get; init; }
        public string? Category { get; init; }

        public PropertyTransform(string name, string? format = null, string? displayName = null, string? category = null)
        {
            Name = name;
            Format = format;
            DisplayName = displayName;
            Category = category;
        }

        public virtual string Transform(object? value)
        {
            if (value is null)
            {
                return string.Empty;
            }

            if (Format is null)
            {
                return value.ToString() ?? string.Empty;
            }

            return string.Format(Format, value);
        }
    }

    public abstract record class PropertyTransform<T> : PropertyTransform
    {
        protected PropertyTransform(string name, string? format = null, string? displayName = null,
            string? category = null) : base(name, format, displayName, category)
        {
        }

    }

    public record class PointPropertyTransform : PropertyTransform
    {
        public PointPropertyTransform(string name, string? format = null, string? displayName = null,
            string? category = null) : base(name, format, displayName, category)
        {
        }

        public override string Transform(object? value)
        {
            if (value is Point point)
            {
                return $"{base.Transform(point.X)};{base.Transform(point.Y)}";
            }
            return base.Transform(value);
        }
    }

    public class PropertyTransformDictionary : Dictionary<string,PropertyTransform>
    {
        public void Add(string name, string? format = null,  string? displayName = null, string? category = null)
        {
            Add(name,new PropertyTransform(name, format, displayName, category));
        }
    }

    public partial class SampleExplorerView : UserControl
    {
        public SampleExplorerView()
        {
            InitializeComponent();
            Viewport.PropertyChanged += OnViewportPropertyChanged;

            _propertyFilter = new()
            {
                { nameof(ViewportControl.ViewportWidth), "{0:N2}", "Width" },
                { nameof(ViewportControl.ViewportHeight), "{0:N2}", "Height" },
                { nameof(ViewportControl.ViewportCenterX), "{0:N2}", "CenterX" },
                { nameof(ViewportControl.ViewportCenterY), "{0:N2}", "CenterY" },
                { nameof(ViewportControl.ViewportTop), "{0:N2}", "Top" },
                { nameof(ViewportControl.ViewportBottom), "{0:N2}", "Bottom" },
                { nameof(ViewportControl.ViewportRight), "{0:N2}", "Right" },
                { nameof(ViewportControl.ViewportLeft), "{0:N2}", "Left" },
                { nameof(ViewportControl.Scale), "{0:P2}", "Zoom" },
                { nameof(ViewportControl.Rotation), "{0:N2}" },
            };

            _propertyFilter.Add(nameof(ViewportControl.ViewportPointerLocation), new PointPropertyTransform(nameof(ViewportControl.ViewportPointerLocation), "{0:N2}", "Pointer", "Viewport"));
        }

        private readonly PropertyTransformDictionary _propertyFilter;

        private void OnViewportPropertyChanged(object? sender, Avalonia.AvaloniaPropertyChangedEventArgs e)
        {
            if (DataContext is SampleExplorerViewModel {SelectedSample: not null} viewModel)
            {
                if (_propertyFilter.TryGetValue(e.Property.Name, out PropertyTransform? filter))
                {
                    viewModel.SelectedSample.UpdateProperty(filter.DisplayName ?? filter.Name, filter.Transform(e.NewValue), filter.Category ?? "Viewport");
                }
            }
        }
    }
}
