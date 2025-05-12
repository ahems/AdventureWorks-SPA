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
    [Route("odata/adventureworks/SalesltCustomerAddresses")]
    public partial class SalesltCustomerAddressesController : ODataController
    {
        private AdventureWorks.Server.Data.adventureworksContext context;

        public SalesltCustomerAddressesController(AdventureWorks.Server.Data.adventureworksContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress> GetSalesltCustomerAddresses()
        {
            var items = this.context.SalesltCustomerAddresses.AsQueryable<AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress>();
            this.OnSalesltCustomerAddressesRead(ref items);

            return items;
        }

        partial void OnSalesltCustomerAddressesRead(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress> items);

        partial void OnSalesltCustomeraddressGet(ref SingleResult<AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/adventureworks/SalesltCustomerAddresses(CustomerId={keyCustomerId},AddressId={keyAddressId})")]
        public SingleResult<AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress> GetSalesltCustomeraddress([FromODataUri] int keyCustomerId, [FromODataUri] int keyAddressId)
        {
            var items = this.context.SalesltCustomerAddresses.Where(i => i.CustomerId == keyCustomerId && i.AddressId == keyAddressId);
            var result = SingleResult.Create(items);

            OnSalesltCustomeraddressGet(ref result);

            return result;
        }
        partial void OnSalesltCustomeraddressDeleted(AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress item);
        partial void OnAfterSalesltCustomeraddressDeleted(AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress item);

        [HttpDelete("/odata/adventureworks/SalesltCustomerAddresses(CustomerId={keyCustomerId},AddressId={keyAddressId})")]
        public IActionResult DeleteSalesltCustomeraddress([FromODataUri] int keyCustomerId, [FromODataUri] int keyAddressId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.SalesltCustomerAddresses
                    .Where(i => i.CustomerId == keyCustomerId && i.AddressId == keyAddressId)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnSalesltCustomeraddressDeleted(item);
                this.context.SalesltCustomerAddresses.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSalesltCustomeraddressDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalesltCustomeraddressUpdated(AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress item);
        partial void OnAfterSalesltCustomeraddressUpdated(AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress item);

        [HttpPut("/odata/adventureworks/SalesltCustomerAddresses(CustomerId={keyCustomerId},AddressId={keyAddressId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSalesltCustomeraddress([FromODataUri] int keyCustomerId, [FromODataUri] int keyAddressId, [FromBody]AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.CustomerId != keyCustomerId && item.AddressId != keyAddressId))
                {
                    return BadRequest();
                }
                this.OnSalesltCustomeraddressUpdated(item);
                this.context.SalesltCustomerAddresses.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltCustomerAddresses.Where(i => i.CustomerId == keyCustomerId && i.AddressId == keyAddressId);
                Request.QueryString = Request.QueryString.Add("$expand", "Address,Customer");
                this.OnAfterSalesltCustomeraddressUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/adventureworks/SalesltCustomerAddresses(CustomerId={keyCustomerId},AddressId={keyAddressId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSalesltCustomeraddress([FromODataUri] int keyCustomerId, [FromODataUri] int keyAddressId, [FromBody]Delta<AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.SalesltCustomerAddresses.Where(i => i.CustomerId == keyCustomerId && i.AddressId == keyAddressId).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnSalesltCustomeraddressUpdated(item);
                this.context.SalesltCustomerAddresses.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltCustomerAddresses.Where(i => i.CustomerId == keyCustomerId && i.AddressId == keyAddressId);
                Request.QueryString = Request.QueryString.Add("$expand", "Address,Customer");
                this.OnAfterSalesltCustomeraddressUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalesltCustomeraddressCreated(AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress item);
        partial void OnAfterSalesltCustomeraddressCreated(AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress item)
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

                this.OnSalesltCustomeraddressCreated(item);
                this.context.SalesltCustomerAddresses.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltCustomerAddresses.Where(i => i.CustomerId == item.CustomerId && i.AddressId == item.AddressId);

                Request.QueryString = Request.QueryString.Add("$expand", "Address,Customer");

                this.OnAfterSalesltCustomeraddressCreated(item);

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
