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
    public partial class EditSalesltProduct
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
        public int ProductId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            salesltProduct = await adventureworksService.GetSalesltProductByProductId(productId:ProductId);
        }
        protected bool errorVisible;
        protected AdventureWorks.Server.Models.adventureworks.SalesltProduct salesltProduct;

        protected IEnumerable<AdventureWorks.Server.Models.adventureworks.SalesltProductcategory> salesltProductCategoriesForProductCategoryId;

        protected IEnumerable<AdventureWorks.Server.Models.adventureworks.SalesltProductmodel> salesltProductModelsForProductModelId;


        protected int salesltProductCategoriesForProductCategoryIdCount;
        protected AdventureWorks.Server.Models.adventureworks.SalesltProductcategory salesltProductCategoriesForProductCategoryIdValue;
        protected async Task salesltProductCategoriesForProductCategoryIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await adventureworksService.GetSalesltProductCategories(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Name, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                salesltProductCategoriesForProductCategoryId = result.Value.AsODataEnumerable();
                salesltProductCategoriesForProductCategoryIdCount = result.Count;

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load ProductCategory" });
            }
        }

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
        protected async Task FormSubmit()
        {
            try
            {
                await adventureworksService.UpdateSalesltProduct(productId:ProductId, salesltProduct);
                DialogService.Close(salesltProduct);
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