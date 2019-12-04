using SQLite;
using System;
using Xamarin.Forms;

namespace Fac.Models
{
    public class CaseSummary
    {
        [PrimaryKey, AutoIncrement]
        public Guid CaseId { get; set; }
        public string Message { get; set; }
        public string Image { get; set; }
        [Ignore]
        public ImageSource ImgSrc { get; set; }
        public string SiteRefId { get; set; }
        public string Sender { get; set; }
        public int Status { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string OrderNumber { get; set; }
        public int FreightCarrierId { get; set; }
    }
}