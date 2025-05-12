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
    [Route("odata/adventureworks/SalesltAddresses")]
    public partial class SalesltAddressesController : ODataController
    {
        private AdventureWorks.Server.Data.adventureworksContext context;

        public SalesltAddressesController(AdventureWorks.Server.Data.adventureworksContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<AdventureWorks.Server.Models.adventureworks.SalesltAddress> GetSalesltAddresses()
        {
            var items = this.context.SalesltAddresses.AsQueryable<AdventureWorks.Server.Models.adventureworks.SalesltAddress>();
            this.OnSalesltAddressesRead(ref items);

            return items;
        }

        partial void OnSalesltAddressesRead(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltAddress> items);

        partial void OnSalesltAddressGet(ref SingleResult<AdventureWorks.Server.Models.adventureworks.SalesltAddress> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/adventureworks/SalesltAddresses(AddressId={AddressId})")]
        public SingleResult<AdventureWorks.Server.Models.adventureworks.SalesltAddress> GetSalesltAddress(int key)
        {
            var items = this.context.SalesltAddresses.Where(i => i.AddressId == key);
            var result = SingleResult.Create(items);

            OnSalesltAddressGet(ref result);

            return result;
        }
        partial void OnSalesltAddressDeleted(AdventureWorks.Server.Models.adventureworks.SalesltAddress item);
        partial void OnAfterSalesltAddressDeleted(AdventureWorks.Server.Models.adventureworks.SalesltAddress item);

        [HttpDelete("/odata/adventureworks/SalesltAddresses(AddressId={AddressId})")]
        public IActionResult DeleteSalesltAddress(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.SalesltAddresses
                    .Where(i => i.AddressId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnSalesltAddressDeleted(item);
                this.context.SalesltAddresses.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSalesltAddressDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalesltAddressUpdated(AdventureWorks.Server.Models.adventureworks.SalesltAddress item);
        partial void OnAfterSalesltAddressUpdated(AdventureWorks.Server.Models.adventureworks.SalesltAddress item);

        [HttpPut("/odata/adventureworks/SalesltAddresses(AddressId={AddressId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSalesltAddress(int key, [FromBody]AdventureWorks.Server.Models.adventureworks.SalesltAddress item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.AddressId != key))
                {
                    return BadRequest();
                }
                this.OnSalesltAddressUpdated(item);
                this.context.SalesltAddresses.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltAddresses.Where(i => i.AddressId == key);
                
                this.OnAfterSalesltAddressUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/adventureworks/SalesltAddresses(AddressId={AddressId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSalesltAddress(int key, [FromBody]Delta<AdventureWorks.Server.Models.adventureworks.SalesltAddress> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.SalesltAddresses.Where(i => i.AddressId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnSalesltAddressUpdated(item);
                this.context.SalesltAddresses.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltAddresses.Where(i => i.AddressId == key);
                
                this.OnAfterSalesltAddressUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalesltAddressCreated(AdventureWorks.Server.Models.adventureworks.SalesltAddress item);
        partial void OnAfterSalesltAddressCreated(AdventureWorks.Server.Models.adventureworks.SalesltAddress item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] AdventureWorks.Server.Models.adventureworks.SalesltAddress item)
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

                this.OnSalesltAddressCreated(item);
                this.context.SalesltAddresses.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltAddresses.Where(i => i.AddressId == item.AddressId);

                

                this.OnAfterSalesltAddressCreated(item);

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
