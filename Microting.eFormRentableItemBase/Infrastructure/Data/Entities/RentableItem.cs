using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microting.eForm.Infrastructure.Constants;
using Microting.eFormApi.BasePn.Infrastructure.Database.Base;

namespace Microting.eFormRentableItemBase.Infrastructure.Data.Entities
{
    public class RentableItem : BaseEntity
    {
        public RentableItem()
        {
            this.ContractRentableItems = new HashSet<ContractRentableItem>();
        }

        [StringLength(100)]
        public string Brand { get; set; }

        [StringLength(100)]
        public string ModelName { get; set; }

        public DateTime RegistrationDate { get; set; }

        public string VinNumber { get; set; }

        public string SerialNumber { get; set; }

        public string PlateNumber { get; set; }
        
        public int eFormId { get; set; }

        public virtual ICollection<ContractRentableItem> ContractRentableItems { get; set; }


        public async Task Create(eFormRentableItemPnDbContext dbContext)
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            Version = 1;
            WorkflowState = Constants.WorkflowStates.Created;

            await dbContext.RentableItem.AddAsync(this);
            await dbContext.SaveChangesAsync();

            await dbContext.RentableItemsVersion.AddAsync(MapVersion(this));
            await dbContext.SaveChangesAsync();
        }

        public async Task Update(eFormRentableItemPnDbContext dbContext)
        {
            RentableItem rentableItem = await dbContext.RentableItem.FirstOrDefaultAsync(x => x.Id == Id);
            
            if (rentableItem == null)
            {
                throw new NullReferenceException($"Could not find RentableItem with id {Id}");
            }
            
            rentableItem.Brand = Brand;
            rentableItem.ModelName = ModelName;
            rentableItem.RegistrationDate = RegistrationDate;
            rentableItem.VinNumber = VinNumber;
            rentableItem.SerialNumber = SerialNumber;
            rentableItem.PlateNumber = PlateNumber;
            rentableItem.eFormId = eFormId;
            
            if (dbContext.ChangeTracker.HasChanges())
            {
                rentableItem.UpdatedAt = DateTime.Now;
                rentableItem.UpdatedByUserId = UpdatedByUserId;
                rentableItem.Version += 1;

                await dbContext.RentableItemsVersion.AddAsync(MapVersion(rentableItem));
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task Delete(eFormRentableItemPnDbContext dbContext)
        {
            RentableItem rentableItem = await dbContext.RentableItem.FirstOrDefaultAsync(x => x.Id == Id);
            
            if (rentableItem == null)
            {
                throw new NullReferenceException($"Could not find RentableItem with id {Id}");
            }

            rentableItem.WorkflowState = Constants.WorkflowStates.Removed;

            if (dbContext.ChangeTracker.HasChanges())
            {
                rentableItem.UpdatedAt = DateTime.Now;
                rentableItem.UpdatedByUserId = UpdatedByUserId;
                rentableItem.Version += 1;

                await dbContext.RentableItemsVersion.AddAsync(MapVersion(rentableItem));
                await dbContext.SaveChangesAsync();
            }
        }
        
        private RentableItemVersion MapVersion(RentableItem rentableItem)
        {
            RentableItemVersion rentableItemVersion = new RentableItemVersion
            {
                Brand = rentableItem.Brand,
                ModelName = rentableItem.ModelName,
                RegistrationDate = rentableItem.RegistrationDate,
                VinNumber = rentableItem.VinNumber,
                SerialNumber = rentableItem.SerialNumber,
                PlateNumber = rentableItem.PlateNumber,
                CreatedAt = rentableItem.CreatedAt,
                CreatedByUserId = rentableItem.CreatedByUserId,
                UpdatedAt = rentableItem.UpdatedAt,
                UpdatedByUserId = rentableItem.UpdatedByUserId,
                Version = rentableItem.Version,
                WorkflowState = rentableItem.WorkflowState,
                RentableItemId = rentableItem.Id
            };

            return rentableItemVersion;
        }
    }
}