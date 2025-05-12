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
    [Route("odata/adventureworks/SalesltProductModels")]
    public partial class SalesltProductModelsController : ODataController
    {
        private AdventureWorks.Server.Data.adventureworksContext context;

        public SalesltProductModelsController(AdventureWorks.Server.Data.adventureworksContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<AdventureWorks.Server.Models.adventureworks.SalesltProductmodel> GetSalesltProductModels()
        {
            var items = this.context.SalesltProductModels.AsQueryable<AdventureWorks.Server.Models.adventureworks.SalesltProductmodel>();
            this.OnSalesltProductModelsRead(ref items);

            return items;
        }

        partial void OnSalesltProductModelsRead(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltProductmodel> items);

        partial void OnSalesltProductmodelGet(ref SingleResult<AdventureWorks.Server.Models.adventureworks.SalesltProductmodel> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/adventureworks/SalesltProductModels(ProductModelId={ProductModelId})")]
        public SingleResult<AdventureWorks.Server.Models.adventureworks.SalesltProductmodel> GetSalesltProductmodel(int key)
        {
            var items = this.context.SalesltProductModels.Where(i => i.ProductModelId == key);
            var result = SingleResult.Create(items);

            OnSalesltProductmodelGet(ref result);

            return result;
        }
        partial void OnSalesltProductmodelDeleted(AdventureWorks.Server.Models.adventureworks.SalesltProductmodel item);
        partial void OnAfterSalesltProductmodelDeleted(AdventureWorks.Server.Models.adventureworks.SalesltProductmodel item);

        [HttpDelete("/odata/adventureworks/SalesltProductModels(ProductModelId={ProductModelId})")]
        public IActionResult DeleteSalesltProductmodel(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.SalesltProductModels
                    .Where(i => i.ProductModelId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnSalesltProductmodelDeleted(item);
                this.context.SalesltProductModels.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSalesltProductmodelDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalesltProductmodelUpdated(AdventureWorks.Server.Models.adventureworks.SalesltProductmodel item);
        partial void OnAfterSalesltProductmodelUpdated(AdventureWorks.Server.Models.adventureworks.SalesltProductmodel item);

        [HttpPut("/odata/adventureworks/SalesltProductModels(ProductModelId={ProductModelId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSalesltProductmodel(int key, [FromBody]AdventureWorks.Server.Models.adventureworks.SalesltProductmodel item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.ProductModelId != key))
                {
                    return BadRequest();
                }
                this.OnSalesltProductmodelUpdated(item);
                this.context.SalesltProductModels.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltProductModels.Where(i => i.ProductModelId == key);
                
                this.OnAfterSalesltProductmodelUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/adventureworks/SalesltProductModels(ProductModelId={ProductModelId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSalesltProductmodel(int key, [FromBody]Delta<AdventureWorks.Server.Models.adventureworks.SalesltProductmodel> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.SalesltProductModels.Where(i => i.ProductModelId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnSalesltProductmodelUpdated(item);
                this.context.SalesltProductModels.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltProductModels.Where(i => i.ProductModelId == key);
                
                this.OnAfterSalesltProductmodelUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalesltProductmodelCreated(AdventureWorks.Server.Models.adventureworks.SalesltProductmodel item);
        partial void OnAfterSalesltProductmodelCreated(AdventureWorks.Server.Models.adventureworks.SalesltProductmodel item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] AdventureWorks.Server.Models.adventureworks.SalesltProductmodel item)
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

                this.OnSalesltProductmodelCreated(item);
                this.context.SalesltProductModels.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltProductModels.Where(i => i.ProductModelId == item.ProductModelId);

                

                this.OnAfterSalesltProductmodelCreated(item);

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
