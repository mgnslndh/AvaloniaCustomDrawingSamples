using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AvaloniaCustomDrawingSamples.ViewModels.Samples;

using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaCustomDrawingSamples.ViewModels
{
    public partial class SampleExplorerViewModel : ObservableViewModel
    {
        [ObservableProperty]
        private SampleViewModel? _selectedSample;

        [ObservableProperty]
        private ObservableCollection<SampleViewModel> _samples = new();

        public SampleExplorerViewModel()
        {
            Samples.Add(new BasicSampleViewModel());
            SelectedSample = Samples.First();
        }
    }
}
