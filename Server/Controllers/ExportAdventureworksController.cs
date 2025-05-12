using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using AdventureWorks.Server.Data;

namespace AdventureWorks.Server.Controllers
{
    public partial class ExportadventureworksController : ExportController
    {
        private readonly adventureworksContext context;
        private readonly adventureworksService service;

        public ExportadventureworksController(adventureworksContext context, adventureworksService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/adventureworks/salesltaddresses/csv")]
        [HttpGet("/export/adventureworks/salesltaddresses/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalesltAddressesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSalesltAddresses(), Request.Query, false), fileName);
        }

        [HttpGet("/export/adventureworks/salesltaddresses/excel")]
        [HttpGet("/export/adventureworks/salesltaddresses/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalesltAddressesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSalesltAddresses(), Request.Query, false), fileName);
        }

        [HttpGet("/export/adventureworks/buildversions/csv")]
        [HttpGet("/export/adventureworks/buildversions/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBuildVersionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetBuildVersions(), Request.Query, false), fileName);
        }

        [HttpGet("/export/adventureworks/buildversions/excel")]
        [HttpGet("/export/adventureworks/buildversions/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBuildVersionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetBuildVersions(), Request.Query, false), fileName);
        }

        [HttpGet("/export/adventureworks/salesltcustomers/csv")]
        [HttpGet("/export/adventureworks/salesltcustomers/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalesltCustomersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSalesltCustomers(), Request.Query, false), fileName);
        }

        [HttpGet("/export/adventureworks/salesltcustomers/excel")]
        [HttpGet("/export/adventureworks/salesltcustomers/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalesltCustomersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSalesltCustomers(), Request.Query, false), fileName);
        }

        [HttpGet("/export/adventureworks/salesltcustomeraddresses/csv")]
        [HttpGet("/export/adventureworks/salesltcustomeraddresses/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalesltCustomerAddressesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSalesltCustomerAddresses(), Request.Query, false), fileName);
        }

        [HttpGet("/export/adventureworks/salesltcustomeraddresses/excel")]
        [HttpGet("/export/adventureworks/salesltcustomeraddresses/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalesltCustomerAddressesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSalesltCustomerAddresses(), Request.Query, false), fileName);
        }

        [HttpGet("/export/adventureworks/errorlogs/csv")]
        [HttpGet("/export/adventureworks/errorlogs/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportErrorLogsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetErrorLogs(), Request.Query, false), fileName);
        }

        [HttpGet("/export/adventureworks/errorlogs/excel")]
        [HttpGet("/export/adventureworks/errorlogs/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportErrorLogsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetErrorLogs(), Request.Query, false), fileName);
        }

        [HttpGet("/export/adventureworks/salesltproducts/csv")]
        [HttpGet("/export/adventureworks/salesltproducts/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalesltProductsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSalesltProducts(), Request.Query, false), fileName);
        }

        [HttpGet("/export/adventureworks/salesltproducts/excel")]
        [HttpGet("/export/adventureworks/salesltproducts/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalesltProductsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSalesltProducts(), Request.Query, false), fileName);
        }

        [HttpGet("/export/adventureworks/salesltproductcategories/csv")]
        [HttpGet("/export/adventureworks/salesltproductcategories/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalesltProductCategoriesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSalesltProductCategories(), Request.Query, false), fileName);
        }

        [HttpGet("/export/adventureworks/salesltproductcategories/excel")]
        [HttpGet("/export/adventureworks/salesltproductcategories/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalesltProductCategoriesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSalesltProductCategories(), Request.Query, false), fileName);
        }

        [HttpGet("/export/adventureworks/salesltproductdescriptions/csv")]
        [HttpGet("/export/adventureworks/salesltproductdescriptions/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalesltProductDescriptionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSalesltProductDescriptions(), Request.Query, false), fileName);
        }

        [HttpGet("/export/adventureworks/salesltproductdescriptions/excel")]
        [HttpGet("/export/adventureworks/salesltproductdescriptions/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalesltProductDescriptionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSalesltProductDescriptions(), Request.Query, false), fileName);
        }

        [HttpGet("/export/adventureworks/salesltproductmodels/csv")]
        [HttpGet("/export/adventureworks/salesltproductmodels/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalesltProductModelsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSalesltProductModels(), Request.Query, false), fileName);
        }

        [HttpGet("/export/adventureworks/salesltproductmodels/excel")]
        [HttpGet("/export/adventureworks/salesltproductmodels/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalesltProductModelsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSalesltProductModels(), Request.Query, false), fileName);
        }

        [HttpGet("/export/adventureworks/salesltproductmodelproductdescriptions/csv")]
        [HttpGet("/export/adventureworks/salesltproductmodelproductdescriptions/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalesltProductModelProductDescriptionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSalesltProductModelProductDescriptions(), Request.Query, false), fileName);
        }

        [HttpGet("/export/adventureworks/salesltproductmodelproductdescriptions/excel")]
        [HttpGet("/export/adventureworks/salesltproductmodelproductdescriptions/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalesltProductModelProductDescriptionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSalesltProductModelProductDescriptions(), Request.Query, false), fileName);
        }

        [HttpGet("/export/adventureworks/salesltsalesorderdetails/csv")]
        [HttpGet("/export/adventureworks/salesltsalesorderdetails/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalesltSalesOrderDetailsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSalesltSalesOrderDetails(), Request.Query, false), fileName);
        }

        [HttpGet("/export/adventureworks/salesltsalesorderdetails/excel")]
        [HttpGet("/export/adventureworks/salesltsalesorderdetails/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalesltSalesOrderDetailsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSalesltSalesOrderDetails(), Request.Query, false), fileName);
        }

        [HttpGet("/export/adventureworks/salesltsalesorderheaders/csv")]
        [HttpGet("/export/adventureworks/salesltsalesorderheaders/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalesltSalesOrderHeadersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSalesltSalesOrderHeaders(), Request.Query, false), fileName);
        }

        [HttpGet("/export/adventureworks/salesltsalesorderheaders/excel")]
        [HttpGet("/export/adventureworks/salesltsalesorderheaders/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalesltSalesOrderHeadersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSalesltSalesOrderHeaders(), Request.Query, false), fileName);
        }
    }
}
