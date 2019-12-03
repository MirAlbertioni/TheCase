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
    public class CaseController : Controller
    {
        private readonly AppSettings _settings;
        private FacDbContext _facDbContext;

        public CaseController(FacDbContext facDbContext, AppSettings settings)
        {
            _facDbContext = facDbContext;
            _settings = settings;
        }

        [HttpGet]
        [Route("GetCases")]
        public List<CaseSummary> GetCases()
        {
            var cases = _facDbContext.Case.Select(c => new CaseSummary()
            {
                CaseId = c.CaseId,
                FreightCarrierId = c.FreightCarrierId,
                Image = c.Image,
                Message = c.Message,
                OrderNumber = c.OrderNumber,
                Sender = c.Sender,
                SiteRefId = c.SiteRefId,
                Status = c.Status,
                CategoryName = _facDbContext.Category.FirstOrDefault(x => x.CategoryId == c.CategoryId) != null 
                ? _facDbContext.Category.FirstOrDefault(x => x.CategoryId == c.CategoryId).CategoryName 
                : "",
                SubCategoryName = _facDbContext.SubCategory.FirstOrDefault(x => x.SubCategoryId == c.SubCategoryId) != null 
                ? _facDbContext.SubCategory.FirstOrDefault(x => x.SubCategoryId == c.SubCategoryId).SubCategoryName 
                : ""

            });
            return cases.ToList();
        }

        [HttpPost]
        [Route("AddCase")]
        public HttpResponseMessage AddCase([FromBody] Case request)
        {
            //Logger starts
            if (request != null || request.CaseId == Guid.Empty)
            {
                //Logger add case
                try
                {
                    _facDbContext.Case.Add(request);

                    //Logger try save changes
                    _facDbContext.SaveChanges();
                }
                catch (Exception e)
                {
                    //Logger failed
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
