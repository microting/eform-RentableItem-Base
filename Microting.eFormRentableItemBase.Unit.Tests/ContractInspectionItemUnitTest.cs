using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microting.eForm.Infrastructure.Constants;
using Microting.eFormRentableItemBase.Infrastructure.Data.Entities;
using NUnit.Framework;

namespace Microting.eFormRentableItemBase.Unit.Tests
{
    [TestFixture]
    public class ContractInspectionItemUnitTest : DbTestFixture
    {
        [Test]
        public async Task ContractInspectionItem_Create_DoesCreate()
        {
            //Arrange
            Contract contract = new Contract();
            Random rnd = new Random();
            DateTime contractEnd = DateTime.Now;
            DateTime contractStart = DateTime.Now;
            contract.Status = 66;
            contract.ContractEnd = contractEnd;
            contract.ContractNr = rnd.Next(1, 255);
            contract.ContractStart = contractStart;
            contract.CustomerId = rnd.Next(1, 255);
            await contract.Create(DbContext);

            RentableItem rentableItem = new RentableItem
            {
                ModelName = Guid.NewGuid().ToString(),
                Brand = Guid.NewGuid().ToString(),
                RegistrationDate = DateTime.Now,
                VinNumber = Guid.NewGuid().ToString(),
                PlateNumber = Guid.NewGuid().ToString(),
                SerialNumber = Guid.NewGuid().ToString(),
                eFormId = rnd.Next(1, 255)
            };
            
            ContractInspection contractInspection = new ContractInspection
            {
                ContractId = contract.Id,
                DoneAt = DateTime.Now,
               
            };
            await contractInspection.Create(DbContext);

            ContractInspectionItem inspectionItem = new ContractInspectionItem
            {
                ContractInspectionId = contractInspection.Id,
                RentableItemId = rentableItem.Id,
                SDKCaseId = rnd.Next(1, 66),
                SiteId = rnd.Next(1, 999999),
                Status = 66
            };
            
            //Act
            await inspectionItem.Create(DbContext);

            ContractInspectionItem dbInspectionItem =
                await DbContext.ContractInspectionItem.AsNoTracking().FirstAsync();
            List<ContractInspectionItem> inspectionItems =
                await DbContext.ContractInspectionItem.AsNoTracking().ToListAsync();
            List<ContractInspectionItemVersion> versionList =
                await DbContext.ContractInspectionItemVersion.AsNoTracking().ToListAsync();
            
            //Assert
            Assert.NotNull(dbInspectionItem);
            Assert.NotNull(inspectionItems);
            Assert.NotNull(versionList);

            Assert.AreEqual(inspectionItem.Status, dbInspectionItem.Status);
            Assert.AreEqual(inspectionItem.ContractInspectionId, dbInspectionItem.ContractInspectionId);
            Assert.AreEqual(inspectionItem.RentableItemId, dbInspectionItem.RentableItemId);
            Assert.AreEqual(inspectionItem.SDKCaseId, dbInspectionItem.SDKCaseId);
            Assert.AreEqual(inspectionItem.SiteId, dbInspectionItem.SiteId);
        }
        
