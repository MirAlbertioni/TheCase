using System;

namespace Fac.Api.Models
{
    public class Case
    {
        public Guid CaseId { get; set; }
        public string Message { get; set; }
        public string Image { get; set; }
        public string SiteRefId { get; set; }
        public string Sender { get; set; }
        //Status 
        //0 = Unread
        //1 = Read
        //99 = Deleted (Remove from DB after xx days)
        public int Status { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string OrderNumber { get; set; }
        public int FreightCarrierId { get; set; }
    }
}