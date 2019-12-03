using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Fac.Models;
using Fac.ViewModels;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;

namespace Fac.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UnreadCasesPage : ContentPage
    {
        UnreadCasesViewModel viewModel;

        public UnreadCasesPage()
        {
            InitializeComponent();
            Title = "Unread";
            BindingContext = viewModel = new UnreadCasesViewModel();
        }

        async void OnCaseSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is CaseSummary item))
                return;

            await Navigation.PushAsync(new CaseDetailPage(item));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async Task OnImageResultTapped(object sender, SelectedItemChangedEventArgs args)
        {
            try
            {
                if (!(args.SelectedItem is CaseSummary item))
                    return;

                if (item.ImgSrc == null) return;

                var page = new ImagePopup
                {
                    _imgSrc = item.ImgSrc
                };
                await PopupNavigation.Instance.PushAsync(page);
            }
            catch (Exception ex)
            {

            }
        }

        async void AddCase_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewCasePage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Cases.Count == 0)
                viewModel.LoadCasesCommand.Execute(null);
        }
    }
}