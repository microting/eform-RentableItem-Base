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
            Assert.That(dbContract, Is.Not.Null);

            Assert.That(contractList.Count, Is.EqualTo(1));
            Assert.That(versionList.Count, Is.EqualTo(1));

            Assert.That(dbContract.Status, Is.EqualTo(contract.Status));
            Assert.That(dbContract.ContractEnd.ToString(), Is.EqualTo(contract.ContractEnd.ToString()));
            Assert.That(dbContract.ContractNr, Is.EqualTo(contract.ContractNr));
            Assert.That(dbContract.ContractStart.ToString(), Is.EqualTo(contract.ContractStart.ToString()));
            Assert.That(dbContract.CustomerId, Is.EqualTo(contract.CustomerId));
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
            Assert.That(dbContract, Is.Not.Null);

            Assert.That(contractList.Count, Is.EqualTo(1));
            Assert.That(versionList.Count, Is.EqualTo(2));

            Assert.That(dbContract.Status, Is.EqualTo(contract.Status));
            Assert.That(dbContract.ContractEnd.ToString(), Is.EqualTo(contract.ContractEnd.ToString()));
            Assert.That(dbContract.ContractNr, Is.EqualTo(contract.ContractNr));
            Assert.That(dbContract.ContractStart.ToString(), Is.EqualTo(contract.ContractStart.ToString()));
            Assert.That(dbContract.CustomerId, Is.EqualTo(contract.CustomerId));
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
            Assert.That(dbContract, Is.Not.Null);

            Assert.That(contractList.Count, Is.EqualTo(1));
            Assert.That(versionList.Count, Is.EqualTo(2));

            Assert.That(dbContract.Status, Is.EqualTo(contract.Status));
            Assert.That(dbContract.ContractEnd.ToString(), Is.EqualTo(contract.ContractEnd.ToString()));
            Assert.That(dbContract.ContractNr, Is.EqualTo(contract.ContractNr));
            Assert.That(dbContract.ContractStart.ToString(), Is.EqualTo(contract.ContractStart.ToString()));
            Assert.That(dbContract.CustomerId, Is.EqualTo(contract.CustomerId));
            Assert.That(dbContract.WorkflowState, Is.EqualTo(Constants.WorkflowStates.Removed));
        }
    }
}