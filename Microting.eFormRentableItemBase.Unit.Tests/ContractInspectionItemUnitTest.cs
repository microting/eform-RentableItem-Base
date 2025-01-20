using System;
using System.Collections.Generic;
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
            Random rnd = new Random();
            DateTime contractEnd = DateTime.Now;
            DateTime contractStart = DateTime.Now;
            Contract contract = new Contract
            {
                Status = 66,
                ContractEnd = contractEnd,
                ContractNr = rnd.Next(1, 255),
                ContractStart = contractStart,
                CustomerId = rnd.Next(1, 255)
            };
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
            Assert.That(dbInspectionItem, Is.Not.Null);
            Assert.That(inspectionItems, Is.Not.Null);
            Assert.That(versionList, Is.Not.Null);

            Assert.That(dbInspectionItem.Status, Is.EqualTo(inspectionItem.Status));
            Assert.That(dbInspectionItem.ContractInspectionId, Is.EqualTo(inspectionItem.ContractInspectionId));
            Assert.That(dbInspectionItem.RentableItemId, Is.EqualTo(inspectionItem.RentableItemId));
            Assert.That(dbInspectionItem.SDKCaseId, Is.EqualTo(inspectionItem.SDKCaseId));
            Assert.That(dbInspectionItem.SiteId, Is.EqualTo(inspectionItem.SiteId));
        }

        [Test]
        public async Task ContractInspectionItem_Update_DoesUpdate()
        {
            //Arrange
            Random rnd = new Random();
            DateTime contractEnd = DateTime.Now;
            DateTime contractStart = DateTime.Now;
            Contract contract = new Contract
            {
                Status = 66,
                ContractEnd = contractEnd,
                ContractNr = rnd.Next(1, 255),
                ContractStart = contractStart,
                CustomerId = rnd.Next(1, 255)
            };
            await contract.Create(DbContext);
            Contract contract2 = new Contract
            {
                Status = 100,
                ContractEnd = contractEnd,
                ContractNr = rnd.Next(1, 255),
                ContractStart = contractStart,
                CustomerId = rnd.Next(1, 255)
            };
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
            RentableItem rentableItem2 = new RentableItem
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
            inspectionItem.RentableItemId = rentableItem2.Id;
            //Act
            await inspectionItem.Update(DbContext);

            ContractInspectionItem dbInspectionItem =
                await DbContext.ContractInspectionItem.AsNoTracking().FirstAsync();
            List<ContractInspectionItem> inspectionItems =
                await DbContext.ContractInspectionItem.AsNoTracking().ToListAsync();
            List<ContractInspectionItemVersion> versionList =
                await DbContext.ContractInspectionItemVersion.AsNoTracking().ToListAsync();

            //Assert
            Assert.That(dbInspectionItem, Is.Not.Null);
            Assert.That(inspectionItems, Is.Not.Null);
            Assert.That(versionList, Is.Not.Null);

            Assert.That(dbInspectionItem.Status, Is.EqualTo(inspectionItem.Status));
            Assert.That(dbInspectionItem.ContractInspectionId, Is.EqualTo(inspectionItem.ContractInspectionId));
            Assert.That(dbInspectionItem.RentableItemId, Is.EqualTo(inspectionItem.RentableItemId));
            Assert.That(dbInspectionItem.SDKCaseId, Is.EqualTo(inspectionItem.SDKCaseId));
            Assert.That(dbInspectionItem.SiteId, Is.EqualTo(inspectionItem.SiteId));
        }

        [Test]
        public async Task ContractInspectionItem_Delete_DoesDelete()
        {
                 //Arrange
            Random rnd = new Random();
            DateTime contractEnd = DateTime.Now;
            DateTime contractStart = DateTime.Now;
            Contract contract = new Contract
            {
                Status = 66,
                ContractEnd = contractEnd,
                ContractNr = rnd.Next(1, 255),
                ContractStart = contractStart,
                CustomerId = rnd.Next(1, 255)
            };
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
            Assert.That(dbInspectionItem, Is.Not.Null);
            Assert.That(inspectionItems, Is.Not.Null);
            Assert.That(versionList, Is.Not.Null);

            Assert.That(dbInspectionItem.Status, Is.EqualTo(inspectionItem.Status));
            Assert.That(dbInspectionItem.ContractInspectionId, Is.EqualTo(inspectionItem.ContractInspectionId));
            Assert.That(dbInspectionItem.RentableItemId, Is.EqualTo(inspectionItem.RentableItemId));
            Assert.That(dbInspectionItem.SDKCaseId, Is.EqualTo(inspectionItem.SDKCaseId));
            Assert.That(dbInspectionItem.SiteId, Is.EqualTo(inspectionItem.SiteId));
            Assert.That(Constants.WorkflowStates.Removed, Is.EqualTo(inspectionItem.WorkflowState));
        }
    }
}