        [Test]
        public async Task ContractInspectionItem_Update_DoesUpdate()
        {
            //Arrange
            Contract contract = new Contract();
            Random rnd = new Random();
            DateTime contractEnd = DateTime.Now;
            DateTime contractStart = DateTime.Now;
            contract.Status = 66;
            contract.ContractEnd = contractEnd;
            contract.ContractNr = rnd.Next(1, 255);
            contract.ContractStart = contractStart;
            contract.CustomerId = rnd.Next(1, 255);
            await contract.Create(DbContext);
            Contract contract2 = new Contract();
            contract.Status = 100;
            contract.ContractEnd = contractEnd;
            contract.ContractNr = rnd.Next(1, 255);
            contract.ContractStart = contractStart;
            contract.CustomerId = rnd.Next(1, 255);
            await contract2.Create(DbContext);

            RentableItem rentableItem = new RentableItem
            {
                ModelName = Guid.NewGuid().ToString(),
                Brand = Guid.NewGuid().ToString(),
                RegistrationDate = DateTime.Now,
                VinNumber = Guid.NewGuid().ToString(),
                PlateNumber = Guid.NewGuid().ToString(),
                SerialNumber = Guid.NewGuid().ToString(),
                eFormId = rnd.Next(1, 255)
            };
            RentableItem rentbaleItem2 = new RentableItem
            {
                Brand = Guid.NewGuid().ToString(),
                ModelName = Guid.NewGuid().ToString(),
                VinNumber = Guid.NewGuid().ToString(),
                SerialNumber = Guid.NewGuid().ToString(),
                PlateNumber = Guid.NewGuid().ToString(),
                RegistrationDate = DateTime.Now.AddDays(1)
            };
            
            ContractInspection contractInspection = new ContractInspection
            {
                ContractId = contract.Id,
                DoneAt = DateTime.Now,
            };
            await contractInspection.Create(DbContext);

            ContractInspection contractInspection2 = new ContractInspection
            {
                ContractId = contract2.Id,
                DoneAt = DateTime.Now.AddDays(1)
            };
            
            ContractInspectionItem inspectionItem = new ContractInspectionItem
            {
                ContractInspectionId = contractInspection.Id,
                RentableItemId = rentableItem.Id,
                SDKCaseId = rnd.Next(1, 66),
                SiteId = rnd.Next(1, 999999),
                Status = 66
            };
            await inspectionItem.Create(DbContext);

            inspectionItem.Status = 100;
            inspectionItem.SDKCaseId = rnd.Next(1, 255);
            inspectionItem.SiteId = rnd.Next(1, 255);
            inspectionItem.ContractInspectionId = contractInspection2.Id;
            inspectionItem.RentableItemId = rentbaleItem2.Id;
            //Act
            await inspectionItem.Update(DbContext);
            
            ContractInspectionItem dbInspectionItem =
                await DbContext.ContractInspectionItem.AsNoTracking().FirstAsync();
            List<ContractInspectionItem> inspectionItems =
                await DbContext.ContractInspectionItem.AsNoTracking().ToListAsync();
            List<ContractInspectionItemVersion> versionList =
                await DbContext.ContractInspectionItemVersion.AsNoTracking().ToListAsync();
            
            //Assert
            Assert.NotNull(dbInspectionItem);
            Assert.NotNull(inspectionItems);
            Assert.NotNull(versionList);

            Assert.AreEqual(inspectionItem.Status, dbInspectionItem.Status);
            Assert.AreEqual(inspectionItem.ContractInspectionId, dbInspectionItem.ContractInspectionId);
            Assert.AreEqual(inspectionItem.RentableItemId, dbInspectionItem.RentableItemId);
            Assert.AreEqual(inspectionItem.SDKCaseId, dbInspectionItem.SDKCaseId);
            Assert.AreEqual(inspectionItem.SiteId, dbInspectionItem.SiteId);
        }

        [Test]
        public async Task ContractInspectionItem_Delete_DoesDelete()
        {
                 //Arrange
            Contract contract = new Contract();
            Random rnd = new Random();
            DateTime contractEnd = DateTime.Now;
            DateTime contractStart = DateTime.Now;
            contract.Status = 66;
            contract.ContractEnd = contractEnd;
            contract.ContractNr = rnd.Next(1, 255);
            contract.ContractStart = contractStart;
            contract.CustomerId = rnd.Next(1, 255);
            await contract.Create(DbContext);

            RentableItem rentableItem = new RentableItem
            {
                ModelName = Guid.NewGuid().ToString(),
                Brand = Guid.NewGuid().ToString(),
                RegistrationDate = DateTime.Now,
                VinNumber = Guid.NewGuid().ToString(),
                PlateNumber = Guid.NewGuid().ToString(),
                SerialNumber = Guid.NewGuid().ToString(),
                eFormId = rnd.Next(1, 255)
            };
            
            ContractInspection contractInspection = new ContractInspection
            {
                ContractId = contract.Id,
                DoneAt = DateTime.Now,
               
            };
            await contractInspection.Create(DbContext);

            ContractInspectionItem inspectionItem = new ContractInspectionItem
            {
                ContractInspectionId = contractInspection.Id,
                RentableItemId = rentableItem.Id,
                SDKCaseId = rnd.Next(1, 66),
                SiteId = rnd.Next(1, 999999),
                Status = 66
            };
            await inspectionItem.Create(DbContext);

            //Act
            await inspectionItem.Delete(DbContext);
            ContractInspectionItem dbInspectionItem =
                await DbContext.ContractInspectionItem.AsNoTracking().FirstAsync();
            List<ContractInspectionItem> inspectionItems =
                await DbContext.ContractInspectionItem.AsNoTracking().ToListAsync();
            List<ContractInspectionItemVersion> versionList =
                await DbContext.ContractInspectionItemVersion.AsNoTracking().ToListAsync();
            
            //Assert
            Assert.NotNull(dbInspectionItem);
            Assert.NotNull(inspectionItems);
            Assert.NotNull(versionList);

            Assert.AreEqual(inspectionItem.Status, dbInspectionItem.Status);
            Assert.AreEqual(inspectionItem.ContractInspectionId, dbInspectionItem.ContractInspectionId);
            Assert.AreEqual(inspectionItem.RentableItemId, dbInspectionItem.RentableItemId);
            Assert.AreEqual(inspectionItem.SDKCaseId, dbInspectionItem.SDKCaseId);
            Assert.AreEqual(inspectionItem.SiteId, dbInspectionItem.SiteId);
            Assert.AreEqual(inspectionItem.WorkflowState, Constants.WorkflowStates.Removed);
        }
    }
}