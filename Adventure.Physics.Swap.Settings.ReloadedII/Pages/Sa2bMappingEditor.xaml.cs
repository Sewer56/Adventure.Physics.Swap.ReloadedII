using System.Windows;
using Adventure.Physics.Swap.Settings.ReloadedII.Models.Model;
using Adventure.Physics.Swap.Settings.ReloadedII.Models.ViewModel;
using Adventure.Physics.Swap.Shared.Configs;
using Adventure.Physics.Swap.Shared.Enums;

namespace Adventure.Physics.Swap.Settings.ReloadedII.Pages
{
    /// <summary>
    /// Interaction logic for MappingEditor.xaml
    /// </summary>
    public partial class Sa2bMappingEditor : ReloadedIIPage
    {
        public MappingEditorViewModel<Sa2bCharacter, AllCharacters>  ViewModel  { get; set; }

        public Sa2bMappingEditor(Config config, MappingEditorParameters<Sa2bCharacter, AllCharacters> parameters)
        {
            InitializeComponent();
            ViewModel   = new MappingEditorViewModel<Sa2bCharacter, AllCharacters>(parameters, config);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Save();
        }
    }
}
