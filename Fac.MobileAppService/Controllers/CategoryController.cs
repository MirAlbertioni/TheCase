using System;
using Microsoft.AspNetCore.Mvc;
using Fac.Api;
using Fac.Api.Data;
using Fac.Api.Models;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;
using System.Linq;

namespace Fac.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly AppSettings _settings;
        private FacDbContext _facDbContext;

        public CategoryController(FacDbContext facDbContext, AppSettings settings)
        {
            _facDbContext = facDbContext;
            _settings = settings;
        }

        [HttpGet]
        [Route("GetCategories")]
        public CategoriesSummary GetCategories()
        {
            var all = new CategoriesSummary();
            all.Categorylist = new List<Category>();
            all.SubCategoryList = new List<SubCategory>();
            try
            {
                all.Categorylist = _facDbContext.Category.ToList();
                all.SubCategoryList = _facDbContext.SubCategory.ToList();
                return all;
            }
            catch (Exception ex)
            {

            }
            return all;
        }
    }
}
