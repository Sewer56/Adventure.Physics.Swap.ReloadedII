using Adventure.Physics.Swap.Settings.ReloadedII.Pages;
using Reloaded.WPF.MVVM;

namespace Adventure.Physics.Swap.Settings.ReloadedII.Models.ViewModel;

public class MainWindowViewModel : ObservableObject
{
    public ApplicationPage Page
    {
        get => _page;
        set => _page = value;
    }

    public string WindowTitle   { get; set; } = "Heh, this is not Reloaded II";
    private ApplicationPage _page;

    public void ForceSwitchToPage(ApplicationPage page)
    {
        _page = page;
        RaisePropertyChangedEvent(nameof(Page));
    }
}