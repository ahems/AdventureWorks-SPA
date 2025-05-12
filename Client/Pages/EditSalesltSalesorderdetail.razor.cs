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
    public partial class EditSalesltSalesorderdetail
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

        [Parameter]
        public int SalesOrderId { get; set; }

        [Parameter]
        public int SalesOrderDetailId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            salesltSalesorderdetail = await adventureworksService.GetSalesltSalesorderdetailBySalesOrderIdAndSalesOrderDetailId(salesOrderId:SalesOrderId, salesOrderDetailId:SalesOrderDetailId);
        }
        protected bool errorVisible;
        protected AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail salesltSalesorderdetail;

        protected IEnumerable<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader> salesltSalesOrderHeadersForSalesOrderId;

        protected IEnumerable<AdventureWorks.Server.Models.adventureworks.SalesltProduct> salesltProductsForProductId;


        protected int salesltSalesOrderHeadersForSalesOrderIdCount;
        protected AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader salesltSalesOrderHeadersForSalesOrderIdValue;
        protected async Task salesltSalesOrderHeadersForSalesOrderIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await adventureworksService.GetSalesltSalesOrderHeaders(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(SalesOrderNumber, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                salesltSalesOrderHeadersForSalesOrderId = result.Value.AsODataEnumerable();
                salesltSalesOrderHeadersForSalesOrderIdCount = result.Count;

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load SalesOrderHeader" });
            }
        }

        protected int salesltProductsForProductIdCount;
        protected AdventureWorks.Server.Models.adventureworks.SalesltProduct salesltProductsForProductIdValue;
        protected async Task salesltProductsForProductIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await adventureworksService.GetSalesltProducts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Name, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                salesltProductsForProductId = result.Value.AsODataEnumerable();
                salesltProductsForProductIdCount = result.Count;

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Product" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                await adventureworksService.UpdateSalesltSalesorderdetail(salesOrderId:SalesOrderId, salesOrderDetailId:SalesOrderDetailId, salesltSalesorderdetail);
                DialogService.Close(salesltSalesorderdetail);
            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}