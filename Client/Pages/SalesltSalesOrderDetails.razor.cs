using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace AdventureWorks.Client.Pages
{
    public partial class SalesltSalesOrderDetails
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        public adventureworksService adventureworksService { get; set; }

        protected IEnumerable<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail> salesltSalesOrderDetails;

        protected RadzenDataGrid<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail> grid0;
        protected int count;

        protected async Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var result = await adventureworksService.GetSalesltSalesOrderDetails(filter: $"{args.Filter}", expand: "SalesOrderHeader,Product", orderby: $"{args.OrderBy}", top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null);
                salesltSalesOrderDetails = result.Value.AsODataEnumerable();
                count = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load SalesltSalesOrderDetails" });
            }
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddSalesltSalesorderdetail>("Add SalesltSalesorderdetail", null);
            await grid0.Reload();
        }

        protected async Task EditRow(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail args)
        {
            await DialogService.OpenAsync<EditSalesltSalesorderdetail>("Edit SalesltSalesorderdetail", new Dictionary<string, object> { {"SalesOrderId", args.SalesOrderId}, {"SalesOrderDetailId", args.SalesOrderDetailId} });
            await grid0.Reload();
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail salesltSalesorderdetail)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await adventureworksService.DeleteSalesltSalesorderdetail(salesOrderId:salesltSalesorderdetail.SalesOrderId, salesOrderDetailId:salesltSalesorderdetail.SalesOrderDetailId);

                    if (deleteResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete SalesltSalesorderdetail"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await adventureworksService.ExportSalesltSalesOrderDetailsToCSV(new Query
                {
                    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
                    OrderBy = $"{grid0.Query.OrderBy}",
                    Expand = "SalesOrderHeader,Product",
                    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
                }, "SalesltSalesOrderDetails");
            }

            if (args == null || args.Value == "xlsx")
            {
                await adventureworksService.ExportSalesltSalesOrderDetailsToExcel(new Query
                {
                    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
                    OrderBy = $"{grid0.Query.OrderBy}",
                    Expand = "SalesOrderHeader,Product",
                    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
                }, "SalesltSalesOrderDetails");
            }
        }
    }
}