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
    [Route("odata/adventureworks/SalesltProductDescriptions")]
    public partial class SalesltProductDescriptionsController : ODataController
    {
        private AdventureWorks.Server.Data.adventureworksContext context;

        public SalesltProductDescriptionsController(AdventureWorks.Server.Data.adventureworksContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<AdventureWorks.Server.Models.adventureworks.SalesltProductdescription> GetSalesltProductDescriptions()
        {
            var items = this.context.SalesltProductDescriptions.AsQueryable<AdventureWorks.Server.Models.adventureworks.SalesltProductdescription>();
            this.OnSalesltProductDescriptionsRead(ref items);

            return items;
        }

        partial void OnSalesltProductDescriptionsRead(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltProductdescription> items);

        partial void OnSalesltProductdescriptionGet(ref SingleResult<AdventureWorks.Server.Models.adventureworks.SalesltProductdescription> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/adventureworks/SalesltProductDescriptions(ProductDescriptionId={ProductDescriptionId})")]
        public SingleResult<AdventureWorks.Server.Models.adventureworks.SalesltProductdescription> GetSalesltProductdescription(int key)
        {
            var items = this.context.SalesltProductDescriptions.Where(i => i.ProductDescriptionId == key);
            var result = SingleResult.Create(items);

            OnSalesltProductdescriptionGet(ref result);

            return result;
        }
        partial void OnSalesltProductdescriptionDeleted(AdventureWorks.Server.Models.adventureworks.SalesltProductdescription item);
        partial void OnAfterSalesltProductdescriptionDeleted(AdventureWorks.Server.Models.adventureworks.SalesltProductdescription item);

        [HttpDelete("/odata/adventureworks/SalesltProductDescriptions(ProductDescriptionId={ProductDescriptionId})")]
        public IActionResult DeleteSalesltProductdescription(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.SalesltProductDescriptions
                    .Where(i => i.ProductDescriptionId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnSalesltProductdescriptionDeleted(item);
                this.context.SalesltProductDescriptions.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSalesltProductdescriptionDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalesltProductdescriptionUpdated(AdventureWorks.Server.Models.adventureworks.SalesltProductdescription item);
        partial void OnAfterSalesltProductdescriptionUpdated(AdventureWorks.Server.Models.adventureworks.SalesltProductdescription item);

        [HttpPut("/odata/adventureworks/SalesltProductDescriptions(ProductDescriptionId={ProductDescriptionId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSalesltProductdescription(int key, [FromBody]AdventureWorks.Server.Models.adventureworks.SalesltProductdescription item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.ProductDescriptionId != key))
                {
                    return BadRequest();
                }
                this.OnSalesltProductdescriptionUpdated(item);
                this.context.SalesltProductDescriptions.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltProductDescriptions.Where(i => i.ProductDescriptionId == key);
                
                this.OnAfterSalesltProductdescriptionUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/adventureworks/SalesltProductDescriptions(ProductDescriptionId={ProductDescriptionId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSalesltProductdescription(int key, [FromBody]Delta<AdventureWorks.Server.Models.adventureworks.SalesltProductdescription> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.SalesltProductDescriptions.Where(i => i.ProductDescriptionId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnSalesltProductdescriptionUpdated(item);
                this.context.SalesltProductDescriptions.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltProductDescriptions.Where(i => i.ProductDescriptionId == key);
                
                this.OnAfterSalesltProductdescriptionUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalesltProductdescriptionCreated(AdventureWorks.Server.Models.adventureworks.SalesltProductdescription item);
        partial void OnAfterSalesltProductdescriptionCreated(AdventureWorks.Server.Models.adventureworks.SalesltProductdescription item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] AdventureWorks.Server.Models.adventureworks.SalesltProductdescription item)
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

                this.OnSalesltProductdescriptionCreated(item);
                this.context.SalesltProductDescriptions.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltProductDescriptions.Where(i => i.ProductDescriptionId == item.ProductDescriptionId);

                

                this.OnAfterSalesltProductdescriptionCreated(item);

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
