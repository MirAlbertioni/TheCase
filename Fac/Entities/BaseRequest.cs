namespace Fac.Entities
{
    public class BaseRequest
    {
        protected BaseRequest(string siteReferenceId, string pickerId)
        {
            SiteRefId = siteReferenceId;
            UserId = pickerId;
        }

        public string SiteRefId { get; set; }
        public string UserId { get; set; }
    }
}