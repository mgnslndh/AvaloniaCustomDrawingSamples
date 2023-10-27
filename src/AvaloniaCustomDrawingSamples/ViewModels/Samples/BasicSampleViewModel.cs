using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AvaloniaCustomDrawing.Controls;

using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaCustomDrawingSamples.ViewModels.Samples
{
    public partial class BasicSampleViewModel : SampleViewModel
    {
        public BasicSampleViewModel()
        {
            Name = "Basic";
            Drawable = new GroupDrawable(new IViewportDrawable[]  {new RedBoxDrawable(), new AxisDrawable() });
        }
    }
}
