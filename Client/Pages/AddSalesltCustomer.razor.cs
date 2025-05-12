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
    public partial class AddSalesltCustomer
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
            salesltCustomer = new AdventureWorks.Server.Models.adventureworks.SalesltCustomer();
        }
        protected bool errorVisible;
        protected AdventureWorks.Server.Models.adventureworks.SalesltCustomer salesltCustomer;

        protected async Task FormSubmit()
        {
            try
            {
                await adventureworksService.CreateSalesltCustomer(salesltCustomer);
                DialogService.Close(salesltCustomer);
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