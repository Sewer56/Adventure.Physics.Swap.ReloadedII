using System;
using Adventure.Physics.Swap.Settings.ReloadedII.Models.ViewModel;
using Reloaded.WPF.Theme.Default;

namespace Adventure.Physics.Swap.Settings.ReloadedII
{
    /// <summary>
    /// Special type of <see cref="ReloadedWindow"/> that adds localization support
    /// to the existing windows as well as auto-changes Window titles.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class ReloadedIIPage : ReloadedPage
    {
        public ReloadedIIPage()
        {
            this.AnimateInFinished += OnAnimateInFinished;
        }

        protected virtual void OnAnimateInFinished()
        {
            // Change window title to current page title.
            if (!String.IsNullOrEmpty(this.Title))
            {
                var viewModel = IoC.Get<MainWindowViewModel>();
                viewModel.WindowTitle = this.Title;
            }
        }
    }
}
