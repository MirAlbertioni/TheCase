using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Fac.Models;
using Fac.ViewModels;
using System.IO;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;

namespace Fac.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CaseDetailPage : ContentPage
    {
        CaseDetailViewModel viewModel;

        public CaseDetailPage(CaseDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public CaseDetailPage(CaseSummary item)
        {
            InitializeComponent();

            byte[] Base64Stream = Convert.FromBase64String(item.Image);
            CaseImage.Source = ImageSource.FromStream(() => new MemoryStream(Base64Stream));

            NameOfStatus.Text = StatusName(item.Status);
            viewModel = new CaseDetailViewModel(item);
            BindingContext = viewModel;
        }

        public string StatusName(int status)
        {
            switch (status)
            {
                case 0:
                    return "Unread";
                case 1:
                    return "Under investigation";
                case 2:
                    return "Need more information";
                case 3:
                    return "Closed";
                case 99:
                    return "Deleted";
                default:
                    return "";
            }
        }

        async Task OnImageNameTapped(object sender, EventArgs args)
        {
            try
            {
                var page = new ImagePopup
                {
                    _imgSrc = viewModel.Case.ImgSrc
                };
                await PopupNavigation.Instance.PushAsync(page);
            }
            catch (Exception ex)
            {
                
            }
        }

        private async void OnClose(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}