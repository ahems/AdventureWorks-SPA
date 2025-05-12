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
    public partial class AddSalesltProductcategory
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
            salesltProductcategory = new AdventureWorks.Server.Models.adventureworks.SalesltProductcategory();
        }
        protected bool errorVisible;
        protected AdventureWorks.Server.Models.adventureworks.SalesltProductcategory salesltProductcategory;

        protected IEnumerable<AdventureWorks.Server.Models.adventureworks.SalesltProductcategory> salesltProductCategoriesForParentProductCategoryId;


        protected int salesltProductCategoriesForParentProductCategoryIdCount;
        protected AdventureWorks.Server.Models.adventureworks.SalesltProductcategory salesltProductCategoriesForParentProductCategoryIdValue;
        protected async Task salesltProductCategoriesForParentProductCategoryIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await adventureworksService.GetSalesltProductCategories(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Name, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                salesltProductCategoriesForParentProductCategoryId = result.Value.AsODataEnumerable();
                salesltProductCategoriesForParentProductCategoryIdCount = result.Count;

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load ProductCategory" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                await adventureworksService.CreateSalesltProductcategory(salesltProductcategory);
                DialogService.Close(salesltProductcategory);
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