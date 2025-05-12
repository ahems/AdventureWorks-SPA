
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Radzen;

namespace AdventureWorks.Client
{
    public partial class adventureworksService
    {
        private readonly HttpClient httpClient;
        private readonly Uri baseUri;
        private readonly NavigationManager navigationManager;

        public adventureworksService(NavigationManager navigationManager, HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;

            this.navigationManager = navigationManager;
            this.baseUri = new Uri($"{navigationManager.BaseUri}odata/adventureworks/");
        }


        public async System.Threading.Tasks.Task ExportSalesltAddressesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltaddresses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltaddresses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSalesltAddressesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltaddresses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltaddresses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSalesltAddresses(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltAddress>> GetSalesltAddresses(Query query)
        {
            return await GetSalesltAddresses(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltAddress>> GetSalesltAddresses(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SalesltAddresses");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalesltAddresses(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltAddress>>(response);
        }

        partial void OnCreateSalesltAddress(HttpRequestMessage requestMessage);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltAddress> CreateSalesltAddress(AdventureWorks.Server.Models.adventureworks.SalesltAddress salesltAddress = default(AdventureWorks.Server.Models.adventureworks.SalesltAddress))
        {
            var uri = new Uri(baseUri, $"SalesltAddresses");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salesltAddress), Encoding.UTF8, "application/json");

            OnCreateSalesltAddress(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AdventureWorks.Server.Models.adventureworks.SalesltAddress>(response);
        }

        partial void OnDeleteSalesltAddress(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSalesltAddress(int addressId = default(int))
        {
            var uri = new Uri(baseUri, $"SalesltAddresses({addressId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSalesltAddress(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSalesltAddressByAddressId(HttpRequestMessage requestMessage);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltAddress> GetSalesltAddressByAddressId(string expand = default(string), int addressId = default(int))
        {
            var uri = new Uri(baseUri, $"SalesltAddresses({addressId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalesltAddressByAddressId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AdventureWorks.Server.Models.adventureworks.SalesltAddress>(response);
        }

        partial void OnUpdateSalesltAddress(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSalesltAddress(int addressId = default(int), AdventureWorks.Server.Models.adventureworks.SalesltAddress salesltAddress = default(AdventureWorks.Server.Models.adventureworks.SalesltAddress))
        {
            var uri = new Uri(baseUri, $"SalesltAddresses({addressId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salesltAddress), Encoding.UTF8, "application/json");

            OnUpdateSalesltAddress(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportBuildVersionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/buildversions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/buildversions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportBuildVersionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/buildversions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/buildversions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetBuildVersions(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.BuildVersion>> GetBuildVersions(Query query)
        {
            return await GetBuildVersions(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.BuildVersion>> GetBuildVersions(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"BuildVersions");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetBuildVersions(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.BuildVersion>>(response);
        }

        partial void OnCreateBuildVersion(HttpRequestMessage requestMessage);

        public async Task<AdventureWorks.Server.Models.adventureworks.BuildVersion> CreateBuildVersion(AdventureWorks.Server.Models.adventureworks.BuildVersion buildVersion = default(AdventureWorks.Server.Models.adventureworks.BuildVersion))
        {
            var uri = new Uri(baseUri, $"BuildVersions");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(buildVersion), Encoding.UTF8, "application/json");

            OnCreateBuildVersion(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AdventureWorks.Server.Models.adventureworks.BuildVersion>(response);
        }

        partial void OnDeleteBuildVersion(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteBuildVersion(byte systemInformationId = default(byte))
        {
            var uri = new Uri(baseUri, $"BuildVersions({systemInformationId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteBuildVersion(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetBuildVersionBySystemInformationId(HttpRequestMessage requestMessage);

        public async Task<AdventureWorks.Server.Models.adventureworks.BuildVersion> GetBuildVersionBySystemInformationId(string expand = default(string), byte systemInformationId = default(byte))
        {
            var uri = new Uri(baseUri, $"BuildVersions({systemInformationId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetBuildVersionBySystemInformationId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AdventureWorks.Server.Models.adventureworks.BuildVersion>(response);
        }

        partial void OnUpdateBuildVersion(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateBuildVersion(byte systemInformationId = default(byte), AdventureWorks.Server.Models.adventureworks.BuildVersion buildVersion = default(AdventureWorks.Server.Models.adventureworks.BuildVersion))
        {
            var uri = new Uri(baseUri, $"BuildVersions({systemInformationId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(buildVersion), Encoding.UTF8, "application/json");

            OnUpdateBuildVersion(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSalesltCustomersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltcustomers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltcustomers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSalesltCustomersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltcustomers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltcustomers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSalesltCustomers(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltCustomer>> GetSalesltCustomers(Query query)
        {
            return await GetSalesltCustomers(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltCustomer>> GetSalesltCustomers(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SalesltCustomers");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalesltCustomers(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltCustomer>>(response);
        }

        partial void OnCreateSalesltCustomer(HttpRequestMessage requestMessage);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltCustomer> CreateSalesltCustomer(AdventureWorks.Server.Models.adventureworks.SalesltCustomer salesltCustomer = default(AdventureWorks.Server.Models.adventureworks.SalesltCustomer))
        {
            var uri = new Uri(baseUri, $"SalesltCustomers");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salesltCustomer), Encoding.UTF8, "application/json");

            OnCreateSalesltCustomer(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AdventureWorks.Server.Models.adventureworks.SalesltCustomer>(response);
        }

        partial void OnDeleteSalesltCustomer(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSalesltCustomer(int customerId = default(int))
        {
            var uri = new Uri(baseUri, $"SalesltCustomers({customerId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSalesltCustomer(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSalesltCustomerByCustomerId(HttpRequestMessage requestMessage);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltCustomer> GetSalesltCustomerByCustomerId(string expand = default(string), int customerId = default(int))
        {
            var uri = new Uri(baseUri, $"SalesltCustomers({customerId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalesltCustomerByCustomerId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AdventureWorks.Server.Models.adventureworks.SalesltCustomer>(response);
        }

        partial void OnUpdateSalesltCustomer(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSalesltCustomer(int customerId = default(int), AdventureWorks.Server.Models.adventureworks.SalesltCustomer salesltCustomer = default(AdventureWorks.Server.Models.adventureworks.SalesltCustomer))
        {
            var uri = new Uri(baseUri, $"SalesltCustomers({customerId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salesltCustomer), Encoding.UTF8, "application/json");

            OnUpdateSalesltCustomer(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSalesltCustomerAddressesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltcustomeraddresses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltcustomeraddresses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSalesltCustomerAddressesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltcustomeraddresses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltcustomeraddresses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSalesltCustomerAddresses(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress>> GetSalesltCustomerAddresses(Query query)
        {
            return await GetSalesltCustomerAddresses(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress>> GetSalesltCustomerAddresses(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SalesltCustomerAddresses");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalesltCustomerAddresses(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress>>(response);
        }

        partial void OnCreateSalesltCustomeraddress(HttpRequestMessage requestMessage);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress> CreateSalesltCustomeraddress(AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress salesltCustomeraddress = default(AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress))
        {
            var uri = new Uri(baseUri, $"SalesltCustomerAddresses");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salesltCustomeraddress), Encoding.UTF8, "application/json");

            OnCreateSalesltCustomeraddress(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress>(response);
        }

        partial void OnDeleteSalesltCustomeraddress(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSalesltCustomeraddress(int customerId = default(int), int addressId = default(int))
        {
            var uri = new Uri(baseUri, $"SalesltCustomerAddresses(CustomerId={customerId},AddressId={addressId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSalesltCustomeraddress(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSalesltCustomeraddressByCustomerIdAndAddressId(HttpRequestMessage requestMessage);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress> GetSalesltCustomeraddressByCustomerIdAndAddressId(string expand = default(string), int customerId = default(int), int addressId = default(int))
        {
            var uri = new Uri(baseUri, $"SalesltCustomerAddresses(CustomerId={customerId},AddressId={addressId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalesltCustomeraddressByCustomerIdAndAddressId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress>(response);
        }

        partial void OnUpdateSalesltCustomeraddress(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSalesltCustomeraddress(int customerId = default(int), int addressId = default(int), AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress salesltCustomeraddress = default(AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress))
        {
            var uri = new Uri(baseUri, $"SalesltCustomerAddresses(CustomerId={customerId},AddressId={addressId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salesltCustomeraddress), Encoding.UTF8, "application/json");

            OnUpdateSalesltCustomeraddress(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportErrorLogsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/errorlogs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/errorlogs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportErrorLogsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/errorlogs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/errorlogs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetErrorLogs(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.ErrorLog>> GetErrorLogs(Query query)
        {
            return await GetErrorLogs(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.ErrorLog>> GetErrorLogs(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"ErrorLogs");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetErrorLogs(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.ErrorLog>>(response);
        }

        partial void OnCreateErrorLog(HttpRequestMessage requestMessage);

        public async Task<AdventureWorks.Server.Models.adventureworks.ErrorLog> CreateErrorLog(AdventureWorks.Server.Models.adventureworks.ErrorLog errorLog = default(AdventureWorks.Server.Models.adventureworks.ErrorLog))
        {
            var uri = new Uri(baseUri, $"ErrorLogs");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(errorLog), Encoding.UTF8, "application/json");

            OnCreateErrorLog(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AdventureWorks.Server.Models.adventureworks.ErrorLog>(response);
        }

        partial void OnDeleteErrorLog(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteErrorLog(int errorLogId = default(int))
        {
            var uri = new Uri(baseUri, $"ErrorLogs({errorLogId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteErrorLog(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetErrorLogByErrorLogId(HttpRequestMessage requestMessage);

        public async Task<AdventureWorks.Server.Models.adventureworks.ErrorLog> GetErrorLogByErrorLogId(string expand = default(string), int errorLogId = default(int))
        {
            var uri = new Uri(baseUri, $"ErrorLogs({errorLogId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetErrorLogByErrorLogId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AdventureWorks.Server.Models.adventureworks.ErrorLog>(response);
        }

        partial void OnUpdateErrorLog(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateErrorLog(int errorLogId = default(int), AdventureWorks.Server.Models.adventureworks.ErrorLog errorLog = default(AdventureWorks.Server.Models.adventureworks.ErrorLog))
        {
            var uri = new Uri(baseUri, $"ErrorLogs({errorLogId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(errorLog), Encoding.UTF8, "application/json");

            OnUpdateErrorLog(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSalesltProductsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltproducts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltproducts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSalesltProductsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltproducts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltproducts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSalesltProducts(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltProduct>> GetSalesltProducts(Query query)
        {
            return await GetSalesltProducts(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltProduct>> GetSalesltProducts(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SalesltProducts");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalesltProducts(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltProduct>>(response);
        }

        partial void OnCreateSalesltProduct(HttpRequestMessage requestMessage);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProduct> CreateSalesltProduct(AdventureWorks.Server.Models.adventureworks.SalesltProduct salesltProduct = default(AdventureWorks.Server.Models.adventureworks.SalesltProduct))
        {
            var uri = new Uri(baseUri, $"SalesltProducts");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salesltProduct), Encoding.UTF8, "application/json");

            OnCreateSalesltProduct(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AdventureWorks.Server.Models.adventureworks.SalesltProduct>(response);
        }

        partial void OnDeleteSalesltProduct(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSalesltProduct(int productId = default(int))
        {
            var uri = new Uri(baseUri, $"SalesltProducts({productId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSalesltProduct(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSalesltProductByProductId(HttpRequestMessage requestMessage);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProduct> GetSalesltProductByProductId(string expand = default(string), int productId = default(int))
        {
            var uri = new Uri(baseUri, $"SalesltProducts({productId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalesltProductByProductId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AdventureWorks.Server.Models.adventureworks.SalesltProduct>(response);
        }

        partial void OnUpdateSalesltProduct(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSalesltProduct(int productId = default(int), AdventureWorks.Server.Models.adventureworks.SalesltProduct salesltProduct = default(AdventureWorks.Server.Models.adventureworks.SalesltProduct))
        {
            var uri = new Uri(baseUri, $"SalesltProducts({productId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salesltProduct), Encoding.UTF8, "application/json");

            OnUpdateSalesltProduct(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSalesltProductCategoriesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltproductcategories/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltproductcategories/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSalesltProductCategoriesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltproductcategories/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltproductcategories/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSalesltProductCategories(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltProductcategory>> GetSalesltProductCategories(Query query)
        {
            return await GetSalesltProductCategories(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltProductcategory>> GetSalesltProductCategories(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SalesltProductCategories");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalesltProductCategories(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltProductcategory>>(response);
        }

        partial void OnCreateSalesltProductcategory(HttpRequestMessage requestMessage);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProductcategory> CreateSalesltProductcategory(AdventureWorks.Server.Models.adventureworks.SalesltProductcategory salesltProductcategory = default(AdventureWorks.Server.Models.adventureworks.SalesltProductcategory))
        {
            var uri = new Uri(baseUri, $"SalesltProductCategories");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salesltProductcategory), Encoding.UTF8, "application/json");

            OnCreateSalesltProductcategory(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AdventureWorks.Server.Models.adventureworks.SalesltProductcategory>(response);
        }

        partial void OnDeleteSalesltProductcategory(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSalesltProductcategory(int productCategoryId = default(int))
        {
            var uri = new Uri(baseUri, $"SalesltProductCategories({productCategoryId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSalesltProductcategory(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSalesltProductcategoryByProductCategoryId(HttpRequestMessage requestMessage);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProductcategory> GetSalesltProductcategoryByProductCategoryId(string expand = default(string), int productCategoryId = default(int))
        {
            var uri = new Uri(baseUri, $"SalesltProductCategories({productCategoryId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalesltProductcategoryByProductCategoryId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AdventureWorks.Server.Models.adventureworks.SalesltProductcategory>(response);
        }

        partial void OnUpdateSalesltProductcategory(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSalesltProductcategory(int productCategoryId = default(int), AdventureWorks.Server.Models.adventureworks.SalesltProductcategory salesltProductcategory = default(AdventureWorks.Server.Models.adventureworks.SalesltProductcategory))
        {
            var uri = new Uri(baseUri, $"SalesltProductCategories({productCategoryId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salesltProductcategory), Encoding.UTF8, "application/json");

            OnUpdateSalesltProductcategory(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSalesltProductDescriptionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltproductdescriptions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltproductdescriptions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSalesltProductDescriptionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltproductdescriptions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltproductdescriptions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSalesltProductDescriptions(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltProductdescription>> GetSalesltProductDescriptions(Query query)
        {
            return await GetSalesltProductDescriptions(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltProductdescription>> GetSalesltProductDescriptions(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SalesltProductDescriptions");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalesltProductDescriptions(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltProductdescription>>(response);
        }

        partial void OnCreateSalesltProductdescription(HttpRequestMessage requestMessage);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProductdescription> CreateSalesltProductdescription(AdventureWorks.Server.Models.adventureworks.SalesltProductdescription salesltProductdescription = default(AdventureWorks.Server.Models.adventureworks.SalesltProductdescription))
        {
            var uri = new Uri(baseUri, $"SalesltProductDescriptions");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salesltProductdescription), Encoding.UTF8, "application/json");

            OnCreateSalesltProductdescription(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AdventureWorks.Server.Models.adventureworks.SalesltProductdescription>(response);
        }

        partial void OnDeleteSalesltProductdescription(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSalesltProductdescription(int productDescriptionId = default(int))
        {
            var uri = new Uri(baseUri, $"SalesltProductDescriptions({productDescriptionId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSalesltProductdescription(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSalesltProductdescriptionByProductDescriptionId(HttpRequestMessage requestMessage);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProductdescription> GetSalesltProductdescriptionByProductDescriptionId(string expand = default(string), int productDescriptionId = default(int))
        {
            var uri = new Uri(baseUri, $"SalesltProductDescriptions({productDescriptionId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalesltProductdescriptionByProductDescriptionId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AdventureWorks.Server.Models.adventureworks.SalesltProductdescription>(response);
        }

        partial void OnUpdateSalesltProductdescription(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSalesltProductdescription(int productDescriptionId = default(int), AdventureWorks.Server.Models.adventureworks.SalesltProductdescription salesltProductdescription = default(AdventureWorks.Server.Models.adventureworks.SalesltProductdescription))
        {
            var uri = new Uri(baseUri, $"SalesltProductDescriptions({productDescriptionId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salesltProductdescription), Encoding.UTF8, "application/json");

            OnUpdateSalesltProductdescription(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSalesltProductModelsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltproductmodels/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltproductmodels/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSalesltProductModelsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltproductmodels/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltproductmodels/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSalesltProductModels(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltProductmodel>> GetSalesltProductModels(Query query)
        {
            return await GetSalesltProductModels(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltProductmodel>> GetSalesltProductModels(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SalesltProductModels");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalesltProductModels(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltProductmodel>>(response);
        }

        partial void OnCreateSalesltProductmodel(HttpRequestMessage requestMessage);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProductmodel> CreateSalesltProductmodel(AdventureWorks.Server.Models.adventureworks.SalesltProductmodel salesltProductmodel = default(AdventureWorks.Server.Models.adventureworks.SalesltProductmodel))
        {
            var uri = new Uri(baseUri, $"SalesltProductModels");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salesltProductmodel), Encoding.UTF8, "application/json");

            OnCreateSalesltProductmodel(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AdventureWorks.Server.Models.adventureworks.SalesltProductmodel>(response);
        }

        partial void OnDeleteSalesltProductmodel(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSalesltProductmodel(int productModelId = default(int))
        {
            var uri = new Uri(baseUri, $"SalesltProductModels({productModelId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSalesltProductmodel(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSalesltProductmodelByProductModelId(HttpRequestMessage requestMessage);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProductmodel> GetSalesltProductmodelByProductModelId(string expand = default(string), int productModelId = default(int))
        {
            var uri = new Uri(baseUri, $"SalesltProductModels({productModelId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalesltProductmodelByProductModelId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AdventureWorks.Server.Models.adventureworks.SalesltProductmodel>(response);
        }

        partial void OnUpdateSalesltProductmodel(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSalesltProductmodel(int productModelId = default(int), AdventureWorks.Server.Models.adventureworks.SalesltProductmodel salesltProductmodel = default(AdventureWorks.Server.Models.adventureworks.SalesltProductmodel))
        {
            var uri = new Uri(baseUri, $"SalesltProductModels({productModelId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salesltProductmodel), Encoding.UTF8, "application/json");

            OnUpdateSalesltProductmodel(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSalesltProductModelProductDescriptionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltproductmodelproductdescriptions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltproductmodelproductdescriptions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSalesltProductModelProductDescriptionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltproductmodelproductdescriptions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltproductmodelproductdescriptions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSalesltProductModelProductDescriptions(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription>> GetSalesltProductModelProductDescriptions(Query query)
        {
            return await GetSalesltProductModelProductDescriptions(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription>> GetSalesltProductModelProductDescriptions(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SalesltProductModelProductDescriptions");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalesltProductModelProductDescriptions(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription>>(response);
        }

        partial void OnCreateSalesltProductmodelproductdescription(HttpRequestMessage requestMessage);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription> CreateSalesltProductmodelproductdescription(AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription salesltProductmodelproductdescription = default(AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription))
        {
            var uri = new Uri(baseUri, $"SalesltProductModelProductDescriptions");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salesltProductmodelproductdescription), Encoding.UTF8, "application/json");

            OnCreateSalesltProductmodelproductdescription(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription>(response);
        }

        partial void OnDeleteSalesltProductmodelproductdescription(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSalesltProductmodelproductdescription(int productModelId = default(int), int productDescriptionId = default(int), string culture = default(string))
        {
            var uri = new Uri(baseUri, $"SalesltProductModelProductDescriptions(ProductModelId={productModelId},ProductDescriptionId={productDescriptionId},Culture='{Uri.EscapeDataString(culture.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSalesltProductmodelproductdescription(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSalesltProductmodelproductdescriptionByProductModelIdAndProductDescriptionIdAndCulture(HttpRequestMessage requestMessage);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription> GetSalesltProductmodelproductdescriptionByProductModelIdAndProductDescriptionIdAndCulture(string expand = default(string), int productModelId = default(int), int productDescriptionId = default(int), string culture = default(string))
        {
            var uri = new Uri(baseUri, $"SalesltProductModelProductDescriptions(ProductModelId={productModelId},ProductDescriptionId={productDescriptionId},Culture='{Uri.EscapeDataString(culture.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalesltProductmodelproductdescriptionByProductModelIdAndProductDescriptionIdAndCulture(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription>(response);
        }

        partial void OnUpdateSalesltProductmodelproductdescription(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSalesltProductmodelproductdescription(int productModelId = default(int), int productDescriptionId = default(int), string culture = default(string), AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription salesltProductmodelproductdescription = default(AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription))
        {
            var uri = new Uri(baseUri, $"SalesltProductModelProductDescriptions(ProductModelId={productModelId},ProductDescriptionId={productDescriptionId},Culture='{Uri.EscapeDataString(culture.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salesltProductmodelproductdescription), Encoding.UTF8, "application/json");

            OnUpdateSalesltProductmodelproductdescription(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSalesltSalesOrderDetailsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltsalesorderdetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltsalesorderdetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSalesltSalesOrderDetailsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltsalesorderdetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltsalesorderdetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSalesltSalesOrderDetails(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail>> GetSalesltSalesOrderDetails(Query query)
        {
            return await GetSalesltSalesOrderDetails(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail>> GetSalesltSalesOrderDetails(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SalesltSalesOrderDetails");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalesltSalesOrderDetails(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail>>(response);
        }

        partial void OnCreateSalesltSalesorderdetail(HttpRequestMessage requestMessage);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail> CreateSalesltSalesorderdetail(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail salesltSalesorderdetail = default(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail))
        {
            var uri = new Uri(baseUri, $"SalesltSalesOrderDetails");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salesltSalesorderdetail), Encoding.UTF8, "application/json");

            OnCreateSalesltSalesorderdetail(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail>(response);
        }

        partial void OnDeleteSalesltSalesorderdetail(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSalesltSalesorderdetail(int salesOrderId = default(int), int salesOrderDetailId = default(int))
        {
            var uri = new Uri(baseUri, $"SalesltSalesOrderDetails(SalesOrderId={salesOrderId},SalesOrderDetailId={salesOrderDetailId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSalesltSalesorderdetail(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSalesltSalesorderdetailBySalesOrderIdAndSalesOrderDetailId(HttpRequestMessage requestMessage);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail> GetSalesltSalesorderdetailBySalesOrderIdAndSalesOrderDetailId(string expand = default(string), int salesOrderId = default(int), int salesOrderDetailId = default(int))
        {
            var uri = new Uri(baseUri, $"SalesltSalesOrderDetails(SalesOrderId={salesOrderId},SalesOrderDetailId={salesOrderDetailId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalesltSalesorderdetailBySalesOrderIdAndSalesOrderDetailId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail>(response);
        }

        partial void OnUpdateSalesltSalesorderdetail(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSalesltSalesorderdetail(int salesOrderId = default(int), int salesOrderDetailId = default(int), AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail salesltSalesorderdetail = default(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail))
        {
            var uri = new Uri(baseUri, $"SalesltSalesOrderDetails(SalesOrderId={salesOrderId},SalesOrderDetailId={salesOrderDetailId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salesltSalesorderdetail), Encoding.UTF8, "application/json");

            OnUpdateSalesltSalesorderdetail(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSalesltSalesOrderHeadersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltsalesorderheaders/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltsalesorderheaders/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSalesltSalesOrderHeadersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltsalesorderheaders/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltsalesorderheaders/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSalesltSalesOrderHeaders(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader>> GetSalesltSalesOrderHeaders(Query query)
        {
            return await GetSalesltSalesOrderHeaders(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader>> GetSalesltSalesOrderHeaders(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SalesltSalesOrderHeaders");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalesltSalesOrderHeaders(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader>>(response);
        }

        partial void OnCreateSalesltSalesorderheader(HttpRequestMessage requestMessage);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader> CreateSalesltSalesorderheader(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader salesltSalesorderheader = default(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader))
        {
            var uri = new Uri(baseUri, $"SalesltSalesOrderHeaders");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salesltSalesorderheader), Encoding.UTF8, "application/json");

            OnCreateSalesltSalesorderheader(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader>(response);
        }

        partial void OnDeleteSalesltSalesorderheader(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSalesltSalesorderheader(int salesOrderId = default(int))
        {
            var uri = new Uri(baseUri, $"SalesltSalesOrderHeaders({salesOrderId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSalesltSalesorderheader(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSalesltSalesorderheaderBySalesOrderId(HttpRequestMessage requestMessage);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader> GetSalesltSalesorderheaderBySalesOrderId(string expand = default(string), int salesOrderId = default(int))
        {
            var uri = new Uri(baseUri, $"SalesltSalesOrderHeaders({salesOrderId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSalesltSalesorderheaderBySalesOrderId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader>(response);
        }

        partial void OnUpdateSalesltSalesorderheader(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSalesltSalesorderheader(int salesOrderId = default(int), AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader salesltSalesorderheader = default(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader))
        {
            var uri = new Uri(baseUri, $"SalesltSalesOrderHeaders({salesOrderId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(salesltSalesorderheader), Encoding.UTF8, "application/json");

            OnUpdateSalesltSalesorderheader(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
    }
}