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
    public class ContractRentableItemUnitTest : DbTestFixture
    {
        [Test]
        public async Task ContractRentableItem_Create_DoesCreate()
        {
            //Arrange
            RentableItem rentableItem = new RentableItem
            {
                Brand = Guid.NewGuid().ToString(),
                ModelName =  Guid.NewGuid().ToString(),
                VinNumber = Guid.NewGuid().ToString(),
                SerialNumber = Guid.NewGuid().ToString(),
                PlateNumber = Guid.NewGuid().ToString(),
                RegistrationDate = DateTime.Now
            };
            await rentableItem.Create(DbContext);

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

            ContractRentableItem contractRentableItem = new ContractRentableItem
            {
                ContractId = contract.Id,
                RentableItemId = rentableItem.Id
            };
            //Act
            await contractRentableItem.Create(DbContext);

            ContractRentableItem dbContractRentableItem = DbContext.ContractRentableItem.AsNoTracking().First();
            List<ContractRentableItem> contractRentableItems = DbContext.ContractRentableItem.AsNoTracking().ToList();
            List<ContractRentableItemVersion> versionList =
                DbContext.ContractRentableItemVersion.AsNoTracking().ToList();
            //Assert
            Assert.That(dbContractRentableItem, Is.Not.Null);

            Assert.That(contractRentableItems.Count, Is.EqualTo(1));
            Assert.That(versionList.Count, Is.EqualTo(1));

            Assert.That(dbContractRentableItem.ContractId, Is.EqualTo(contractRentableItem.ContractId));
            Assert.That(dbContractRentableItem.RentableItemId, Is.EqualTo(contractRentableItem.RentableItemId));
        }

        [Test]
        public async Task ContractRentableItem_Update_DoesUpdate_OnlyContractID()
        {
             //Arrange
            RentableItem rentableItem = new RentableItem
            {
                Brand = Guid.NewGuid().ToString(),
                ModelName =  Guid.NewGuid().ToString(),
                VinNumber = Guid.NewGuid().ToString(),
                SerialNumber = Guid.NewGuid().ToString(),
                PlateNumber = Guid.NewGuid().ToString(),
                RegistrationDate = DateTime.Now
            };
            await rentableItem.Create(DbContext);

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

            ContractRentableItem contractRentableItem = new ContractRentableItem
            {
                ContractId = contract.Id,
                RentableItemId = rentableItem.Id
            };
            await contractRentableItem.Create(DbContext);
            Contract contract2 = new Contract
            {
                Status = 100,
                ContractEnd = contractEnd.AddDays(1),
                ContractNr = rnd.Next(1, 255),
                ContractStart = contractStart.AddDays(1),
                CustomerId = rnd.Next(1, 255)
            };
            await contract2.Create(DbContext);

            contractRentableItem.ContractId = contract2.Id;
            //Act
            await contractRentableItem.Update(DbContext);

            ContractRentableItem dbContractRentableItem = DbContext.ContractRentableItem.AsNoTracking().First();
            List<ContractRentableItem> contractRentableItems = DbContext.ContractRentableItem.AsNoTracking().ToList();
            List<ContractRentableItemVersion> versionList =
                DbContext.ContractRentableItemVersion.AsNoTracking().ToList();
            //Assert
            Assert.That(dbContractRentableItem, Is.Not.Null);

            Assert.That(contractRentableItems.Count, Is.EqualTo(1));
            Assert.That(versionList.Count, Is.EqualTo(2));

            Assert.That(dbContractRentableItem.ContractId, Is.EqualTo(contractRentableItem.ContractId));
            Assert.That(dbContractRentableItem.RentableItemId, Is.EqualTo(contractRentableItem.RentableItemId));
        }
        [Test]
        public async Task ContractRentableItem_Update_DoesUpdate_OnlyRentableItemID()
        {
             //Arrange
            RentableItem rentableItem = new RentableItem
            {
                Brand = Guid.NewGuid().ToString(),
                ModelName =  Guid.NewGuid().ToString(),
                VinNumber = Guid.NewGuid().ToString(),
                SerialNumber = Guid.NewGuid().ToString(),
                PlateNumber = Guid.NewGuid().ToString(),
                RegistrationDate = DateTime.Now
            };
            await rentableItem.Create(DbContext);

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

            ContractRentableItem contractRentableItem = new ContractRentableItem
            {
                ContractId = contract.Id,
                RentableItemId = rentableItem.Id
            };
            await contractRentableItem.Create(DbContext);
            RentableItem rentableItem2 = new RentableItem
            {
                Brand = Guid.NewGuid().ToString(),
                ModelName =  Guid.NewGuid().ToString(),
                VinNumber = Guid.NewGuid().ToString(),
                SerialNumber = Guid.NewGuid().ToString(),
                PlateNumber = Guid.NewGuid().ToString(),
                RegistrationDate = DateTime.Now.AddDays(1)
            };
            await rentableItem2.Create(DbContext);

            contractRentableItem.RentableItemId = rentableItem2.Id;
            //Act
            await contractRentableItem.Update(DbContext);

            ContractRentableItem dbContractRentableItem = DbContext.ContractRentableItem.AsNoTracking().First();
            List<ContractRentableItem> contractRentableItems = DbContext.ContractRentableItem.AsNoTracking().ToList();
            List<ContractRentableItemVersion> versionList =
                DbContext.ContractRentableItemVersion.AsNoTracking().ToList();
            //Assert
            Assert.That(dbContractRentableItem, Is.Not.Null);

            Assert.That(contractRentableItems.Count, Is.EqualTo(1));
            Assert.That(versionList.Count, Is.EqualTo(2));

            Assert.That(dbContractRentableItem.ContractId, Is.EqualTo(contractRentableItem.ContractId));
            Assert.That(dbContractRentableItem.RentableItemId, Is.EqualTo(contractRentableItem.RentableItemId));
        }
         [Test]
        public async Task ContractRentableItem_Update_DoesUpdate()
        {
             //Arrange
            RentableItem rentableItem = new RentableItem
            {
                Brand = Guid.NewGuid().ToString(),
                ModelName =  Guid.NewGuid().ToString(),
                VinNumber = Guid.NewGuid().ToString(),
                SerialNumber = Guid.NewGuid().ToString(),
                PlateNumber = Guid.NewGuid().ToString(),
                RegistrationDate = DateTime.Now
            };
            await rentableItem.Create(DbContext);

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

            ContractRentableItem contractRentableItem = new ContractRentableItem
            {
                ContractId = contract.Id,
                RentableItemId = rentableItem.Id
            };
            await contractRentableItem.Create(DbContext);
            RentableItem rentableItem2 = new RentableItem
            {
                Brand = Guid.NewGuid().ToString(),
                ModelName =  Guid.NewGuid().ToString(),
                VinNumber = Guid.NewGuid().ToString(),
                SerialNumber = Guid.NewGuid().ToString(),
                PlateNumber = Guid.NewGuid().ToString(),
                RegistrationDate = DateTime.Now.AddDays(1)
            };
            await rentableItem2.Create(DbContext);
            Contract contract2 = new Contract
            {
                Status = 100,
                ContractEnd = contractEnd,
                ContractNr = rnd.Next(1, 255),
                ContractStart = contractStart,
                CustomerId = rnd.Next(1, 255)
            };
            await contract2.Create(DbContext);
            contractRentableItem.ContractId = contract2.Id;
            contractRentableItem.RentableItemId = rentableItem2.Id;
            //Act
            await contractRentableItem.Update(DbContext);

            ContractRentableItem dbContractRentableItem = DbContext.ContractRentableItem.AsNoTracking().First();
            List<ContractRentableItem> contractRentableItems = DbContext.ContractRentableItem.AsNoTracking().ToList();
            List<ContractRentableItemVersion> versionList =
                DbContext.ContractRentableItemVersion.AsNoTracking().ToList();
            //Assert
            Assert.That(dbContractRentableItem, Is.Not.Null);

            Assert.That(contractRentableItems.Count, Is.EqualTo(1));
            Assert.That(versionList.Count, Is.EqualTo(2));

            Assert.That(dbContractRentableItem.ContractId, Is.EqualTo(contractRentableItem.ContractId));
            Assert.That(dbContractRentableItem.RentableItemId, Is.EqualTo(contractRentableItem.RentableItemId));
        }

        [Test]
        public async Task ContractRentableItem_Delete_DoesDelete()
        {
             //Arrange
            RentableItem rentableItem = new RentableItem
            {
                Brand = Guid.NewGuid().ToString(),
                ModelName =  Guid.NewGuid().ToString(),
                VinNumber = Guid.NewGuid().ToString(),
                SerialNumber = Guid.NewGuid().ToString(),
                PlateNumber = Guid.NewGuid().ToString(),
                RegistrationDate = DateTime.Now
            };
            await rentableItem.Create(DbContext);

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

            ContractRentableItem contractRentableItem = new ContractRentableItem
            {
                ContractId = contract.Id,
                RentableItemId = rentableItem.Id
            };
            await contractRentableItem.Create(DbContext);

            //Act
            await contractRentableItem.Delete(DbContext);

            ContractRentableItem dbContractRentableItem = DbContext.ContractRentableItem.AsNoTracking().First();
            List<ContractRentableItem> contractRentableItems = DbContext.ContractRentableItem.AsNoTracking().ToList();
            List<ContractRentableItemVersion> versionList =
                DbContext.ContractRentableItemVersion.AsNoTracking().ToList();
            //Assert
            Assert.That(dbContractRentableItem, Is.Not.Null);

            Assert.That(contractRentableItems.Count, Is.EqualTo(1));
            Assert.That(versionList.Count, Is.EqualTo(2));

            Assert.That(dbContractRentableItem.ContractId, Is.EqualTo(contractRentableItem.ContractId));
            Assert.That(dbContractRentableItem.RentableItemId, Is.EqualTo(contractRentableItem.RentableItemId));
            Assert.That(dbContractRentableItem.WorkflowState, Is.EqualTo(Constants.WorkflowStates.Removed));
        }
    }
}