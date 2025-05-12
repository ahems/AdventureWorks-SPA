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
    [Route("odata/adventureworks/SalesltProductModelProductDescriptions")]
    public partial class SalesltProductModelProductDescriptionsController : ODataController
    {
        private AdventureWorks.Server.Data.adventureworksContext context;

        public SalesltProductModelProductDescriptionsController(AdventureWorks.Server.Data.adventureworksContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription> GetSalesltProductModelProductDescriptions()
        {
            var items = this.context.SalesltProductModelProductDescriptions.AsQueryable<AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription>();
            this.OnSalesltProductModelProductDescriptionsRead(ref items);

            return items;
        }

        partial void OnSalesltProductModelProductDescriptionsRead(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription> items);

        partial void OnSalesltProductmodelproductdescriptionGet(ref SingleResult<AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/adventureworks/SalesltProductModelProductDescriptions(ProductModelId={keyProductModelId},ProductDescriptionId={keyProductDescriptionId},Culture={keyCulture})")]
        public SingleResult<AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription> GetSalesltProductmodelproductdescription([FromODataUri] int keyProductModelId, [FromODataUri] int keyProductDescriptionId, [FromODataUri] string keyCulture)
        {
            var items = this.context.SalesltProductModelProductDescriptions.Where(i => i.ProductModelId == keyProductModelId && i.ProductDescriptionId == keyProductDescriptionId && i.Culture == Uri.UnescapeDataString(keyCulture));
            var result = SingleResult.Create(items);

            OnSalesltProductmodelproductdescriptionGet(ref result);

            return result;
        }
        partial void OnSalesltProductmodelproductdescriptionDeleted(AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription item);
        partial void OnAfterSalesltProductmodelproductdescriptionDeleted(AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription item);

        [HttpDelete("/odata/adventureworks/SalesltProductModelProductDescriptions(ProductModelId={keyProductModelId},ProductDescriptionId={keyProductDescriptionId},Culture={keyCulture})")]
        public IActionResult DeleteSalesltProductmodelproductdescription([FromODataUri] int keyProductModelId, [FromODataUri] int keyProductDescriptionId, [FromODataUri] string keyCulture)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.SalesltProductModelProductDescriptions
                    .Where(i => i.ProductModelId == keyProductModelId && i.ProductDescriptionId == keyProductDescriptionId && i.Culture == Uri.UnescapeDataString(keyCulture))
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnSalesltProductmodelproductdescriptionDeleted(item);
                this.context.SalesltProductModelProductDescriptions.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSalesltProductmodelproductdescriptionDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalesltProductmodelproductdescriptionUpdated(AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription item);
        partial void OnAfterSalesltProductmodelproductdescriptionUpdated(AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription item);

        [HttpPut("/odata/adventureworks/SalesltProductModelProductDescriptions(ProductModelId={keyProductModelId},ProductDescriptionId={keyProductDescriptionId},Culture={keyCulture})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSalesltProductmodelproductdescription([FromODataUri] int keyProductModelId, [FromODataUri] int keyProductDescriptionId, [FromODataUri] string keyCulture, [FromBody]AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.ProductModelId != keyProductModelId && item.ProductDescriptionId != keyProductDescriptionId && item.Culture != Uri.UnescapeDataString(keyCulture)))
                {
                    return BadRequest();
                }
                this.OnSalesltProductmodelproductdescriptionUpdated(item);
                this.context.SalesltProductModelProductDescriptions.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltProductModelProductDescriptions.Where(i => i.ProductModelId == keyProductModelId && i.ProductDescriptionId == keyProductDescriptionId && i.Culture == Uri.UnescapeDataString(keyCulture));
                Request.QueryString = Request.QueryString.Add("$expand", "ProductDescription,ProductModel");
                this.OnAfterSalesltProductmodelproductdescriptionUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/adventureworks/SalesltProductModelProductDescriptions(ProductModelId={keyProductModelId},ProductDescriptionId={keyProductDescriptionId},Culture={keyCulture})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSalesltProductmodelproductdescription([FromODataUri] int keyProductModelId, [FromODataUri] int keyProductDescriptionId, [FromODataUri] string keyCulture, [FromBody]Delta<AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.SalesltProductModelProductDescriptions.Where(i => i.ProductModelId == keyProductModelId && i.ProductDescriptionId == keyProductDescriptionId && i.Culture == Uri.UnescapeDataString(keyCulture)).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnSalesltProductmodelproductdescriptionUpdated(item);
                this.context.SalesltProductModelProductDescriptions.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltProductModelProductDescriptions.Where(i => i.ProductModelId == keyProductModelId && i.ProductDescriptionId == keyProductDescriptionId && i.Culture == Uri.UnescapeDataString(keyCulture));
                Request.QueryString = Request.QueryString.Add("$expand", "ProductDescription,ProductModel");
                this.OnAfterSalesltProductmodelproductdescriptionUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalesltProductmodelproductdescriptionCreated(AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription item);
        partial void OnAfterSalesltProductmodelproductdescriptionCreated(AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription item)
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

                this.OnSalesltProductmodelproductdescriptionCreated(item);
                this.context.SalesltProductModelProductDescriptions.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltProductModelProductDescriptions.Where(i => i.ProductModelId == item.ProductModelId && i.ProductDescriptionId == item.ProductDescriptionId && i.Culture == item.Culture);

                Request.QueryString = Request.QueryString.Add("$expand", "ProductDescription,ProductModel");

                this.OnAfterSalesltProductmodelproductdescriptionCreated(item);

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
