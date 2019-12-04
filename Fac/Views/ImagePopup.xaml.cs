using Fac.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fac.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImagePopup : PopupPage
    {
        //public CaseSummary _caseSummary { get; set; }
        //public Case _case { get; set; }
        public ImageSource _imgSrc { get; set; }

        public ImagePopup()
        {
            InitializeComponent();
        }

        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();

            //CaseImage.Source = _caseSummary != null && _caseSummary.ImgSrc != null ? _caseSummary.ImgSrc
            //    : _case != null && _case.ImgSrc != null ? _case.ImgSrc
            //    : _imgSrc != null ? _imgSrc
            //    : CaseImage.Source;

            CaseImage.Source = _imgSrc != null ? _imgSrc : CaseImage.Source;

            FrameContainer.HeightRequest = -1;

            if (!IsAnimationEnabled)
            {
                CloseImage.Rotation = 0;
                CloseImage.Scale = 1;
                CloseImage.Opacity = 1;

                return;
            }

            CloseImage.Rotation = 30;
            CloseImage.Scale = 0.3;
            CloseImage.Opacity = 0;
        }

        protected override async Task OnAppearingAnimationEndAsync()
        {
            //CaseImage.Source = _caseSummary != null && _caseSummary.ImgSrc != null ? _caseSummary.ImgSrc
            //    : _case != null && _case.ImgSrc != null ? _case.ImgSrc
            //    : CaseImage.Source;

            CaseImage.Source = _imgSrc != null ? _imgSrc : CaseImage.Source;

            if (!IsAnimationEnabled)
                return;

            await Task.WhenAll(
                CloseImage.FadeTo(1),
                CloseImage.ScaleTo(1, easing: Easing.CubicIn),
                CloseImage.RotateTo(0));
        }

        protected override async Task OnDisappearingAnimationBeginAsync()
        {
            if (!IsAnimationEnabled)
                return;

            var taskSource = new TaskCompletionSource<bool>();

            var currentHeight = FrameContainer.Height;

            FrameContainer.Animate("HideAnimation", d =>
            {
                FrameContainer.HeightRequest = d;
            },
            start: currentHeight,
            end: 170,
            finished: async (d, b) =>
            {
                await Task.Delay(300);
                taskSource.TrySetResult(true);
            });

            await taskSource.Task;
        }

        private void OnCloseButtonTapped(object sender, EventArgs e)
        {
            CloseAllPopup();
        }

        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();

            return false;
        }

        private async void CloseAllPopup()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }
    }
}