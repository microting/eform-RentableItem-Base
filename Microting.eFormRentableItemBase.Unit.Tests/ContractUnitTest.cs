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
    public class ContractUnitTest : DbTestFixture
    {
        [Test]
        public async Task Contract_Create_DoesCreate()
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

            //Act
            await contract.Create(DbContext);

            Contract dbContract = DbContext.Contract.AsNoTracking().First();
            List<Contract> contractList = DbContext.Contract.AsNoTracking().ToList();
            List<ContractVersion> versionList = DbContext.ContractVersion.AsNoTracking().ToList();

            //Assert
            Assert.NotNull(dbContract);

            Assert.AreEqual(1, contractList.Count);
            Assert.AreEqual(1, versionList.Count);

            Assert.AreEqual(contract.Status, dbContract.Status);
            Assert.AreEqual(contract.ContractEnd.ToString(), dbContract.ContractEnd.ToString());
            Assert.AreEqual(contract.ContractNr, dbContract.ContractNr);
            Assert.AreEqual(contract.ContractStart.ToString(), dbContract.ContractStart.ToString());
            Assert.AreEqual(contract.CustomerId, dbContract.CustomerId);
        }

        [Test]
        public async Task Contract_Update_DoesUpdate()
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

            contract.Status = 100;
            contract.ContractEnd = DateTime.Now;
            contract.ContractNr = rnd.Next(1, 255);
            contract.ContractStart = DateTime.Now;
            contract.CustomerId = rnd.Next(1, 255);

            //Act
            await contract.Update(DbContext);

            Contract dbContract = DbContext.Contract.AsNoTracking().First();
            List<Contract> contractList = DbContext.Contract.AsNoTracking().ToList();
            List<ContractVersion> versionList = DbContext.ContractVersion.AsNoTracking().ToList();

            //Assert
            Assert.NotNull(dbContract);

            Assert.AreEqual(1, contractList.Count);
            Assert.AreEqual(2, versionList.Count);

            Assert.AreEqual(contract.Status, dbContract.Status);
            Assert.AreEqual(contract.ContractEnd.ToString(), dbContract.ContractEnd.ToString());
            Assert.AreEqual(contract.ContractNr, dbContract.ContractNr);
            Assert.AreEqual(contract.ContractStart.ToString(), dbContract.ContractStart.ToString());
            Assert.AreEqual(contract.CustomerId, dbContract.CustomerId);
        }

        [Test]
        public async Task Contract_Delete_DoesDelete()
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

            //Act
            await contract.Delete(DbContext);

            Contract dbContract = DbContext.Contract.AsNoTracking().First();
            List<Contract> contractList = DbContext.Contract.AsNoTracking().ToList();
            List<ContractVersion> versionList = DbContext.ContractVersion.AsNoTracking().ToList();

            //Assert
            Assert.NotNull(dbContract);

            Assert.AreEqual(1, contractList.Count);
            Assert.AreEqual(2, versionList.Count);

            Assert.AreEqual(contract.Status, dbContract.Status);
            Assert.AreEqual(contract.ContractEnd.ToString(), dbContract.ContractEnd.ToString());
            Assert.AreEqual(contract.ContractNr, dbContract.ContractNr);
            Assert.AreEqual(contract.ContractStart.ToString(), dbContract.ContractStart.ToString());
            Assert.AreEqual(contract.CustomerId, dbContract.CustomerId);
            Assert.AreEqual(Constants.WorkflowStates.Removed, dbContract.WorkflowState);
        }
    }
}