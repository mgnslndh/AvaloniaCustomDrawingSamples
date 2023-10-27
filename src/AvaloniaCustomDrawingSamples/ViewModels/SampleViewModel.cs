using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using Avalonia;
using Avalonia.Media;

using AvaloniaCustomDrawing.Controls;

using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaCustomDrawingSamples.ViewModels
{
    public abstract partial class SampleViewModel : ObservableViewModel
    {
        [ObservableProperty]
        private string? _name;

        [ObservableProperty]
        private PropertyCategoryCollection _categories = new();

        [ObservableProperty]
        private IViewportDrawable? _drawable;

        public void UpdateProperty(string name, object? value, string category)
        {
            Categories.AddOrUpdate(category, name, value);
        }
    }

    public partial class PropertyViewModel : ObservableViewModel
    {
        [ObservableProperty]
        private string? _name;

        [ObservableProperty]
        private object? _value;
    }

    public partial class PropertyCategoryViewModel : ObservableViewModel
    {
        [ObservableProperty]
        private string? _name;

        [ObservableProperty]
        private ObservableCollection<PropertyViewModel> _properties = new();

        public void AddOrUpdate(string propertyName, object? value)
        {
            var property = Properties.FirstOrDefault(p => p.Name == propertyName);

            if (property is null)
            {
                property = new PropertyViewModel { Name = propertyName, Value = value };
                Properties.Add(property);
            }

            property.Value = value;
        }
    }

    public class PropertyCategoryCollection : ObservableCollection<PropertyCategoryViewModel>
    {
        public void AddOrUpdate(string categoryName, string propertyName, object? value)
        {
            var category = this.FirstOrDefault(c => c.Name == categoryName);

            if (category is null)
            {
                category = new PropertyCategoryViewModel { Name = categoryName };
                Add(category);
            }

            category.AddOrUpdate(propertyName, value);
        }
    }
}
