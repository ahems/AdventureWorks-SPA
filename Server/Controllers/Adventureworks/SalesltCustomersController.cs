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
    [Route("odata/adventureworks/SalesltCustomers")]
    public partial class SalesltCustomersController : ODataController
    {
        private AdventureWorks.Server.Data.adventureworksContext context;

        public SalesltCustomersController(AdventureWorks.Server.Data.adventureworksContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<AdventureWorks.Server.Models.adventureworks.SalesltCustomer> GetSalesltCustomers()
        {
            var items = this.context.SalesltCustomers.AsQueryable<AdventureWorks.Server.Models.adventureworks.SalesltCustomer>();
            this.OnSalesltCustomersRead(ref items);

            return items;
        }

        partial void OnSalesltCustomersRead(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltCustomer> items);

        partial void OnSalesltCustomerGet(ref SingleResult<AdventureWorks.Server.Models.adventureworks.SalesltCustomer> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/adventureworks/SalesltCustomers(CustomerId={CustomerId})")]
        public SingleResult<AdventureWorks.Server.Models.adventureworks.SalesltCustomer> GetSalesltCustomer(int key)
        {
            var items = this.context.SalesltCustomers.Where(i => i.CustomerId == key);
            var result = SingleResult.Create(items);

            OnSalesltCustomerGet(ref result);

            return result;
        }
        partial void OnSalesltCustomerDeleted(AdventureWorks.Server.Models.adventureworks.SalesltCustomer item);
        partial void OnAfterSalesltCustomerDeleted(AdventureWorks.Server.Models.adventureworks.SalesltCustomer item);

        [HttpDelete("/odata/adventureworks/SalesltCustomers(CustomerId={CustomerId})")]
        public IActionResult DeleteSalesltCustomer(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.SalesltCustomers
                    .Where(i => i.CustomerId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnSalesltCustomerDeleted(item);
                this.context.SalesltCustomers.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSalesltCustomerDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalesltCustomerUpdated(AdventureWorks.Server.Models.adventureworks.SalesltCustomer item);
        partial void OnAfterSalesltCustomerUpdated(AdventureWorks.Server.Models.adventureworks.SalesltCustomer item);

        [HttpPut("/odata/adventureworks/SalesltCustomers(CustomerId={CustomerId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSalesltCustomer(int key, [FromBody]AdventureWorks.Server.Models.adventureworks.SalesltCustomer item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.CustomerId != key))
                {
                    return BadRequest();
                }
                this.OnSalesltCustomerUpdated(item);
                this.context.SalesltCustomers.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltCustomers.Where(i => i.CustomerId == key);
                
                this.OnAfterSalesltCustomerUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/adventureworks/SalesltCustomers(CustomerId={CustomerId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSalesltCustomer(int key, [FromBody]Delta<AdventureWorks.Server.Models.adventureworks.SalesltCustomer> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.SalesltCustomers.Where(i => i.CustomerId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnSalesltCustomerUpdated(item);
                this.context.SalesltCustomers.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltCustomers.Where(i => i.CustomerId == key);
                
                this.OnAfterSalesltCustomerUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalesltCustomerCreated(AdventureWorks.Server.Models.adventureworks.SalesltCustomer item);
        partial void OnAfterSalesltCustomerCreated(AdventureWorks.Server.Models.adventureworks.SalesltCustomer item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] AdventureWorks.Server.Models.adventureworks.SalesltCustomer item)
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

                this.OnSalesltCustomerCreated(item);
                this.context.SalesltCustomers.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltCustomers.Where(i => i.CustomerId == item.CustomerId);

                

                this.OnAfterSalesltCustomerCreated(item);

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
