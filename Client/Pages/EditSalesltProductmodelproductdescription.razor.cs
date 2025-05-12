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
    public partial class EditSalesltProductmodelproductdescription
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

        [Parameter]
        public int ProductDescriptionId { get; set; }

        [Parameter]
        public string Culture { get; set; }

        protected override async Task OnInitializedAsync()
        {
            salesltProductmodelproductdescription = await adventureworksService.GetSalesltProductmodelproductdescriptionByProductModelIdAndProductDescriptionIdAndCulture(productModelId:ProductModelId, productDescriptionId:ProductDescriptionId, culture:Culture);
        }
        protected bool errorVisible;
        protected AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription salesltProductmodelproductdescription;

        protected IEnumerable<AdventureWorks.Server.Models.adventureworks.SalesltProductmodel> salesltProductModelsForProductModelId;

        protected IEnumerable<AdventureWorks.Server.Models.adventureworks.SalesltProductdescription> salesltProductDescriptionsForProductDescriptionId;


        protected int salesltProductModelsForProductModelIdCount;
        protected AdventureWorks.Server.Models.adventureworks.SalesltProductmodel salesltProductModelsForProductModelIdValue;
        protected async Task salesltProductModelsForProductModelIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await adventureworksService.GetSalesltProductModels(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Name, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                salesltProductModelsForProductModelId = result.Value.AsODataEnumerable();
                salesltProductModelsForProductModelIdCount = result.Count;

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load ProductModel" });
            }
        }

        protected int salesltProductDescriptionsForProductDescriptionIdCount;
        protected AdventureWorks.Server.Models.adventureworks.SalesltProductdescription salesltProductDescriptionsForProductDescriptionIdValue;
        protected async Task salesltProductDescriptionsForProductDescriptionIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await adventureworksService.GetSalesltProductDescriptions(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Description, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                salesltProductDescriptionsForProductDescriptionId = result.Value.AsODataEnumerable();
                salesltProductDescriptionsForProductDescriptionIdCount = result.Count;

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load ProductDescription" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                await adventureworksService.UpdateSalesltProductmodelproductdescription(productModelId:ProductModelId, productDescriptionId:ProductDescriptionId, culture:Culture, salesltProductmodelproductdescription);
                DialogService.Close(salesltProductmodelproductdescription);
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