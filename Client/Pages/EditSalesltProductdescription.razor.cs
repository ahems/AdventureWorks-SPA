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
    public partial class EditSalesltProductdescription
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
        public int ProductDescriptionId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            salesltProductdescription = await adventureworksService.GetSalesltProductdescriptionByProductDescriptionId(productDescriptionId:ProductDescriptionId);
        }
        protected bool errorVisible;
        protected AdventureWorks.Server.Models.adventureworks.SalesltProductdescription salesltProductdescription;

        protected async Task FormSubmit()
        {
            try
            {
                await adventureworksService.UpdateSalesltProductdescription(productDescriptionId:ProductDescriptionId, salesltProductdescription);
                DialogService.Close(salesltProductdescription);
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