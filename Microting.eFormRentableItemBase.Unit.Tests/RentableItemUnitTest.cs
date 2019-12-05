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
    public class RentableItemUnitTest : DbTestFixture
    {
        [Test]
        public async Task RentableItem_Create_DoesCreate()
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
            //Act
            await rentableItem.Create(DbContext);

            RentableItem dbRentableItem = DbContext.RentableItem.AsNoTracking().First();
            List<RentableItem> itemList = DbContext.RentableItem.AsNoTracking().ToList();
            List<RentableItemVersion> versionList = DbContext.RentableItemsVersion.AsNoTracking().ToList();
            // Assert
            Assert.NotNull(rentableItem);

            Assert.AreEqual(1, itemList.Count());

            Assert.AreEqual(1, versionList.Count());

            Assert.AreEqual(rentableItem.Brand, dbRentableItem.Brand);
            Assert.AreEqual(rentableItem.ModelName, dbRentableItem.ModelName);
            Assert.AreEqual(rentableItem.PlateNumber, dbRentableItem.PlateNumber);
            Assert.AreEqual(rentableItem.VinNumber, dbRentableItem.VinNumber);
            Assert.AreEqual(rentableItem.SerialNumber, dbRentableItem.SerialNumber);
            Assert.AreEqual(rentableItem.RegistrationDate.ToString(), dbRentableItem.RegistrationDate.ToString());
            Assert.AreEqual(rentableItem.WorkflowState, dbRentableItem.WorkflowState);
        }

        [Test]
        public async Task RentableItem_Update_DoesUpdate()
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
            //Act
            rentableItem.Brand = Guid.NewGuid().ToString();
            rentableItem.ModelName = Guid.NewGuid().ToString();
            rentableItem.VinNumber = Guid.NewGuid().ToString();
            rentableItem.SerialNumber = Guid.NewGuid().ToString();
            rentableItem.PlateNumber = Guid.NewGuid().ToString();
            rentableItem.RegistrationDate = DateTime.Now.AddDays(1);

            await rentableItem.Update(DbContext);
            
            RentableItem dbRentableItem = DbContext.RentableItem.AsNoTracking().First();
            List<RentableItem> itemList = DbContext.RentableItem.AsNoTracking().ToList();
            List<RentableItemVersion> versionList = DbContext.RentableItemsVersion.AsNoTracking().ToList();
            // Assert
            Assert.NotNull(rentableItem);

            Assert.AreEqual(1, itemList.Count());

            Assert.AreEqual(2, versionList.Count());

            Assert.AreEqual(rentableItem.Brand, dbRentableItem.Brand);
            Assert.AreEqual(rentableItem.ModelName, dbRentableItem.ModelName);
            Assert.AreEqual(rentableItem.PlateNumber, dbRentableItem.PlateNumber);
            Assert.AreEqual(rentableItem.VinNumber, dbRentableItem.VinNumber);
            Assert.AreEqual(rentableItem.SerialNumber, dbRentableItem.SerialNumber);
            Assert.AreEqual(rentableItem.RegistrationDate.ToString(), dbRentableItem.RegistrationDate.ToString());
            Assert.AreEqual(rentableItem.WorkflowState, dbRentableItem.WorkflowState);
        }

        [Test]
        public async Task RentableItem_Delete_DoesDelete()
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
            //Act
            await rentableItem.Delete(DbContext);

            RentableItem dbRentableItem = DbContext.RentableItem.AsNoTracking().First();
            List<RentableItem> itemList = DbContext.RentableItem.AsNoTracking().ToList();
            List<RentableItemVersion> versionList = DbContext.RentableItemsVersion.AsNoTracking().ToList();
            // Assert
            Assert.NotNull(rentableItem);

            Assert.AreEqual(1, itemList.Count());

            Assert.AreEqual(2, versionList.Count());

            Assert.AreEqual(rentableItem.Brand, dbRentableItem.Brand);
            Assert.AreEqual(rentableItem.ModelName, dbRentableItem.ModelName);
            Assert.AreEqual(rentableItem.PlateNumber, dbRentableItem.PlateNumber);
            Assert.AreEqual(rentableItem.VinNumber, dbRentableItem.VinNumber);
            Assert.AreEqual(rentableItem.SerialNumber, dbRentableItem.SerialNumber);
            Assert.AreEqual(rentableItem.RegistrationDate.ToString(), dbRentableItem.RegistrationDate.ToString());
            Assert.AreEqual(Constants.WorkflowStates.Removed, dbRentableItem.WorkflowState);   
        }
    }
}