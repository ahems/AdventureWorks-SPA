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
    [Route("odata/adventureworks/BuildVersions")]
    public partial class BuildVersionsController : ODataController
    {
        private AdventureWorks.Server.Data.adventureworksContext context;

        public BuildVersionsController(AdventureWorks.Server.Data.adventureworksContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<AdventureWorks.Server.Models.adventureworks.BuildVersion> GetBuildVersions()
        {
            var items = this.context.BuildVersions.AsQueryable<AdventureWorks.Server.Models.adventureworks.BuildVersion>();
            this.OnBuildVersionsRead(ref items);

            return items;
        }

        partial void OnBuildVersionsRead(ref IQueryable<AdventureWorks.Server.Models.adventureworks.BuildVersion> items);

        partial void OnBuildVersionGet(ref SingleResult<AdventureWorks.Server.Models.adventureworks.BuildVersion> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/adventureworks/BuildVersions(SystemInformationId={SystemInformationId})")]
        public SingleResult<AdventureWorks.Server.Models.adventureworks.BuildVersion> GetBuildVersion(byte key)
        {
            var items = this.context.BuildVersions.Where(i => i.SystemInformationId == key);
            var result = SingleResult.Create(items);

            OnBuildVersionGet(ref result);

            return result;
        }
        partial void OnBuildVersionDeleted(AdventureWorks.Server.Models.adventureworks.BuildVersion item);
        partial void OnAfterBuildVersionDeleted(AdventureWorks.Server.Models.adventureworks.BuildVersion item);

        [HttpDelete("/odata/adventureworks/BuildVersions(SystemInformationId={SystemInformationId})")]
        public IActionResult DeleteBuildVersion(byte key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.BuildVersions
                    .Where(i => i.SystemInformationId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnBuildVersionDeleted(item);
                this.context.BuildVersions.Remove(item);
                this.context.SaveChanges();
                this.OnAfterBuildVersionDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnBuildVersionUpdated(AdventureWorks.Server.Models.adventureworks.BuildVersion item);
        partial void OnAfterBuildVersionUpdated(AdventureWorks.Server.Models.adventureworks.BuildVersion item);

        [HttpPut("/odata/adventureworks/BuildVersions(SystemInformationId={SystemInformationId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutBuildVersion(byte key, [FromBody]AdventureWorks.Server.Models.adventureworks.BuildVersion item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.SystemInformationId != key))
                {
                    return BadRequest();
                }
                this.OnBuildVersionUpdated(item);
                this.context.BuildVersions.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BuildVersions.Where(i => i.SystemInformationId == key);
                
                this.OnAfterBuildVersionUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/adventureworks/BuildVersions(SystemInformationId={SystemInformationId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchBuildVersion(byte key, [FromBody]Delta<AdventureWorks.Server.Models.adventureworks.BuildVersion> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.BuildVersions.Where(i => i.SystemInformationId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnBuildVersionUpdated(item);
                this.context.BuildVersions.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BuildVersions.Where(i => i.SystemInformationId == key);
                
                this.OnAfterBuildVersionUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnBuildVersionCreated(AdventureWorks.Server.Models.adventureworks.BuildVersion item);
        partial void OnAfterBuildVersionCreated(AdventureWorks.Server.Models.adventureworks.BuildVersion item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] AdventureWorks.Server.Models.adventureworks.BuildVersion item)
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

                this.OnBuildVersionCreated(item);
                this.context.BuildVersions.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BuildVersions.Where(i => i.SystemInformationId == item.SystemInformationId);

                

                this.OnAfterBuildVersionCreated(item);

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
