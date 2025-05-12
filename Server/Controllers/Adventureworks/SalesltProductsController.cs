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
    [Route("odata/adventureworks/SalesltProducts")]
    public partial class SalesltProductsController : ODataController
    {
        private AdventureWorks.Server.Data.adventureworksContext context;

        public SalesltProductsController(AdventureWorks.Server.Data.adventureworksContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<AdventureWorks.Server.Models.adventureworks.SalesltProduct> GetSalesltProducts()
        {
            var items = this.context.SalesltProducts.AsQueryable<AdventureWorks.Server.Models.adventureworks.SalesltProduct>();
            this.OnSalesltProductsRead(ref items);

            return items;
        }

        partial void OnSalesltProductsRead(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltProduct> items);

        partial void OnSalesltProductGet(ref SingleResult<AdventureWorks.Server.Models.adventureworks.SalesltProduct> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/adventureworks/SalesltProducts(ProductId={ProductId})")]
        public SingleResult<AdventureWorks.Server.Models.adventureworks.SalesltProduct> GetSalesltProduct(int key)
        {
            var items = this.context.SalesltProducts.Where(i => i.ProductId == key);
            var result = SingleResult.Create(items);

            OnSalesltProductGet(ref result);

            return result;
        }
        partial void OnSalesltProductDeleted(AdventureWorks.Server.Models.adventureworks.SalesltProduct item);
        partial void OnAfterSalesltProductDeleted(AdventureWorks.Server.Models.adventureworks.SalesltProduct item);

        [HttpDelete("/odata/adventureworks/SalesltProducts(ProductId={ProductId})")]
        public IActionResult DeleteSalesltProduct(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.SalesltProducts
                    .Where(i => i.ProductId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnSalesltProductDeleted(item);
                this.context.SalesltProducts.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSalesltProductDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalesltProductUpdated(AdventureWorks.Server.Models.adventureworks.SalesltProduct item);
        partial void OnAfterSalesltProductUpdated(AdventureWorks.Server.Models.adventureworks.SalesltProduct item);

        [HttpPut("/odata/adventureworks/SalesltProducts(ProductId={ProductId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSalesltProduct(int key, [FromBody]AdventureWorks.Server.Models.adventureworks.SalesltProduct item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.ProductId != key))
                {
                    return BadRequest();
                }
                this.OnSalesltProductUpdated(item);
                this.context.SalesltProducts.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltProducts.Where(i => i.ProductId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "ProductCategory,ProductModel");
                this.OnAfterSalesltProductUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/adventureworks/SalesltProducts(ProductId={ProductId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSalesltProduct(int key, [FromBody]Delta<AdventureWorks.Server.Models.adventureworks.SalesltProduct> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.SalesltProducts.Where(i => i.ProductId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnSalesltProductUpdated(item);
                this.context.SalesltProducts.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltProducts.Where(i => i.ProductId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "ProductCategory,ProductModel");
                this.OnAfterSalesltProductUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalesltProductCreated(AdventureWorks.Server.Models.adventureworks.SalesltProduct item);
        partial void OnAfterSalesltProductCreated(AdventureWorks.Server.Models.adventureworks.SalesltProduct item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] AdventureWorks.Server.Models.adventureworks.SalesltProduct item)
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

                this.OnSalesltProductCreated(item);
                this.context.SalesltProducts.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltProducts.Where(i => i.ProductId == item.ProductId);

                Request.QueryString = Request.QueryString.Add("$expand", "ProductCategory,ProductModel");

                this.OnAfterSalesltProductCreated(item);

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
