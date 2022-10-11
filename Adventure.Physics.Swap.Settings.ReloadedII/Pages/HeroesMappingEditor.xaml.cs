using System.Windows;
using Adventure.Physics.Swap.Settings.ReloadedII.Models.Model;
using Adventure.Physics.Swap.Settings.ReloadedII.Models.ViewModel;
using Adventure.Physics.Swap.Shared.Configs;
using Adventure.Physics.Swap.Shared.Enums;

namespace Adventure.Physics.Swap.Settings.ReloadedII.Pages;

/// <summary>
/// Interaction logic for MappingEditor.xaml
/// </summary>
public partial class HeroesMappingEditor : ReloadedIIPage
{
    public MappingEditorViewModel<HeroesCharacter, AllCharacters>  ViewModel  { get; set; }

    public HeroesMappingEditor(Config config, MappingEditorParameters<HeroesCharacter, AllCharacters> parameters)
    {
        InitializeComponent();
        ViewModel   = new MappingEditorViewModel<HeroesCharacter, AllCharacters>(parameters, config);
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        ViewModel.Save();
    }
}