using Fac.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fac.Entities
{
    public class CaseRequest : BaseRequest
    {
        public CaseRequest(string siteReferenceId, string pickerId, Case request) : base(siteReferenceId, pickerId)
        {
            this.Case = request;
        }

        public Case Case { get; set; }
    }
}
