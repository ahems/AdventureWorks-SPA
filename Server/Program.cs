using Radzen;
using AdventureWorks.Server.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.OData;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveWebAssemblyComponents();
builder.Services.AddControllers();
builder.Services.AddRadzenComponents();
builder.Services.AddRadzenCookieThemeService(options =>
{
    options.Name = "AdventureWorksTheme";
    options.Duration = TimeSpan.FromDays(365);
});
builder.Services.AddHttpClient();
builder.Services.AddScoped<AdventureWorks.Server.adventureworksService>();
builder.Services.AddDbContext<AdventureWorks.Server.Data.adventureworksContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("adventureworksConnection"));
});
builder.Services.AddControllers().AddOData(opt =>
{
    var oDataBuilderadventureworks = new ODataConventionModelBuilder();
    oDataBuilderadventureworks.EntitySet<AdventureWorks.Server.Models.adventureworks.SalesltAddress>("SalesltAddresses");
    oDataBuilderadventureworks.EntitySet<AdventureWorks.Server.Models.adventureworks.BuildVersion>("BuildVersions");
    oDataBuilderadventureworks.EntitySet<AdventureWorks.Server.Models.adventureworks.SalesltCustomer>("SalesltCustomers");
    oDataBuilderadventureworks.EntitySet<AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress>("SalesltCustomerAddresses").EntityType.HasKey(entity => new { entity.CustomerId, entity.AddressId });
    oDataBuilderadventureworks.EntitySet<AdventureWorks.Server.Models.adventureworks.ErrorLog>("ErrorLogs");
    oDataBuilderadventureworks.EntitySet<AdventureWorks.Server.Models.adventureworks.SalesltProduct>("SalesltProducts");
    oDataBuilderadventureworks.EntitySet<AdventureWorks.Server.Models.adventureworks.SalesltProductcategory>("SalesltProductCategories");
    oDataBuilderadventureworks.EntitySet<AdventureWorks.Server.Models.adventureworks.SalesltProductdescription>("SalesltProductDescriptions");
    oDataBuilderadventureworks.EntitySet<AdventureWorks.Server.Models.adventureworks.SalesltProductmodel>("SalesltProductModels");
    oDataBuilderadventureworks.EntitySet<AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription>("SalesltProductModelProductDescriptions").EntityType.HasKey(entity => new { entity.ProductModelId, entity.ProductDescriptionId, entity.Culture });
    oDataBuilderadventureworks.EntitySet<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail>("SalesltSalesOrderDetails").EntityType.HasKey(entity => new { entity.SalesOrderId, entity.SalesOrderDetailId });
    oDataBuilderadventureworks.EntitySet<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader>("SalesltSalesOrderHeaders");
    opt.AddRouteComponents("odata/adventureworks", oDataBuilderadventureworks.GetEdmModel()).Count().Filter().OrderBy().Expand().Select().SetMaxTop(null).TimeZone = TimeZoneInfo.Utc;
});
builder.Services.AddScoped<AdventureWorks.Client.adventureworksService>();
var app = builder.Build();
var forwardingOptions = new ForwardedHeadersOptions()
{
    ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
};
forwardingOptions.KnownNetworks.Clear();
forwardingOptions.KnownProxies.Clear();
app.UseForwardedHeaders(forwardingOptions);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>().AddInteractiveWebAssemblyRenderMode().AddAdditionalAssemblies(typeof(AdventureWorks.Client._Imports).Assembly);
app.Run();