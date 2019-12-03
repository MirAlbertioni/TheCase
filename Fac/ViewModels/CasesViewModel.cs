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
                Message = "Hej",
                CategoryName = "Harry Potter",
                SubCategoryName = "Ron",
                OrderNumber = "ABC-001001",
                Sender = "Mir Albertioni",
                Status = 1,
                FreightCarrierId = 1,
                SiteRefId = "1010",
                ImgSrc = "~/Resources/drawable/github_octocat.png"

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
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}