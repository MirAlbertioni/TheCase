using SQLite;
using System;
using Xamarin.Forms;

namespace Fac.Models
{
    public class Case
    {
        public Guid CaseId { get; set; }
        public string Message { get; set; }
        public string Image { get; set; }
        public ImageSource ImgSrc { get; set; }
        public string SiteRefId { get; set; }
        public string Sender { get; set; }
        // 0 = Unread
        // 1 = Read
        // 2 = Re-Reporting
        // 3 = Closed
        // 99 = Deleted
        //
        public int Status { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string OrderNumber { get; set; }
        public int FreightCarrierId { get; set; }
    }
}
