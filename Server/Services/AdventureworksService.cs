using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Radzen;

using AdventureWorks.Server.Data;

namespace AdventureWorks.Server
{
    public partial class adventureworksService
    {
        adventureworksContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly adventureworksContext context;
        private readonly NavigationManager navigationManager;

        public adventureworksService(adventureworksContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public void ApplyQuery<T>(ref IQueryable<T> items, Query query = null)
        {
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }
        }


        public async Task ExportSalesltAddressesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltaddresses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltaddresses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSalesltAddressesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltaddresses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltaddresses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSalesltAddressesRead(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltAddress> items);

        public async Task<IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltAddress>> GetSalesltAddresses(Query query = null)
        {
            var items = Context.SalesltAddresses.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnSalesltAddressesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSalesltAddressGet(AdventureWorks.Server.Models.adventureworks.SalesltAddress item);
        partial void OnGetSalesltAddressByAddressId(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltAddress> items);


        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltAddress> GetSalesltAddressByAddressId(int addressid)
        {
            var items = Context.SalesltAddresses
                              .AsNoTracking()
                              .Where(i => i.AddressId == addressid);

 
            OnGetSalesltAddressByAddressId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSalesltAddressGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSalesltAddressCreated(AdventureWorks.Server.Models.adventureworks.SalesltAddress item);
        partial void OnAfterSalesltAddressCreated(AdventureWorks.Server.Models.adventureworks.SalesltAddress item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltAddress> CreateSalesltAddress(AdventureWorks.Server.Models.adventureworks.SalesltAddress salesltaddress)
        {
            OnSalesltAddressCreated(salesltaddress);

            var existingItem = Context.SalesltAddresses
                              .Where(i => i.AddressId == salesltaddress.AddressId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SalesltAddresses.Add(salesltaddress);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(salesltaddress).State = EntityState.Detached;
                throw;
            }

            OnAfterSalesltAddressCreated(salesltaddress);

            return salesltaddress;
        }

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltAddress> CancelSalesltAddressChanges(AdventureWorks.Server.Models.adventureworks.SalesltAddress item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSalesltAddressUpdated(AdventureWorks.Server.Models.adventureworks.SalesltAddress item);
        partial void OnAfterSalesltAddressUpdated(AdventureWorks.Server.Models.adventureworks.SalesltAddress item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltAddress> UpdateSalesltAddress(int addressid, AdventureWorks.Server.Models.adventureworks.SalesltAddress salesltaddress)
        {
            OnSalesltAddressUpdated(salesltaddress);

            var itemToUpdate = Context.SalesltAddresses
                              .Where(i => i.AddressId == salesltaddress.AddressId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(salesltaddress);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSalesltAddressUpdated(salesltaddress);

            return salesltaddress;
        }

        partial void OnSalesltAddressDeleted(AdventureWorks.Server.Models.adventureworks.SalesltAddress item);
        partial void OnAfterSalesltAddressDeleted(AdventureWorks.Server.Models.adventureworks.SalesltAddress item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltAddress> DeleteSalesltAddress(int addressid)
        {
            var itemToDelete = Context.SalesltAddresses
                              .Where(i => i.AddressId == addressid)
                              .Include(i => i.SalesltCustomerAddresses)
                              .Include(i => i.SalesltSalesOrderHeaders)
                              .Include(i => i.SalesltSalesOrderHeaders1)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSalesltAddressDeleted(itemToDelete);


            Context.SalesltAddresses.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSalesltAddressDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportBuildVersionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/buildversions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/buildversions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportBuildVersionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/buildversions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/buildversions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnBuildVersionsRead(ref IQueryable<AdventureWorks.Server.Models.adventureworks.BuildVersion> items);

        public async Task<IQueryable<AdventureWorks.Server.Models.adventureworks.BuildVersion>> GetBuildVersions(Query query = null)
        {
            var items = Context.BuildVersions.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnBuildVersionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnBuildVersionGet(AdventureWorks.Server.Models.adventureworks.BuildVersion item);
        partial void OnGetBuildVersionBySystemInformationId(ref IQueryable<AdventureWorks.Server.Models.adventureworks.BuildVersion> items);


        public async Task<AdventureWorks.Server.Models.adventureworks.BuildVersion> GetBuildVersionBySystemInformationId(byte systeminformationid)
        {
            var items = Context.BuildVersions
                              .AsNoTracking()
                              .Where(i => i.SystemInformationId == systeminformationid);

 
            OnGetBuildVersionBySystemInformationId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnBuildVersionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnBuildVersionCreated(AdventureWorks.Server.Models.adventureworks.BuildVersion item);
        partial void OnAfterBuildVersionCreated(AdventureWorks.Server.Models.adventureworks.BuildVersion item);

        public async Task<AdventureWorks.Server.Models.adventureworks.BuildVersion> CreateBuildVersion(AdventureWorks.Server.Models.adventureworks.BuildVersion buildversion)
        {
            OnBuildVersionCreated(buildversion);

            var existingItem = Context.BuildVersions
                              .Where(i => i.SystemInformationId == buildversion.SystemInformationId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.BuildVersions.Add(buildversion);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(buildversion).State = EntityState.Detached;
                throw;
            }

            OnAfterBuildVersionCreated(buildversion);

            return buildversion;
        }

        public async Task<AdventureWorks.Server.Models.adventureworks.BuildVersion> CancelBuildVersionChanges(AdventureWorks.Server.Models.adventureworks.BuildVersion item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnBuildVersionUpdated(AdventureWorks.Server.Models.adventureworks.BuildVersion item);
        partial void OnAfterBuildVersionUpdated(AdventureWorks.Server.Models.adventureworks.BuildVersion item);

        public async Task<AdventureWorks.Server.Models.adventureworks.BuildVersion> UpdateBuildVersion(byte systeminformationid, AdventureWorks.Server.Models.adventureworks.BuildVersion buildversion)
        {
            OnBuildVersionUpdated(buildversion);

            var itemToUpdate = Context.BuildVersions
                              .Where(i => i.SystemInformationId == buildversion.SystemInformationId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(buildversion);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterBuildVersionUpdated(buildversion);

            return buildversion;
        }

        partial void OnBuildVersionDeleted(AdventureWorks.Server.Models.adventureworks.BuildVersion item);
        partial void OnAfterBuildVersionDeleted(AdventureWorks.Server.Models.adventureworks.BuildVersion item);

        public async Task<AdventureWorks.Server.Models.adventureworks.BuildVersion> DeleteBuildVersion(byte systeminformationid)
        {
            var itemToDelete = Context.BuildVersions
                              .Where(i => i.SystemInformationId == systeminformationid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnBuildVersionDeleted(itemToDelete);


            Context.BuildVersions.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterBuildVersionDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSalesltCustomersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltcustomers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltcustomers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSalesltCustomersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltcustomers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltcustomers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSalesltCustomersRead(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltCustomer> items);

        public async Task<IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltCustomer>> GetSalesltCustomers(Query query = null)
        {
            var items = Context.SalesltCustomers.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnSalesltCustomersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSalesltCustomerGet(AdventureWorks.Server.Models.adventureworks.SalesltCustomer item);
        partial void OnGetSalesltCustomerByCustomerId(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltCustomer> items);


        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltCustomer> GetSalesltCustomerByCustomerId(int customerid)
        {
            var items = Context.SalesltCustomers
                              .AsNoTracking()
                              .Where(i => i.CustomerId == customerid);

 
            OnGetSalesltCustomerByCustomerId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSalesltCustomerGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSalesltCustomerCreated(AdventureWorks.Server.Models.adventureworks.SalesltCustomer item);
        partial void OnAfterSalesltCustomerCreated(AdventureWorks.Server.Models.adventureworks.SalesltCustomer item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltCustomer> CreateSalesltCustomer(AdventureWorks.Server.Models.adventureworks.SalesltCustomer salesltcustomer)
        {
            OnSalesltCustomerCreated(salesltcustomer);

            var existingItem = Context.SalesltCustomers
                              .Where(i => i.CustomerId == salesltcustomer.CustomerId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SalesltCustomers.Add(salesltcustomer);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(salesltcustomer).State = EntityState.Detached;
                throw;
            }

            OnAfterSalesltCustomerCreated(salesltcustomer);

            return salesltcustomer;
        }

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltCustomer> CancelSalesltCustomerChanges(AdventureWorks.Server.Models.adventureworks.SalesltCustomer item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSalesltCustomerUpdated(AdventureWorks.Server.Models.adventureworks.SalesltCustomer item);
        partial void OnAfterSalesltCustomerUpdated(AdventureWorks.Server.Models.adventureworks.SalesltCustomer item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltCustomer> UpdateSalesltCustomer(int customerid, AdventureWorks.Server.Models.adventureworks.SalesltCustomer salesltcustomer)
        {
            OnSalesltCustomerUpdated(salesltcustomer);

            var itemToUpdate = Context.SalesltCustomers
                              .Where(i => i.CustomerId == salesltcustomer.CustomerId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(salesltcustomer);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSalesltCustomerUpdated(salesltcustomer);

            return salesltcustomer;
        }

        partial void OnSalesltCustomerDeleted(AdventureWorks.Server.Models.adventureworks.SalesltCustomer item);
        partial void OnAfterSalesltCustomerDeleted(AdventureWorks.Server.Models.adventureworks.SalesltCustomer item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltCustomer> DeleteSalesltCustomer(int customerid)
        {
            var itemToDelete = Context.SalesltCustomers
                              .Where(i => i.CustomerId == customerid)
                              .Include(i => i.SalesltCustomerAddresses)
                              .Include(i => i.SalesltSalesOrderHeaders)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSalesltCustomerDeleted(itemToDelete);


            Context.SalesltCustomers.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSalesltCustomerDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSalesltCustomerAddressesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltcustomeraddresses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltcustomeraddresses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSalesltCustomerAddressesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltcustomeraddresses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltcustomeraddresses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSalesltCustomerAddressesRead(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress> items);

        public async Task<IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress>> GetSalesltCustomerAddresses(Query query = null)
        {
            var items = Context.SalesltCustomerAddresses.AsQueryable();

            items = items.Include(i => i.Address);
            items = items.Include(i => i.Customer);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnSalesltCustomerAddressesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSalesltCustomeraddressGet(AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress item);
        partial void OnGetSalesltCustomeraddressByCustomerIdAndAddressId(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress> items);


        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress> GetSalesltCustomeraddressByCustomerIdAndAddressId(int customerid, int addressid)
        {
            var items = Context.SalesltCustomerAddresses
                              .AsNoTracking()
                              .Where(i => i.CustomerId == customerid && i.AddressId == addressid);

            items = items.Include(i => i.Address);
            items = items.Include(i => i.Customer);
 
            OnGetSalesltCustomeraddressByCustomerIdAndAddressId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSalesltCustomeraddressGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSalesltCustomeraddressCreated(AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress item);
        partial void OnAfterSalesltCustomeraddressCreated(AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress> CreateSalesltCustomeraddress(AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress salesltcustomeraddress)
        {
            OnSalesltCustomeraddressCreated(salesltcustomeraddress);

            var existingItem = Context.SalesltCustomerAddresses
                              .Where(i => i.CustomerId == salesltcustomeraddress.CustomerId && i.AddressId == salesltcustomeraddress.AddressId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SalesltCustomerAddresses.Add(salesltcustomeraddress);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(salesltcustomeraddress).State = EntityState.Detached;
                throw;
            }

            OnAfterSalesltCustomeraddressCreated(salesltcustomeraddress);

            return salesltcustomeraddress;
        }

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress> CancelSalesltCustomeraddressChanges(AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSalesltCustomeraddressUpdated(AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress item);
        partial void OnAfterSalesltCustomeraddressUpdated(AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress> UpdateSalesltCustomeraddress(int customerid, int addressid, AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress salesltcustomeraddress)
        {
            OnSalesltCustomeraddressUpdated(salesltcustomeraddress);

            var itemToUpdate = Context.SalesltCustomerAddresses
                              .Where(i => i.CustomerId == salesltcustomeraddress.CustomerId && i.AddressId == salesltcustomeraddress.AddressId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(salesltcustomeraddress);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSalesltCustomeraddressUpdated(salesltcustomeraddress);

            return salesltcustomeraddress;
        }

        partial void OnSalesltCustomeraddressDeleted(AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress item);
        partial void OnAfterSalesltCustomeraddressDeleted(AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress> DeleteSalesltCustomeraddress(int customerid, int addressid)
        {
            var itemToDelete = Context.SalesltCustomerAddresses
                              .Where(i => i.CustomerId == customerid && i.AddressId == addressid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSalesltCustomeraddressDeleted(itemToDelete);


            Context.SalesltCustomerAddresses.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSalesltCustomeraddressDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportErrorLogsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/errorlogs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/errorlogs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportErrorLogsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/errorlogs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/errorlogs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnErrorLogsRead(ref IQueryable<AdventureWorks.Server.Models.adventureworks.ErrorLog> items);

        public async Task<IQueryable<AdventureWorks.Server.Models.adventureworks.ErrorLog>> GetErrorLogs(Query query = null)
        {
            var items = Context.ErrorLogs.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnErrorLogsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnErrorLogGet(AdventureWorks.Server.Models.adventureworks.ErrorLog item);
        partial void OnGetErrorLogByErrorLogId(ref IQueryable<AdventureWorks.Server.Models.adventureworks.ErrorLog> items);


        public async Task<AdventureWorks.Server.Models.adventureworks.ErrorLog> GetErrorLogByErrorLogId(int errorlogid)
        {
            var items = Context.ErrorLogs
                              .AsNoTracking()
                              .Where(i => i.ErrorLogId == errorlogid);

 
            OnGetErrorLogByErrorLogId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnErrorLogGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnErrorLogCreated(AdventureWorks.Server.Models.adventureworks.ErrorLog item);
        partial void OnAfterErrorLogCreated(AdventureWorks.Server.Models.adventureworks.ErrorLog item);

        public async Task<AdventureWorks.Server.Models.adventureworks.ErrorLog> CreateErrorLog(AdventureWorks.Server.Models.adventureworks.ErrorLog errorlog)
        {
            OnErrorLogCreated(errorlog);

            var existingItem = Context.ErrorLogs
                              .Where(i => i.ErrorLogId == errorlog.ErrorLogId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ErrorLogs.Add(errorlog);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(errorlog).State = EntityState.Detached;
                throw;
            }

            OnAfterErrorLogCreated(errorlog);

            return errorlog;
        }

        public async Task<AdventureWorks.Server.Models.adventureworks.ErrorLog> CancelErrorLogChanges(AdventureWorks.Server.Models.adventureworks.ErrorLog item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnErrorLogUpdated(AdventureWorks.Server.Models.adventureworks.ErrorLog item);
        partial void OnAfterErrorLogUpdated(AdventureWorks.Server.Models.adventureworks.ErrorLog item);

        public async Task<AdventureWorks.Server.Models.adventureworks.ErrorLog> UpdateErrorLog(int errorlogid, AdventureWorks.Server.Models.adventureworks.ErrorLog errorlog)
        {
            OnErrorLogUpdated(errorlog);

            var itemToUpdate = Context.ErrorLogs
                              .Where(i => i.ErrorLogId == errorlog.ErrorLogId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(errorlog);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterErrorLogUpdated(errorlog);

            return errorlog;
        }

        partial void OnErrorLogDeleted(AdventureWorks.Server.Models.adventureworks.ErrorLog item);
        partial void OnAfterErrorLogDeleted(AdventureWorks.Server.Models.adventureworks.ErrorLog item);

        public async Task<AdventureWorks.Server.Models.adventureworks.ErrorLog> DeleteErrorLog(int errorlogid)
        {
            var itemToDelete = Context.ErrorLogs
                              .Where(i => i.ErrorLogId == errorlogid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnErrorLogDeleted(itemToDelete);


            Context.ErrorLogs.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterErrorLogDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSalesltProductsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltproducts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltproducts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSalesltProductsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltproducts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltproducts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSalesltProductsRead(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltProduct> items);

        public async Task<IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltProduct>> GetSalesltProducts(Query query = null)
        {
            var items = Context.SalesltProducts.AsQueryable();

            items = items.Include(i => i.ProductCategory);
            items = items.Include(i => i.ProductModel);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnSalesltProductsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSalesltProductGet(AdventureWorks.Server.Models.adventureworks.SalesltProduct item);
        partial void OnGetSalesltProductByProductId(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltProduct> items);


        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProduct> GetSalesltProductByProductId(int productid)
        {
            var items = Context.SalesltProducts
                              .AsNoTracking()
                              .Where(i => i.ProductId == productid);

            items = items.Include(i => i.ProductCategory);
            items = items.Include(i => i.ProductModel);
 
            OnGetSalesltProductByProductId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSalesltProductGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSalesltProductCreated(AdventureWorks.Server.Models.adventureworks.SalesltProduct item);
        partial void OnAfterSalesltProductCreated(AdventureWorks.Server.Models.adventureworks.SalesltProduct item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProduct> CreateSalesltProduct(AdventureWorks.Server.Models.adventureworks.SalesltProduct salesltproduct)
        {
            OnSalesltProductCreated(salesltproduct);

            var existingItem = Context.SalesltProducts
                              .Where(i => i.ProductId == salesltproduct.ProductId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SalesltProducts.Add(salesltproduct);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(salesltproduct).State = EntityState.Detached;
                throw;
            }

            OnAfterSalesltProductCreated(salesltproduct);

            return salesltproduct;
        }

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProduct> CancelSalesltProductChanges(AdventureWorks.Server.Models.adventureworks.SalesltProduct item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSalesltProductUpdated(AdventureWorks.Server.Models.adventureworks.SalesltProduct item);
        partial void OnAfterSalesltProductUpdated(AdventureWorks.Server.Models.adventureworks.SalesltProduct item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProduct> UpdateSalesltProduct(int productid, AdventureWorks.Server.Models.adventureworks.SalesltProduct salesltproduct)
        {
            OnSalesltProductUpdated(salesltproduct);

            var itemToUpdate = Context.SalesltProducts
                              .Where(i => i.ProductId == salesltproduct.ProductId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(salesltproduct);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSalesltProductUpdated(salesltproduct);

            return salesltproduct;
        }

        partial void OnSalesltProductDeleted(AdventureWorks.Server.Models.adventureworks.SalesltProduct item);
        partial void OnAfterSalesltProductDeleted(AdventureWorks.Server.Models.adventureworks.SalesltProduct item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProduct> DeleteSalesltProduct(int productid)
        {
            var itemToDelete = Context.SalesltProducts
                              .Where(i => i.ProductId == productid)
                              .Include(i => i.SalesltSalesOrderDetails)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSalesltProductDeleted(itemToDelete);


            Context.SalesltProducts.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSalesltProductDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSalesltProductCategoriesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltproductcategories/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltproductcategories/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSalesltProductCategoriesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltproductcategories/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltproductcategories/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSalesltProductCategoriesRead(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltProductcategory> items);

        public async Task<IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltProductcategory>> GetSalesltProductCategories(Query query = null)
        {
            var items = Context.SalesltProductCategories.AsQueryable();

            items = items.Include(i => i.ProductCategory);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnSalesltProductCategoriesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSalesltProductcategoryGet(AdventureWorks.Server.Models.adventureworks.SalesltProductcategory item);
        partial void OnGetSalesltProductcategoryByProductCategoryId(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltProductcategory> items);


        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProductcategory> GetSalesltProductcategoryByProductCategoryId(int productcategoryid)
        {
            var items = Context.SalesltProductCategories
                              .AsNoTracking()
                              .Where(i => i.ProductCategoryId == productcategoryid);

            items = items.Include(i => i.ProductCategory);
 
            OnGetSalesltProductcategoryByProductCategoryId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSalesltProductcategoryGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSalesltProductcategoryCreated(AdventureWorks.Server.Models.adventureworks.SalesltProductcategory item);
        partial void OnAfterSalesltProductcategoryCreated(AdventureWorks.Server.Models.adventureworks.SalesltProductcategory item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProductcategory> CreateSalesltProductcategory(AdventureWorks.Server.Models.adventureworks.SalesltProductcategory salesltproductcategory)
        {
            OnSalesltProductcategoryCreated(salesltproductcategory);

            var existingItem = Context.SalesltProductCategories
                              .Where(i => i.ProductCategoryId == salesltproductcategory.ProductCategoryId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SalesltProductCategories.Add(salesltproductcategory);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(salesltproductcategory).State = EntityState.Detached;
                throw;
            }

            OnAfterSalesltProductcategoryCreated(salesltproductcategory);

            return salesltproductcategory;
        }

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProductcategory> CancelSalesltProductcategoryChanges(AdventureWorks.Server.Models.adventureworks.SalesltProductcategory item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSalesltProductcategoryUpdated(AdventureWorks.Server.Models.adventureworks.SalesltProductcategory item);
        partial void OnAfterSalesltProductcategoryUpdated(AdventureWorks.Server.Models.adventureworks.SalesltProductcategory item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProductcategory> UpdateSalesltProductcategory(int productcategoryid, AdventureWorks.Server.Models.adventureworks.SalesltProductcategory salesltproductcategory)
        {
            OnSalesltProductcategoryUpdated(salesltproductcategory);

            var itemToUpdate = Context.SalesltProductCategories
                              .Where(i => i.ProductCategoryId == salesltproductcategory.ProductCategoryId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(salesltproductcategory);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSalesltProductcategoryUpdated(salesltproductcategory);

            return salesltproductcategory;
        }

        partial void OnSalesltProductcategoryDeleted(AdventureWorks.Server.Models.adventureworks.SalesltProductcategory item);
        partial void OnAfterSalesltProductcategoryDeleted(AdventureWorks.Server.Models.adventureworks.SalesltProductcategory item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProductcategory> DeleteSalesltProductcategory(int productcategoryid)
        {
            var itemToDelete = Context.SalesltProductCategories
                              .Where(i => i.ProductCategoryId == productcategoryid)
                              .Include(i => i.SalesltProducts)
                              .Include(i => i.SalesltProductCategories1)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSalesltProductcategoryDeleted(itemToDelete);


            Context.SalesltProductCategories.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSalesltProductcategoryDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSalesltProductDescriptionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltproductdescriptions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltproductdescriptions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSalesltProductDescriptionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltproductdescriptions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltproductdescriptions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSalesltProductDescriptionsRead(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltProductdescription> items);

        public async Task<IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltProductdescription>> GetSalesltProductDescriptions(Query query = null)
        {
            var items = Context.SalesltProductDescriptions.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnSalesltProductDescriptionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSalesltProductdescriptionGet(AdventureWorks.Server.Models.adventureworks.SalesltProductdescription item);
        partial void OnGetSalesltProductdescriptionByProductDescriptionId(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltProductdescription> items);


        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProductdescription> GetSalesltProductdescriptionByProductDescriptionId(int productdescriptionid)
        {
            var items = Context.SalesltProductDescriptions
                              .AsNoTracking()
                              .Where(i => i.ProductDescriptionId == productdescriptionid);

 
            OnGetSalesltProductdescriptionByProductDescriptionId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSalesltProductdescriptionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSalesltProductdescriptionCreated(AdventureWorks.Server.Models.adventureworks.SalesltProductdescription item);
        partial void OnAfterSalesltProductdescriptionCreated(AdventureWorks.Server.Models.adventureworks.SalesltProductdescription item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProductdescription> CreateSalesltProductdescription(AdventureWorks.Server.Models.adventureworks.SalesltProductdescription salesltproductdescription)
        {
            OnSalesltProductdescriptionCreated(salesltproductdescription);

            var existingItem = Context.SalesltProductDescriptions
                              .Where(i => i.ProductDescriptionId == salesltproductdescription.ProductDescriptionId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SalesltProductDescriptions.Add(salesltproductdescription);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(salesltproductdescription).State = EntityState.Detached;
                throw;
            }

            OnAfterSalesltProductdescriptionCreated(salesltproductdescription);

            return salesltproductdescription;
        }

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProductdescription> CancelSalesltProductdescriptionChanges(AdventureWorks.Server.Models.adventureworks.SalesltProductdescription item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSalesltProductdescriptionUpdated(AdventureWorks.Server.Models.adventureworks.SalesltProductdescription item);
        partial void OnAfterSalesltProductdescriptionUpdated(AdventureWorks.Server.Models.adventureworks.SalesltProductdescription item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProductdescription> UpdateSalesltProductdescription(int productdescriptionid, AdventureWorks.Server.Models.adventureworks.SalesltProductdescription salesltproductdescription)
        {
            OnSalesltProductdescriptionUpdated(salesltproductdescription);

            var itemToUpdate = Context.SalesltProductDescriptions
                              .Where(i => i.ProductDescriptionId == salesltproductdescription.ProductDescriptionId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(salesltproductdescription);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSalesltProductdescriptionUpdated(salesltproductdescription);

            return salesltproductdescription;
        }

        partial void OnSalesltProductdescriptionDeleted(AdventureWorks.Server.Models.adventureworks.SalesltProductdescription item);
        partial void OnAfterSalesltProductdescriptionDeleted(AdventureWorks.Server.Models.adventureworks.SalesltProductdescription item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProductdescription> DeleteSalesltProductdescription(int productdescriptionid)
        {
            var itemToDelete = Context.SalesltProductDescriptions
                              .Where(i => i.ProductDescriptionId == productdescriptionid)
                              .Include(i => i.SalesltProductModelProductDescriptions)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSalesltProductdescriptionDeleted(itemToDelete);


            Context.SalesltProductDescriptions.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSalesltProductdescriptionDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSalesltProductModelsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltproductmodels/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltproductmodels/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSalesltProductModelsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltproductmodels/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltproductmodels/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSalesltProductModelsRead(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltProductmodel> items);

        public async Task<IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltProductmodel>> GetSalesltProductModels(Query query = null)
        {
            var items = Context.SalesltProductModels.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnSalesltProductModelsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSalesltProductmodelGet(AdventureWorks.Server.Models.adventureworks.SalesltProductmodel item);
        partial void OnGetSalesltProductmodelByProductModelId(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltProductmodel> items);


        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProductmodel> GetSalesltProductmodelByProductModelId(int productmodelid)
        {
            var items = Context.SalesltProductModels
                              .AsNoTracking()
                              .Where(i => i.ProductModelId == productmodelid);

 
            OnGetSalesltProductmodelByProductModelId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSalesltProductmodelGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSalesltProductmodelCreated(AdventureWorks.Server.Models.adventureworks.SalesltProductmodel item);
        partial void OnAfterSalesltProductmodelCreated(AdventureWorks.Server.Models.adventureworks.SalesltProductmodel item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProductmodel> CreateSalesltProductmodel(AdventureWorks.Server.Models.adventureworks.SalesltProductmodel salesltproductmodel)
        {
            OnSalesltProductmodelCreated(salesltproductmodel);

            var existingItem = Context.SalesltProductModels
                              .Where(i => i.ProductModelId == salesltproductmodel.ProductModelId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SalesltProductModels.Add(salesltproductmodel);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(salesltproductmodel).State = EntityState.Detached;
                throw;
            }

            OnAfterSalesltProductmodelCreated(salesltproductmodel);

            return salesltproductmodel;
        }

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProductmodel> CancelSalesltProductmodelChanges(AdventureWorks.Server.Models.adventureworks.SalesltProductmodel item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSalesltProductmodelUpdated(AdventureWorks.Server.Models.adventureworks.SalesltProductmodel item);
        partial void OnAfterSalesltProductmodelUpdated(AdventureWorks.Server.Models.adventureworks.SalesltProductmodel item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProductmodel> UpdateSalesltProductmodel(int productmodelid, AdventureWorks.Server.Models.adventureworks.SalesltProductmodel salesltproductmodel)
        {
            OnSalesltProductmodelUpdated(salesltproductmodel);

            var itemToUpdate = Context.SalesltProductModels
                              .Where(i => i.ProductModelId == salesltproductmodel.ProductModelId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(salesltproductmodel);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSalesltProductmodelUpdated(salesltproductmodel);

            return salesltproductmodel;
        }

        partial void OnSalesltProductmodelDeleted(AdventureWorks.Server.Models.adventureworks.SalesltProductmodel item);
        partial void OnAfterSalesltProductmodelDeleted(AdventureWorks.Server.Models.adventureworks.SalesltProductmodel item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProductmodel> DeleteSalesltProductmodel(int productmodelid)
        {
            var itemToDelete = Context.SalesltProductModels
                              .Where(i => i.ProductModelId == productmodelid)
                              .Include(i => i.SalesltProducts)
                              .Include(i => i.SalesltProductModelProductDescriptions)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSalesltProductmodelDeleted(itemToDelete);


            Context.SalesltProductModels.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSalesltProductmodelDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSalesltProductModelProductDescriptionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltproductmodelproductdescriptions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltproductmodelproductdescriptions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSalesltProductModelProductDescriptionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltproductmodelproductdescriptions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltproductmodelproductdescriptions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSalesltProductModelProductDescriptionsRead(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription> items);

        public async Task<IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription>> GetSalesltProductModelProductDescriptions(Query query = null)
        {
            var items = Context.SalesltProductModelProductDescriptions.AsQueryable();

            items = items.Include(i => i.ProductDescription);
            items = items.Include(i => i.ProductModel);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnSalesltProductModelProductDescriptionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSalesltProductmodelproductdescriptionGet(AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription item);
        partial void OnGetSalesltProductmodelproductdescriptionByProductModelIdAndProductDescriptionIdAndCulture(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription> items);


        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription> GetSalesltProductmodelproductdescriptionByProductModelIdAndProductDescriptionIdAndCulture(int productmodelid, int productdescriptionid, string culture)
        {
            var items = Context.SalesltProductModelProductDescriptions
                              .AsNoTracking()
                              .Where(i => i.ProductModelId == productmodelid && i.ProductDescriptionId == productdescriptionid && i.Culture == culture);

            items = items.Include(i => i.ProductDescription);
            items = items.Include(i => i.ProductModel);
 
            OnGetSalesltProductmodelproductdescriptionByProductModelIdAndProductDescriptionIdAndCulture(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSalesltProductmodelproductdescriptionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSalesltProductmodelproductdescriptionCreated(AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription item);
        partial void OnAfterSalesltProductmodelproductdescriptionCreated(AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription> CreateSalesltProductmodelproductdescription(AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription salesltproductmodelproductdescription)
        {
            OnSalesltProductmodelproductdescriptionCreated(salesltproductmodelproductdescription);

            var existingItem = Context.SalesltProductModelProductDescriptions
                              .Where(i => i.ProductModelId == salesltproductmodelproductdescription.ProductModelId && i.ProductDescriptionId == salesltproductmodelproductdescription.ProductDescriptionId && i.Culture == salesltproductmodelproductdescription.Culture)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SalesltProductModelProductDescriptions.Add(salesltproductmodelproductdescription);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(salesltproductmodelproductdescription).State = EntityState.Detached;
                throw;
            }

            OnAfterSalesltProductmodelproductdescriptionCreated(salesltproductmodelproductdescription);

            return salesltproductmodelproductdescription;
        }

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription> CancelSalesltProductmodelproductdescriptionChanges(AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSalesltProductmodelproductdescriptionUpdated(AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription item);
        partial void OnAfterSalesltProductmodelproductdescriptionUpdated(AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription> UpdateSalesltProductmodelproductdescription(int productmodelid, int productdescriptionid, string culture, AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription salesltproductmodelproductdescription)
        {
            OnSalesltProductmodelproductdescriptionUpdated(salesltproductmodelproductdescription);

            var itemToUpdate = Context.SalesltProductModelProductDescriptions
                              .Where(i => i.ProductModelId == salesltproductmodelproductdescription.ProductModelId && i.ProductDescriptionId == salesltproductmodelproductdescription.ProductDescriptionId && i.Culture == salesltproductmodelproductdescription.Culture)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(salesltproductmodelproductdescription);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSalesltProductmodelproductdescriptionUpdated(salesltproductmodelproductdescription);

            return salesltproductmodelproductdescription;
        }

        partial void OnSalesltProductmodelproductdescriptionDeleted(AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription item);
        partial void OnAfterSalesltProductmodelproductdescriptionDeleted(AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription> DeleteSalesltProductmodelproductdescription(int productmodelid, int productdescriptionid, string culture)
        {
            var itemToDelete = Context.SalesltProductModelProductDescriptions
                              .Where(i => i.ProductModelId == productmodelid && i.ProductDescriptionId == productdescriptionid && i.Culture == culture)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSalesltProductmodelproductdescriptionDeleted(itemToDelete);


            Context.SalesltProductModelProductDescriptions.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSalesltProductmodelproductdescriptionDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSalesltSalesOrderDetailsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltsalesorderdetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltsalesorderdetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSalesltSalesOrderDetailsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltsalesorderdetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltsalesorderdetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSalesltSalesOrderDetailsRead(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail> items);

        public async Task<IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail>> GetSalesltSalesOrderDetails(Query query = null)
        {
            var items = Context.SalesltSalesOrderDetails.AsQueryable();

            items = items.Include(i => i.Product);
            items = items.Include(i => i.SalesOrderHeader);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnSalesltSalesOrderDetailsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSalesltSalesorderdetailGet(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail item);
        partial void OnGetSalesltSalesorderdetailBySalesOrderIdAndSalesOrderDetailId(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail> items);


        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail> GetSalesltSalesorderdetailBySalesOrderIdAndSalesOrderDetailId(int salesorderid, int salesorderdetailid)
        {
            var items = Context.SalesltSalesOrderDetails
                              .AsNoTracking()
                              .Where(i => i.SalesOrderId == salesorderid && i.SalesOrderDetailId == salesorderdetailid);

            items = items.Include(i => i.Product);
            items = items.Include(i => i.SalesOrderHeader);
 
            OnGetSalesltSalesorderdetailBySalesOrderIdAndSalesOrderDetailId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSalesltSalesorderdetailGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSalesltSalesorderdetailCreated(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail item);
        partial void OnAfterSalesltSalesorderdetailCreated(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail> CreateSalesltSalesorderdetail(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail salesltsalesorderdetail)
        {
            OnSalesltSalesorderdetailCreated(salesltsalesorderdetail);

            var existingItem = Context.SalesltSalesOrderDetails
                              .Where(i => i.SalesOrderId == salesltsalesorderdetail.SalesOrderId && i.SalesOrderDetailId == salesltsalesorderdetail.SalesOrderDetailId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SalesltSalesOrderDetails.Add(salesltsalesorderdetail);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(salesltsalesorderdetail).State = EntityState.Detached;
                throw;
            }

            OnAfterSalesltSalesorderdetailCreated(salesltsalesorderdetail);

            return salesltsalesorderdetail;
        }

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail> CancelSalesltSalesorderdetailChanges(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSalesltSalesorderdetailUpdated(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail item);
        partial void OnAfterSalesltSalesorderdetailUpdated(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail> UpdateSalesltSalesorderdetail(int salesorderid, int salesorderdetailid, AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail salesltsalesorderdetail)
        {
            OnSalesltSalesorderdetailUpdated(salesltsalesorderdetail);

            var itemToUpdate = Context.SalesltSalesOrderDetails
                              .Where(i => i.SalesOrderId == salesltsalesorderdetail.SalesOrderId && i.SalesOrderDetailId == salesltsalesorderdetail.SalesOrderDetailId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(salesltsalesorderdetail);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSalesltSalesorderdetailUpdated(salesltsalesorderdetail);

            return salesltsalesorderdetail;
        }

        partial void OnSalesltSalesorderdetailDeleted(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail item);
        partial void OnAfterSalesltSalesorderdetailDeleted(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail> DeleteSalesltSalesorderdetail(int salesorderid, int salesorderdetailid)
        {
            var itemToDelete = Context.SalesltSalesOrderDetails
                              .Where(i => i.SalesOrderId == salesorderid && i.SalesOrderDetailId == salesorderdetailid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSalesltSalesorderdetailDeleted(itemToDelete);


            Context.SalesltSalesOrderDetails.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSalesltSalesorderdetailDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSalesltSalesOrderHeadersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltsalesorderheaders/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltsalesorderheaders/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSalesltSalesOrderHeadersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/adventureworks/salesltsalesorderheaders/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/adventureworks/salesltsalesorderheaders/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSalesltSalesOrderHeadersRead(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader> items);

        public async Task<IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader>> GetSalesltSalesOrderHeaders(Query query = null)
        {
            var items = Context.SalesltSalesOrderHeaders.AsQueryable();

            items = items.Include(i => i.Address);
            items = items.Include(i => i.Customer);
            items = items.Include(i => i.Address1);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnSalesltSalesOrderHeadersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSalesltSalesorderheaderGet(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader item);
        partial void OnGetSalesltSalesorderheaderBySalesOrderId(ref IQueryable<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader> items);


        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader> GetSalesltSalesorderheaderBySalesOrderId(int salesorderid)
        {
            var items = Context.SalesltSalesOrderHeaders
                              .AsNoTracking()
                              .Where(i => i.SalesOrderId == salesorderid);

            items = items.Include(i => i.Address);
            items = items.Include(i => i.Customer);
            items = items.Include(i => i.Address1);
 
            OnGetSalesltSalesorderheaderBySalesOrderId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSalesltSalesorderheaderGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSalesltSalesorderheaderCreated(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader item);
        partial void OnAfterSalesltSalesorderheaderCreated(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader> CreateSalesltSalesorderheader(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader salesltsalesorderheader)
        {
            OnSalesltSalesorderheaderCreated(salesltsalesorderheader);

            var existingItem = Context.SalesltSalesOrderHeaders
                              .Where(i => i.SalesOrderId == salesltsalesorderheader.SalesOrderId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SalesltSalesOrderHeaders.Add(salesltsalesorderheader);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(salesltsalesorderheader).State = EntityState.Detached;
                throw;
            }

            OnAfterSalesltSalesorderheaderCreated(salesltsalesorderheader);

            return salesltsalesorderheader;
        }

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader> CancelSalesltSalesorderheaderChanges(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSalesltSalesorderheaderUpdated(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader item);
        partial void OnAfterSalesltSalesorderheaderUpdated(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader> UpdateSalesltSalesorderheader(int salesorderid, AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader salesltsalesorderheader)
        {
            OnSalesltSalesorderheaderUpdated(salesltsalesorderheader);

            var itemToUpdate = Context.SalesltSalesOrderHeaders
                              .Where(i => i.SalesOrderId == salesltsalesorderheader.SalesOrderId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(salesltsalesorderheader);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSalesltSalesorderheaderUpdated(salesltsalesorderheader);

            return salesltsalesorderheader;
        }

        partial void OnSalesltSalesorderheaderDeleted(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader item);
        partial void OnAfterSalesltSalesorderheaderDeleted(AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader item);

        public async Task<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader> DeleteSalesltSalesorderheader(int salesorderid)
        {
            var itemToDelete = Context.SalesltSalesOrderHeaders
                              .Where(i => i.SalesOrderId == salesorderid)
                              .Include(i => i.SalesltSalesOrderDetails)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSalesltSalesorderheaderDeleted(itemToDelete);


            Context.SalesltSalesOrderHeaders.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSalesltSalesorderheaderDeleted(itemToDelete);

            return itemToDelete;
        }
        }
}