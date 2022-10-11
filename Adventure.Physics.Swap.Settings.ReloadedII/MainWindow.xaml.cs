using Adventure.Physics.Swap.Settings.ReloadedII.Models.Model;
using Adventure.Physics.Swap.Settings.ReloadedII.Models.ViewModel;
using Adventure.Physics.Swap.Settings.ReloadedII.Pages;
using Adventure.Physics.Swap.Shared.Configs;
using Adventure.Physics.Swap.Shared.Enums;
using Reloaded.WPF.Theme.Default;

namespace Adventure.Physics.Swap.Settings.ReloadedII;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : ReloadedWindow
{
    public MainWindowViewModel RealViewModel { get; set; }
    public Config Config { get; set; }

    public MainWindow()
    {
        RealViewModel   = new MainWindowViewModel();
        Config          = IoC.Get<Config>();
        IoC.Kernel.Bind<MainWindowViewModel>().ToConstant(RealViewModel);

        InitializeComponent();
    }

    private void OpenPhysicsEditor(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        RealViewModel.Page = ApplicationPage.PhysicsEditor;
    }

    private void OpenSadxPhysicsMapping(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        IoC.Kernel.Rebind(typeof(MappingEditorParameters<,>)).ToConstant(new MappingEditorParameters<SadxCharacter, AllCharacters>(Config.SadxMapping, enums => Config.SadxMapping = enums));
        RealViewModel.ForceSwitchToPage(ApplicationPage.SadxMappingEditor);
    }

    private void OpenSa2PhysicsMapping(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        IoC.Kernel.Rebind(typeof(MappingEditorParameters<,>)).ToConstant(new MappingEditorParameters<Sa2bCharacter, AllCharacters>(Config.Sa2bMapping, enums => Config.Sa2bMapping = enums));
        RealViewModel.ForceSwitchToPage(ApplicationPage.Sa2bMappingEditor);
    }

    private void OpenHeroesPhysicsMapping(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        IoC.Kernel.Rebind(typeof(MappingEditorParameters<,>)).ToConstant(new MappingEditorParameters<HeroesCharacter, AllCharacters>(Config.HeroesMapping, enums => Config.HeroesMapping = enums));
        RealViewModel.ForceSwitchToPage(ApplicationPage.HeroesMappingEditor);
    }
}