using Adventure.Physics.Swap.Settings.ReloadedII.Models.ViewModel;

namespace Adventure.Physics.Swap.Settings.ReloadedII.Pages
{
    /// <summary>
    /// Interaction logic for PhysicsEditor.xaml
    /// </summary>
    public partial class PhysicsEditor : ReloadedIIPage
    {
        public PhysicsEditorViewModel ViewModel { get; set; }

        public PhysicsEditor(PhysicsEditorViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
        }

        private void Save_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel.Save();
        }
    }
}
