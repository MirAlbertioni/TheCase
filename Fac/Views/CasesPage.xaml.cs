using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Fac.Models;
using Fac.ViewModels;
using Rg.Plugins.Popup.Services;
using System.Linq;

namespace Fac.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CasesPage : ContentPage
    {
        CasesViewModel viewModel;

        public CasesPage()
        {
            InitializeComponent();
            Title = "Inbox";
            BindingContext = viewModel = new CasesViewModel();
        }

        protected async void OnCaseSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as CaseSummary;
            if (item == null)
                return;

            await Navigation.PushAsync(new CaseDetailPage(item));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        protected async void OnImageResultTapped(object sender, EventArgs args)
        {
            try
            {
                Image imageSender = (Image)sender;
                if (imageSender == null) return;

                var page = new ImagePopup
                {
                    _imgSrc = imageSender.Source
                };
                await PopupNavigation.Instance.PushAsync(page);
            }
            catch (Exception ex)
            {

            }
        }

        protected async void AddCase_Clicked(object sender, EventArgs e)
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