using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Fac.Models;
using Fac.ViewModels;
using Plugin.Media;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.Media.Abstractions;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace Fac.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewCasePage : ContentPage
    {
        private Case Case;
        private static List<Category> Categories;
        private static List<SubCategory> SubCategories;
        NewCaseViewModel viewModel;

        public NewCasePage()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
            }
            BindingContext = viewModel = new NewCaseViewModel();

            Case = new Case();
            Categories = new List<Category>();
            SubCategories = new List<SubCategory>();
        }

        public void CategoryIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            viewModel.SubCatPerCat.Clear();

            var result = viewModel.SubCategories.Where(x => x.CategoryId == viewModel.Categories[selectedIndex].CategoryId).ToList();
            foreach (var item in result)
            {
                viewModel.SubCatPerCat.Add(item);
            }

            var selectedItem = (Category)picker.SelectedItem;
            Case.CategoryId = selectedItem.CategoryId;
        }

        public void SubCategoryIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            var selectedItem = (SubCategory)picker.SelectedItem;
            Case.SubCategoryId = selectedItem.SubCategoryId;
        }

        async Task OnImageResultTapped(object sender, EventArgs args)
        {
            try
            {
                if(string.IsNullOrEmpty(Case.Image)) return;

                byte[] Base64Stream = Convert.FromBase64String(Case.Image);
                Case.ImgSrc = ImageSource.FromStream(() => new MemoryStream(Base64Stream));

                var page = new ImagePopup
                {
                    _imgSrc = Case.ImgSrc
                };
                await PopupNavigation.Instance.PushAsync(page);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        private async void PickPhoto(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photos Not Supported", "Permission not granted to photos.", "OK");
                return;
            }

            await DisplayAlert("Max width 320px", "", "OK");

            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.MaxWidthHeight,
            });

            if (file == null)
                return;

            using (var memoryStream = new MemoryStream())
            {
                file.GetStream().CopyTo(memoryStream);
                byte[] imageBytes = memoryStream.ToArray();

                Case.Image = Convert.ToBase64String(imageBytes);
            }

            ImageResult.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }

        private async void TakePhoto(object sender, EventArgs e)
        {
            var imageSender = (Image)sender;

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Cases",
                SaveToAlbum = true,
                CompressionQuality = 100,
                CustomPhotoSize = 100,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = CameraDevice.Front
            });

            if (file == null)
                return;

            using (var memoryStream = new MemoryStream())
            {
                file.GetStream().CopyTo(memoryStream);
                byte[] imageBytes = memoryStream.ToArray();

                Case.Image = Convert.ToBase64String(imageBytes);
            }

            ImageResult.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }

        async Task Save_Clicked(object sender, EventArgs e)
        {
            try
            {
                Case.CaseId = Guid.NewGuid();
                Case.SiteRefId = "18745";
                Case.Sender = "Mir";
                Case.Status = 0;
                //Case.OrderNumber = OrderNumber.Text;
                Case.Message = Message.Text;

                //if (!int.TryParse(FreightCarrier.Text, out int carrierId))
                //{
                //    await DisplayAlert("Warning", "FreightcarrierId can only contain numbers", "Ok");
                //}
                //else
                //{
                //    Case.FreightCarrierId = carrierId;
                //}

                if (string.IsNullOrEmpty(Case.SiteRefId) || string.IsNullOrEmpty(Case.Sender) ||
                    string.IsNullOrEmpty(Case.Message) || string.IsNullOrEmpty(Case.Image) || 
                    Case.CategoryId == 0 || Case.SubCategoryId == 0)
                {
                    await DisplayAlert("Warning", "All the fields are mandatory", "Ok");
                    return;
                }

                //var result = await viewModel.AddCase(Case);

                //if (!result)
                //{
                //    await DisplayAlert("Error!", "Something went wrong with the post", "Ok");
                //    return;
                //}
                //else
                //{
                //    //MessagingCenter.Send(this, "AddCase", Case);
                //    await Navigation.PopModalAsync();
                //}
                Analytics.TrackEvent("User saved case");
                await Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            await DisplayAlert("Success", "Post was successful", "Ok");
            return;

        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Categories.Count == 0)
                viewModel.LoadCategoriesCommand.Execute(null);
        }

        //private async void FreightCarrierChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    var fcarrierId = (Entry)sender;
        //    if (!int.TryParse(fcarrierId.Text, out int carrierId))
        //    {
        //        await DisplayAlert("Warning", "FreightcarrierId can only contain numbers", "Cancel");
        //    }
        //    else
        //    {
        //        Case.FreightCarrierId = carrierId;
        //    }
        //}

        //private void OrderNumberChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    var orderNumber = (Entry)sender;
        //    Case.OrderNumber = orderNumber.Text;
        //}

        //private void MessageChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    var message = (Entry)sender;
        //    Case.Message = message.Text;
        //}
    }
}