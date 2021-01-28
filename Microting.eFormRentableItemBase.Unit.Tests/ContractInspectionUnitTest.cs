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
    public class ContractInspectionUnitTest : DbTestFixture
    {
        [Test]
        public async Task ContractInspection_Create_DoesCreate()
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

            ContractInspection contractInspection = new ContractInspection
            {
                ContractId = contract.Id,
                DoneAt = DateTime.Now,

            };
            //Act
            await contractInspection.Create(DbContext);

            ContractInspection dbContractInspection = DbContext.ContractInspection.AsNoTracking().First();
            List<ContractInspection> inspectionList = DbContext.ContractInspection.AsNoTracking().ToList();
            List<ContractInspectionVersion> versionList = DbContext.ContractInspectionVersion.AsNoTracking().ToList();
            //Assert

            Assert.NotNull(dbContractInspection);

            Assert.AreEqual(1, inspectionList.Count());

            Assert.AreEqual(1, versionList.Count());

            Assert.AreEqual(contractInspection.ContractId, dbContractInspection.ContractId);
            Assert.AreEqual(contractInspection.DoneAt.ToString(), dbContractInspection.DoneAt.ToString());
            Assert.AreEqual(contractInspection.WorkflowState, dbContractInspection.WorkflowState);
        }

        [Test]
        public async Task ContractInspection_Update_DoesUpdate()
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


            ContractInspection contractInspection = new ContractInspection
            {
                ContractId = contract.Id,
                DoneAt = DateTime.Now,

            };
            await contractInspection.Create(DbContext);

            contractInspection.ContractId = contract2.Id;
            contractInspection.DoneAt = DateTime.Now.AddDays(1);

            //Act
            await contractInspection.Update(DbContext);

            ContractInspection dbContractInspection = DbContext.ContractInspection.AsNoTracking().First();
            List<ContractInspection> inspectionList = DbContext.ContractInspection.AsNoTracking().ToList();
            List<ContractInspectionVersion> versionList = DbContext.ContractInspectionVersion.AsNoTracking().ToList();
            //Assert

            Assert.NotNull(dbContractInspection);

            Assert.AreEqual(1, inspectionList.Count());

            Assert.AreEqual(2, versionList.Count());

            Assert.AreEqual(contractInspection.ContractId, dbContractInspection.ContractId);

            Assert.AreEqual(contractInspection.DoneAt.ToString(), dbContractInspection.DoneAt.ToString());
            Assert.AreEqual(contractInspection.WorkflowState, dbContractInspection.WorkflowState);
        }

        [Test]
        public async Task ContractInspection_Delete_DoesDelete()
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

            ContractInspection contractInspection = new ContractInspection
            {
                ContractId = contract.Id,
                DoneAt = DateTime.Now,

            };
            await contractInspection.Create(DbContext);


            //Act
            await contractInspection.Delete(DbContext);

            ContractInspection dbContractInspection = DbContext.ContractInspection.AsNoTracking().First();
            List<ContractInspection> inspectionList = DbContext.ContractInspection.AsNoTracking().ToList();
            List<ContractInspectionVersion> versionList = DbContext.ContractInspectionVersion.AsNoTracking().ToList();
            //Assert

            Assert.NotNull(dbContractInspection);

            Assert.AreEqual(1, inspectionList.Count());

            Assert.AreEqual(2, versionList.Count());

            Assert.AreEqual(contractInspection.ContractId, dbContractInspection.ContractId);

            Assert.AreEqual(contractInspection.DoneAt.ToString(), dbContractInspection.DoneAt.ToString());
            Assert.AreEqual(Constants.WorkflowStates.Removed, dbContractInspection.WorkflowState);
        }
    }
}