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
            Assert.That(rentableItem, Is.Not.Null);

            Assert.That(itemList.Count(), Is.EqualTo(1));

            Assert.That(versionList.Count(), Is.EqualTo(1));

            Assert.That(dbRentableItem.Brand, Is.EqualTo(rentableItem.Brand));
            Assert.That(dbRentableItem.ModelName, Is.EqualTo(rentableItem.ModelName));
            Assert.That(dbRentableItem.PlateNumber, Is.EqualTo(rentableItem.PlateNumber));
            Assert.That(dbRentableItem.VinNumber, Is.EqualTo(rentableItem.VinNumber));
            Assert.That(dbRentableItem.SerialNumber, Is.EqualTo(rentableItem.SerialNumber));
            Assert.That(dbRentableItem.RegistrationDate.ToString(), Is.EqualTo(rentableItem.RegistrationDate.ToString()));
            Assert.That(dbRentableItem.WorkflowState, Is.EqualTo(rentableItem.WorkflowState));
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
            Assert.That(rentableItem, Is.Not.Null);

            Assert.That(itemList.Count(), Is.EqualTo(1));

            Assert.That(versionList.Count(), Is.EqualTo(2));

            Assert.That(dbRentableItem.Brand, Is.EqualTo(rentableItem.Brand));
            Assert.That(dbRentableItem.ModelName, Is.EqualTo(rentableItem.ModelName));
            Assert.That(dbRentableItem.PlateNumber, Is.EqualTo(rentableItem.PlateNumber));
            Assert.That(dbRentableItem.VinNumber, Is.EqualTo(rentableItem.VinNumber));
            Assert.That(dbRentableItem.SerialNumber, Is.EqualTo(rentableItem.SerialNumber));
            Assert.That(dbRentableItem.RegistrationDate.ToString(), Is.EqualTo(rentableItem.RegistrationDate.ToString()));
            Assert.That(dbRentableItem.WorkflowState, Is.EqualTo(rentableItem.WorkflowState));
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
            Assert.That(rentableItem, Is.Not.Null);

            Assert.That(itemList.Count(), Is.EqualTo(1));

            Assert.That(versionList.Count(), Is.EqualTo(2));

            Assert.That(dbRentableItem.Brand, Is.EqualTo(rentableItem.Brand));
            Assert.That(dbRentableItem.ModelName, Is.EqualTo(rentableItem.ModelName));
            Assert.That(dbRentableItem.PlateNumber, Is.EqualTo(rentableItem.PlateNumber));
            Assert.That(dbRentableItem.VinNumber, Is.EqualTo(rentableItem.VinNumber));
            Assert.That(dbRentableItem.SerialNumber, Is.EqualTo(rentableItem.SerialNumber));
            Assert.That(dbRentableItem.RegistrationDate.ToString(), Is.EqualTo(rentableItem.RegistrationDate.ToString()));
            Assert.That(dbRentableItem.WorkflowState, Is.EqualTo(Constants.WorkflowStates.Removed));   
        }
    }
}