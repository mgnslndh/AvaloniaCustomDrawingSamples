using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaCustomDrawingSamples.ViewModels
{
    public partial class MainViewModel : ObservableViewModel
    {
        public string Greeting => "Welcome to Avalonia!";
        public SampleExplorerViewModel SampleExplorer { get; } = new();
    }
}
