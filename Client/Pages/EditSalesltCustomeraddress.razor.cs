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
    public partial class EditSalesltCustomeraddress
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
        public int CustomerId { get; set; }

        [Parameter]
        public int AddressId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            salesltCustomeraddress = await adventureworksService.GetSalesltCustomeraddressByCustomerIdAndAddressId(customerId:CustomerId, addressId:AddressId);
        }
        protected bool errorVisible;
        protected AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress salesltCustomeraddress;

        protected IEnumerable<AdventureWorks.Server.Models.adventureworks.SalesltCustomer> salesltCustomersForCustomerId;

        protected IEnumerable<AdventureWorks.Server.Models.adventureworks.SalesltAddress> salesltAddressesForAddressId;


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

        protected int salesltAddressesForAddressIdCount;
        protected AdventureWorks.Server.Models.adventureworks.SalesltAddress salesltAddressesForAddressIdValue;
        protected async Task salesltAddressesForAddressIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await adventureworksService.GetSalesltAddresses(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(AddressLine1, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                salesltAddressesForAddressId = result.Value.AsODataEnumerable();
                salesltAddressesForAddressIdCount = result.Count;

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
                await adventureworksService.UpdateSalesltCustomeraddress(customerId:CustomerId, addressId:AddressId, salesltCustomeraddress);
                DialogService.Close(salesltCustomeraddress);
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