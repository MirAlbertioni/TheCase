using System;
using System.Collections.Generic;
using System.Text;

namespace Fac.Api.Models
{
    public class CategoriesSummary
    {
        public List<Category> Categorylist { get; set; }
        public List<SubCategory> SubCategoryList { get; set; }
    }
}
