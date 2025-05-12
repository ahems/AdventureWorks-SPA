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
    [Route("odata/adventureworks/SalesltProductCategories")]
    public partial class SalesltProductCategoriesController : ODataController
    {
        private AdventureWorks.Server.Data.adventureworksContext context;

        public SalesltProductCategoriesController(AdventureWorks.Server.Data.adventureworksContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<AdventureWorks.Server.Models.adventureworks.SalesltProductcategory> GetSalesltProductCategories()
        {
            var items = this.context.SalesltProductCategories.AsQueryable<AdventureWorks.Server.Models.adventureworks.SalesltProductcategory>();
            this.OnSalesltProductCategoriesRead(ref items);

            return items;
        }

        partial void OnSalesltProductCategoriesRead(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltProductcategory> items);

        partial void OnSalesltProductcategoryGet(ref SingleResult<AdventureWorks.Server.Models.adventureworks.SalesltProductcategory> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/adventureworks/SalesltProductCategories(ProductCategoryId={ProductCategoryId})")]
        public SingleResult<AdventureWorks.Server.Models.adventureworks.SalesltProductcategory> GetSalesltProductcategory(int key)
        {
            var items = this.context.SalesltProductCategories.Where(i => i.ProductCategoryId == key);
            var result = SingleResult.Create(items);

            OnSalesltProductcategoryGet(ref result);

            return result;
        }
        partial void OnSalesltProductcategoryDeleted(AdventureWorks.Server.Models.adventureworks.SalesltProductcategory item);
        partial void OnAfterSalesltProductcategoryDeleted(AdventureWorks.Server.Models.adventureworks.SalesltProductcategory item);

        [HttpDelete("/odata/adventureworks/SalesltProductCategories(ProductCategoryId={ProductCategoryId})")]
        public IActionResult DeleteSalesltProductcategory(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.SalesltProductCategories
                    .Where(i => i.ProductCategoryId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnSalesltProductcategoryDeleted(item);
                this.context.SalesltProductCategories.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSalesltProductcategoryDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalesltProductcategoryUpdated(AdventureWorks.Server.Models.adventureworks.SalesltProductcategory item);
        partial void OnAfterSalesltProductcategoryUpdated(AdventureWorks.Server.Models.adventureworks.SalesltProductcategory item);

        [HttpPut("/odata/adventureworks/SalesltProductCategories(ProductCategoryId={ProductCategoryId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSalesltProductcategory(int key, [FromBody]AdventureWorks.Server.Models.adventureworks.SalesltProductcategory item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.ProductCategoryId != key))
                {
                    return BadRequest();
                }
                this.OnSalesltProductcategoryUpdated(item);
                this.context.SalesltProductCategories.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltProductCategories.Where(i => i.ProductCategoryId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "ProductCategory");
                this.OnAfterSalesltProductcategoryUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/adventureworks/SalesltProductCategories(ProductCategoryId={ProductCategoryId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSalesltProductcategory(int key, [FromBody]Delta<AdventureWorks.Server.Models.adventureworks.SalesltProductcategory> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.SalesltProductCategories.Where(i => i.ProductCategoryId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnSalesltProductcategoryUpdated(item);
                this.context.SalesltProductCategories.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltProductCategories.Where(i => i.ProductCategoryId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "ProductCategory");
                this.OnAfterSalesltProductcategoryUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalesltProductcategoryCreated(AdventureWorks.Server.Models.adventureworks.SalesltProductcategory item);
        partial void OnAfterSalesltProductcategoryCreated(AdventureWorks.Server.Models.adventureworks.SalesltProductcategory item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] AdventureWorks.Server.Models.adventureworks.SalesltProductcategory item)
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

                this.OnSalesltProductcategoryCreated(item);
                this.context.SalesltProductCategories.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalesltProductCategories.Where(i => i.ProductCategoryId == item.ProductCategoryId);

                Request.QueryString = Request.QueryString.Add("$expand", "ProductCategory");

                this.OnAfterSalesltProductcategoryCreated(item);

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
