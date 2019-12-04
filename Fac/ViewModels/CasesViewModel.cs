using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Fac.Models;
using Fac.Views;
using Refit;
using Fac.Interface;
using System.Collections.Generic;
using Fac.Entities;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.IO;
using System.Linq;
using Fac.Services;
using Microsoft.AppCenter.Crashes;

namespace Fac.ViewModels
{
    public class CasesViewModel : BaseViewModel
    {
        public ObservableCollection<CaseSummary> Cases { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        public Command LoadCasesCommand { get; set; }
        public CaseDb caseDb;

        public CasesViewModel()
        {
            Cases = new ObservableCollection<CaseSummary>();
            LoadCasesCommand = new Command(() => LoadCases());
            caseDb = new CaseDb();
        }

        public async void GetCases()
        {
            //var items = await DataStore.GetCasesAsync();
            //foreach(var item in items)
            //{
            //    byte[] Base64Stream = Convert.FromBase64String(item.Image);
            //    item.ImgSrc  = ImageSource.FromStream(() => new MemoryStream(Base64Stream));
            //    if (item.Status == 0)
            //    {

            //    }
            //    Cases.Add(item);
            //    //caseDb.AddCaseTodb(item);
            //}
            Cases.Add(new CaseSummary
            {
                CaseId = Guid.NewGuid(),
                Message = "Shelves are empty, please fill them up.",
                CategoryName = "Supermarket Ica",
                SubCategoryName = "Fruits",
                Sender = "Mir Albertioni",
                Status = 1,
                SiteRefId = "1010",
                ImgSrc = "~/Resources/drawable/emptyshelves.jpg"

            });

            Cases.Add(new CaseSummary
            {
                CaseId = Guid.NewGuid(),
                Message = "Broken glass on the ground",
                CategoryName = "Accord Office",
                SubCategoryName = "Cleaning team",
                Sender = "Lorem Ipsum",
                Status = 1,
                SiteRefId = "1010",
                ImgSrc = "~/Resources/drawable/brokenglass.jpg"

            });

            Cases.Add(new CaseSummary
            {
                CaseId = Guid.NewGuid(),
                Message = "Fix the lights above the fruits and veggies.",
                CategoryName = "Supermarket Ica",
                SubCategoryName = "Fruits",
                Sender = "John Doe",
                Status = 1,
                SiteRefId = "1010",
                ImgSrc = "~/Resources/drawable/lightsout.jpg"

            });
        }

        public async Task<bool> AddCase(Case request)
        {
            var result = await DataStore.AddCaseAsync(request);
            return result;
        }

        void LoadCases()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Cases.Clear();
                GetCases();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}