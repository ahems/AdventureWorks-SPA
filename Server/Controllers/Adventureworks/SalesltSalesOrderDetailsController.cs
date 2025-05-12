using System;
using System.Net;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AdventureWorks.Server.Controllers.adventureworks
{
    [Route("odata/adventureworks/SalesltSalesOrderDetails")]
    public partial class SalesltSalesOrderDetailsController : ODataController
    {
        private AdventureWorks.Server.Data.adventureworksContext context;

        public SalesltSalesOrderDetailsController(AdventureWorks.Server.Data.adventureworksContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail> GetSalesltSalesOrderDetails()
        {
            var items = this.context.SalesltSalesOrderDetails.AsQueryable<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail>();
            this.OnSalesltSalesOrderDetailsRead(ref items);

            return items;
        }

        partial void OnSalesltSalesOrderDetailsRead(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail> items);

        partial void OnSalesltSalesorderdetailGet(ref SingleResult<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/adventureworks/SalesltSalesOrderDetails(SalesOrderId={keySalesOrderId},SalesOrderDetailId={keySalesOrderDetailId})")]
        public SingleResult<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail> GetSalesltSalesorderdetail([FromODataUri] int keySalesOrderId, [FromODataUri] int keySalesOrderDetailId)
        {
            var items = this.context.SalesltSalesOrderDetails.Where(i => i.SalesOrderId == keySalesOrderId && i.SalesOrderDetailId == keySalesOrderDetailId);
            var result = SingleResult.Create(items);

            OnSalesltSalesorderdetailGet(ref result);

            return result;
        }
        partial void OnSalesltSalesorderdetailDeleted(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail item);
        partial void OnAfterSalesltSalesorderdetailDeleted(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail item);

        [HttpDelete("/odata/adventureworks/SalesltSalesOrderDetails(SalesOrderId={keySalesOrderId},SalesOrderDetailId={keySalesOrderDetailId})")]
        public IActionResult DeleteSalesltSalesorderdetail([FromODataUri] int keySalesOrderId, [FromODataUri] int keySalesOrderDetailId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.SalesltSalesOrderDetails
                    .Where(i => i.SalesOrderId == keySalesOrderId && i.SalesOrderDetailId == keySalesOrderDetailId)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnSalesltSalesorderdetailDeleted(item);
                this.context.SalesltSalesOrderDetails.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSalesltSalesorderdetailDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalesltSalesorderdetailUpdated(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail item);
        partial void OnAfterSalesltSalesorderdetailUpdated(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail item);

        [HttpPut("/odata/adventureworks/SalesltSalesOrderDetails(SalesOrderId={keySalesOrderId},SalesOrderDetailId={keySalesOrderDetailId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSalesltSalesorderdetail([FromODataUri] int keySalesOrderId, [FromODataUri] int keySalesOrderDetailId, [FromBody]AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.SalesOrderId != keySalesOrderId && item.SalesOrderDetailId != keySalesOrderDetailId))
                {
                    return BadRequest();
                }
                this.OnSalesltSalesorderdetailUpdated(item);
                this.context.SalesltSalesOrderDetails.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltSalesOrderDetails.Where(i => i.SalesOrderId == keySalesOrderId && i.SalesOrderDetailId == keySalesOrderDetailId);
                Request.QueryString = Request.QueryString.Add("$expand", "Product,SalesOrderHeader");
                this.OnAfterSalesltSalesorderdetailUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/adventureworks/SalesltSalesOrderDetails(SalesOrderId={keySalesOrderId},SalesOrderDetailId={keySalesOrderDetailId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSalesltSalesorderdetail([FromODataUri] int keySalesOrderId, [FromODataUri] int keySalesOrderDetailId, [FromBody]Delta<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.SalesltSalesOrderDetails.Where(i => i.SalesOrderId == keySalesOrderId && i.SalesOrderDetailId == keySalesOrderDetailId).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnSalesltSalesorderdetailUpdated(item);
                this.context.SalesltSalesOrderDetails.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltSalesOrderDetails.Where(i => i.SalesOrderId == keySalesOrderId && i.SalesOrderDetailId == keySalesOrderDetailId);
                Request.QueryString = Request.QueryString.Add("$expand", "Product,SalesOrderHeader");
                this.OnAfterSalesltSalesorderdetailUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalesltSalesorderdetailCreated(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail item);
        partial void OnAfterSalesltSalesorderdetailCreated(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null)
                {
                    return BadRequest();
                }

                this.OnSalesltSalesorderdetailCreated(item);
                this.context.SalesltSalesOrderDetails.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltSalesOrderDetails.Where(i => i.SalesOrderId == item.SalesOrderId && i.SalesOrderDetailId == item.SalesOrderDetailId);

                Request.QueryString = Request.QueryString.Add("$expand", "Product,SalesOrderHeader");

                this.OnAfterSalesltSalesorderdetailCreated(item);

                return new ObjectResult(SingleResult.Create(itemToReturn))
                {
                    StatusCode = 201
                };
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}
