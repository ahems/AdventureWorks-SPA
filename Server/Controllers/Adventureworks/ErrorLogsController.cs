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
    [Route("odata/adventureworks/ErrorLogs")]
    public partial class ErrorLogsController : ODataController
    {
        private AdventureWorks.Server.Data.adventureworksContext context;

        public ErrorLogsController(AdventureWorks.Server.Data.adventureworksContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<AdventureWorks.Server.Models.adventureworks.ErrorLog> GetErrorLogs()
        {
            var items = this.context.ErrorLogs.AsQueryable<AdventureWorks.Server.Models.adventureworks.ErrorLog>();
            this.OnErrorLogsRead(ref items);

            return items;
        }

        partial void OnErrorLogsRead(ref IQueryable<AdventureWorks.Server.Models.adventureworks.ErrorLog> items);

        partial void OnErrorLogGet(ref SingleResult<AdventureWorks.Server.Models.adventureworks.ErrorLog> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/adventureworks/ErrorLogs(ErrorLogId={ErrorLogId})")]
        public SingleResult<AdventureWorks.Server.Models.adventureworks.ErrorLog> GetErrorLog(int key)
        {
            var items = this.context.ErrorLogs.Where(i => i.ErrorLogId == key);
            var result = SingleResult.Create(items);

            OnErrorLogGet(ref result);

            return result;
        }
        partial void OnErrorLogDeleted(AdventureWorks.Server.Models.adventureworks.ErrorLog item);
        partial void OnAfterErrorLogDeleted(AdventureWorks.Server.Models.adventureworks.ErrorLog item);

        [HttpDelete("/odata/adventureworks/ErrorLogs(ErrorLogId={ErrorLogId})")]
        public IActionResult DeleteErrorLog(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.ErrorLogs
                    .Where(i => i.ErrorLogId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnErrorLogDeleted(item);
                this.context.ErrorLogs.Remove(item);
                this.context.SaveChanges();
                this.OnAfterErrorLogDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnErrorLogUpdated(AdventureWorks.Server.Models.adventureworks.ErrorLog item);
        partial void OnAfterErrorLogUpdated(AdventureWorks.Server.Models.adventureworks.ErrorLog item);

        [HttpPut("/odata/adventureworks/ErrorLogs(ErrorLogId={ErrorLogId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutErrorLog(int key, [FromBody]AdventureWorks.Server.Models.adventureworks.ErrorLog item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.ErrorLogId != key))
                {
                    return BadRequest();
                }
                this.OnErrorLogUpdated(item);
                this.context.ErrorLogs.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ErrorLogs.Where(i => i.ErrorLogId == key);
                
                this.OnAfterErrorLogUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/adventureworks/ErrorLogs(ErrorLogId={ErrorLogId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchErrorLog(int key, [FromBody]Delta<AdventureWorks.Server.Models.adventureworks.ErrorLog> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.ErrorLogs.Where(i => i.ErrorLogId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnErrorLogUpdated(item);
                this.context.ErrorLogs.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ErrorLogs.Where(i => i.ErrorLogId == key);
                
                this.OnAfterErrorLogUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnErrorLogCreated(AdventureWorks.Server.Models.adventureworks.ErrorLog item);
        partial void OnAfterErrorLogCreated(AdventureWorks.Server.Models.adventureworks.ErrorLog item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] AdventureWorks.Server.Models.adventureworks.ErrorLog item)
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

                this.OnErrorLogCreated(item);
                this.context.ErrorLogs.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ErrorLogs.Where(i => i.ErrorLogId == item.ErrorLogId);

                

                this.OnAfterErrorLogCreated(item);

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
