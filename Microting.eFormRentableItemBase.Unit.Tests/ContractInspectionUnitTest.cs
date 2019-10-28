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
            
            ContractInspection contractInspection = new ContractInspection
            {
                ContractId = contract.Id,
                DoneAt = DateTime.Now,
                SDKCaseId = rnd.Next(1, 255),
                SiteId = rnd.Next(1, 255),
                Status = rnd.Next(1, 255)
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
            Assert.AreEqual(contractInspection.SDKCaseId, dbContractInspection.SDKCaseId);
            Assert.AreEqual(contractInspection.SiteId, dbContractInspection.SiteId);
            Assert.AreEqual(contractInspection.Status, dbContractInspection.Status);
            Assert.AreEqual(contractInspection.DoneAt.ToString(), dbContractInspection.DoneAt.ToString());
            Assert.AreEqual(contractInspection.WorkflowState, dbContractInspection.WorkflowState);
        }

        [Test]
        public async Task ContractInspection_Update_DoesUpdate()
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

            
            ContractInspection contractInspection = new ContractInspection
            {
                ContractId = contract.Id,
                DoneAt = DateTime.Now,
                SDKCaseId = rnd.Next(1, 255),
                SiteId = rnd.Next(1, 255),
                Status = rnd.Next(1, 255)
            };            
            await contractInspection.Create(DbContext);

            contractInspection.ContractId = contract2.Id;
            contractInspection.DoneAt = DateTime.Now.AddDays(1);
            contractInspection.SDKCaseId = rnd.Next(1, 255);
            contractInspection.Status = rnd.Next(1, 255);
            contractInspection.SiteId = rnd.Next(1, 255);
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
            Assert.AreEqual(contractInspection.SDKCaseId, dbContractInspection.SDKCaseId);
            Assert.AreEqual(contractInspection.Status, dbContractInspection.Status);
            Assert.AreEqual(contractInspection.DoneAt.ToString(), dbContractInspection.DoneAt.ToString());
            Assert.AreEqual(contractInspection.WorkflowState, dbContractInspection.WorkflowState);
        }

        [Test]
        public async Task ContractInspection_Delete_DoesDelete()
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
           
            ContractInspection contractInspection = new ContractInspection
            {
                ContractId = contract.Id,
                DoneAt = DateTime.Now,
                SDKCaseId = rnd.Next(1, 255),
                SiteId = rnd.Next(1, 255),
                Status = rnd.Next(1, 255)
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
            Assert.AreEqual(contractInspection.SDKCaseId, dbContractInspection.SDKCaseId);
            Assert.AreEqual(contractInspection.Status, dbContractInspection.Status);
            Assert.AreEqual(contractInspection.DoneAt.ToString(), dbContractInspection.DoneAt.ToString());
            Assert.AreEqual(Constants.WorkflowStates.Removed, dbContractInspection.WorkflowState);
        }
    }
}