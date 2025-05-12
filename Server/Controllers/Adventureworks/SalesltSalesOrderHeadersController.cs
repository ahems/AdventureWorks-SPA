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
    [Route("odata/adventureworks/SalesltSalesOrderHeaders")]
    public partial class SalesltSalesOrderHeadersController : ODataController
    {
        private AdventureWorks.Server.Data.adventureworksContext context;

        public SalesltSalesOrderHeadersController(AdventureWorks.Server.Data.adventureworksContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader> GetSalesltSalesOrderHeaders()
        {
            var items = this.context.SalesltSalesOrderHeaders.AsQueryable<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader>();
            this.OnSalesltSalesOrderHeadersRead(ref items);

            return items;
        }

        partial void OnSalesltSalesOrderHeadersRead(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader> items);

        partial void OnSalesltSalesorderheaderGet(ref SingleResult<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/adventureworks/SalesltSalesOrderHeaders(SalesOrderId={SalesOrderId})")]
        public SingleResult<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader> GetSalesltSalesorderheader(int key)
        {
            var items = this.context.SalesltSalesOrderHeaders.Where(i => i.SalesOrderId == key);
            var result = SingleResult.Create(items);

            OnSalesltSalesorderheaderGet(ref result);

            return result;
        }
        partial void OnSalesltSalesorderheaderDeleted(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader item);
        partial void OnAfterSalesltSalesorderheaderDeleted(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader item);

        [HttpDelete("/odata/adventureworks/SalesltSalesOrderHeaders(SalesOrderId={SalesOrderId})")]
        public IActionResult DeleteSalesltSalesorderheader(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.SalesltSalesOrderHeaders
                    .Where(i => i.SalesOrderId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnSalesltSalesorderheaderDeleted(item);
                this.context.SalesltSalesOrderHeaders.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSalesltSalesorderheaderDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalesltSalesorderheaderUpdated(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader item);
        partial void OnAfterSalesltSalesorderheaderUpdated(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader item);

        [HttpPut("/odata/adventureworks/SalesltSalesOrderHeaders(SalesOrderId={SalesOrderId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSalesltSalesorderheader(int key, [FromBody]AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.SalesOrderId != key))
                {
                    return BadRequest();
                }
                this.OnSalesltSalesorderheaderUpdated(item);
                this.context.SalesltSalesOrderHeaders.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltSalesOrderHeaders.Where(i => i.SalesOrderId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Address,Customer,Address1");
                this.OnAfterSalesltSalesorderheaderUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/adventureworks/SalesltSalesOrderHeaders(SalesOrderId={SalesOrderId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSalesltSalesorderheader(int key, [FromBody]Delta<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.SalesltSalesOrderHeaders.Where(i => i.SalesOrderId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnSalesltSalesorderheaderUpdated(item);
                this.context.SalesltSalesOrderHeaders.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltSalesOrderHeaders.Where(i => i.SalesOrderId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Address,Customer,Address1");
                this.OnAfterSalesltSalesorderheaderUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalesltSalesorderheaderCreated(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader item);
        partial void OnAfterSalesltSalesorderheaderCreated(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader item)
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

                this.OnSalesltSalesorderheaderCreated(item);
                this.context.SalesltSalesOrderHeaders.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltSalesOrderHeaders.Where(i => i.SalesOrderId == item.SalesOrderId);

                Request.QueryString = Request.QueryString.Add("$expand", "Address,Customer,Address1");

                this.OnAfterSalesltSalesorderheaderCreated(item);

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
