using System.Windows;
using Adventure.Physics.Swap.Settings.ReloadedII.Models.ViewModel;
using Adventure.Physics.Swap.Shared.Configs;

namespace Adventure.Physics.Swap.Settings.ReloadedII;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        var config = Config.FromJson(Environment.CurrentDirectory);
        IoC.Kernel.Bind<Config>().ToConstant(config);
        IoC.Kernel.Bind<PhysicsEditorViewModel>().ToConstant(new PhysicsEditorViewModel(config));
    }
}