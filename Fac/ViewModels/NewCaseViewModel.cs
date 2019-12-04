using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Fac.Models;
using Microsoft.AppCenter.Crashes;

namespace Fac.ViewModels
{
    public class NewCaseViewModel : BaseViewModel
    {
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<SubCategory> SubCategories { get; set; }
        public ObservableCollection<SubCategory> SubCatPerCat { get; set; }
        public Case Case { get; set; }
        public Command LoadCategoriesCommand { get; set; }

        public NewCaseViewModel()
        {
            LoadCategoriesCommand = new Command(() => LoadCategories());
            Categories = new ObservableCollection<Category>();
            SubCategories = new ObservableCollection<SubCategory>();
            SubCatPerCat = new ObservableCollection<SubCategory>();
            Case = new Case();
        }

        public async void GetCategories()
        {
            //var items = await DataStore.GetCategories();

            //foreach(var item in items.Categorylist)
            //{
            //    Categories.Add(item);
            //}

            //foreach (var item in items.SubCategoryList)
            //{
            //    SubCategories.Add(item);
            //}
            Categories.Add(new Category { CategoryId = 1, CategoryName = "Supermarket Ica" });
            Categories.Add(new Category { CategoryId = 2, CategoryName = "Accord Office" });
            SubCategories.Add(new SubCategory { SubCategoryId = 1, CategoryId = 1, SubCategoryName = "Fruits" });
            SubCategories.Add(new SubCategory { SubCategoryId = 2, CategoryId = 1, SubCategoryName = "Checkout" });
            SubCategories.Add(new SubCategory { SubCategoryId = 3, CategoryId = 2, SubCategoryName = "Cleaning team" });
            SubCategories.Add(new SubCategory { SubCategoryId = 4, CategoryId = 2, SubCategoryName = "Sales dep." });
        }

        public async Task<bool> AddCase(Case request)
        {
            var result = await DataStore.AddCaseAsync(request);
            return result;
        }

        void LoadCategories()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Categories.Clear();
                SubCategories.Clear();
                GetCategories();
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