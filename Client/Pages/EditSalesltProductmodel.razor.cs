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
    public partial class EditSalesltProductmodel
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
        public int ProductModelId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            salesltProductmodel = await adventureworksService.GetSalesltProductmodelByProductModelId(productModelId:ProductModelId);
        }
        protected bool errorVisible;
        protected AdventureWorks.Server.Models.adventureworks.SalesltProductmodel salesltProductmodel;

        protected async Task FormSubmit()
        {
            try
            {
                await adventureworksService.UpdateSalesltProductmodel(productModelId:ProductModelId, salesltProductmodel);
                DialogService.Close(salesltProductmodel);
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