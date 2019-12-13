/*
The MIT License (MIT)
Copyright (c) 2007 - 2019 Microting A/S
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using Microsoft.EntityFrameworkCore;
using Microting.eFormApi.BasePn.Abstractions;
using Microting.eFormApi.BasePn.Infrastructure.Database.Entities;
using Microting.eFormRentableItemBase.Infrastructure.Data.Entities;

namespace Microting.eFormRentableItemBase.Infrastructure.Data
{
    public class eFormRentableItemPnDbContext : DbContext, IPluginDbContext
    {
        public eFormRentableItemPnDbContext() { }

        public eFormRentableItemPnDbContext(DbContextOptions<eFormRentableItemPnDbContext> options) : base(options) { }
        
        public DbSet<PluginConfigurationValue> PluginConfigurationValues { get; set; }
        public DbSet<PluginConfigurationValueVersion> PluginConfigurationValueVersions { get; set; }
        public DbSet<PluginPermission> PluginPermissions { get; set; }
        public DbSet<PluginGroupPermission> PluginGroupPermissions { get; set; }
        public DbSet<PluginGroupPermissionVersion> PluginGroupPermissionVersions { get; set; }
        public DbSet<Contract> Contract { get; set; }
        public DbSet<ContractVersion> ContractVersion { get; set; }
        public DbSet<ContractInspection> ContractInspection { get; set; }
        public DbSet<ContractInspectionVersion> ContractInspectionVersion { get; set; }
        public DbSet<RentableItem> RentableItem { get; set; }
        public DbSet<RentableItemVersion> RentableItemsVersion { get; set; }
        public DbSet<ContractRentableItem> ContractRentableItem { get; set; }
        public DbSet<ContractRentableItemVersion> ContractRentableItemVersion { get; set; }
        public DbSet<ContractInspectionItem> ContractInspectionItem { get; set; }
        public DbSet<ContractInspectionItemVersion> ContractInspectionItemVersion { get; set; }
    }
}