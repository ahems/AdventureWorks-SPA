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
    public partial class AddSalesltSalesorderheader
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

        protected override async Task OnInitializedAsync()
        {
            salesltSalesorderheader = new AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader();
        }
        protected bool errorVisible;
        protected AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader salesltSalesorderheader;

        protected IEnumerable<AdventureWorks.Server.Models.adventureworks.SalesltCustomer> salesltCustomersForCustomerId;

        protected IEnumerable<AdventureWorks.Server.Models.adventureworks.SalesltAddress> salesltAddressesForShipToAddressId;

        protected IEnumerable<AdventureWorks.Server.Models.adventureworks.SalesltAddress> salesltAddressesForBillToAddressId;


        protected int salesltCustomersForCustomerIdCount;
        protected AdventureWorks.Server.Models.adventureworks.SalesltCustomer salesltCustomersForCustomerIdValue;
        protected async Task salesltCustomersForCustomerIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await adventureworksService.GetSalesltCustomers(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Title, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                salesltCustomersForCustomerId = result.Value.AsODataEnumerable();
                salesltCustomersForCustomerIdCount = result.Count;

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Customer" });
            }
        }

        protected int salesltAddressesForShipToAddressIdCount;
        protected AdventureWorks.Server.Models.adventureworks.SalesltAddress salesltAddressesForShipToAddressIdValue;
        protected async Task salesltAddressesForShipToAddressIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await adventureworksService.GetSalesltAddresses(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(AddressLine1, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                salesltAddressesForShipToAddressId = result.Value.AsODataEnumerable();
                salesltAddressesForShipToAddressIdCount = result.Count;

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Address1" });
            }
        }

        protected int salesltAddressesForBillToAddressIdCount;
        protected AdventureWorks.Server.Models.adventureworks.SalesltAddress salesltAddressesForBillToAddressIdValue;
        protected async Task salesltAddressesForBillToAddressIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await adventureworksService.GetSalesltAddresses(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(AddressLine1, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                salesltAddressesForBillToAddressId = result.Value.AsODataEnumerable();
                salesltAddressesForBillToAddressIdCount = result.Count;

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Address" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                await adventureworksService.CreateSalesltSalesorderheader(salesltSalesorderheader);
                DialogService.Close(salesltSalesorderheader);
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