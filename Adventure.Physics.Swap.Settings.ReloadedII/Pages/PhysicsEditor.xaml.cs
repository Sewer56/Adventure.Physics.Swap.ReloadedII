using System.IO;
using System.Windows;
using Adventure.Physics.Swap.Settings.ReloadedII.Models.ViewModel;
using Ookii.Dialogs.Wpf;

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

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            var saveDialog = new VistaSaveFileDialog
            {
                Title = "Export to physics data to binary format",
                AddExtension = true,
                Filter = "Binary File (*.bin)|*.bin",
                DefaultExt = ".bin",
            };

            if ((bool) saveDialog.ShowDialog())
            {
                File.WriteAllBytes(saveDialog.FileName, ViewModel.ToBytes());
            }
        }
    }
}
