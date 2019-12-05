using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microting.eForm.Infrastructure.Constants;
using Microting.eFormApi.BasePn.Infrastructure.Database.Base;

namespace Microting.eFormRentableItemBase.Infrastructure.Data.Entities
{
    public class ContractRentableItem : BaseEntity
    {
        [ForeignKey("RentableItem")]
        public int RentableItemId { get; set; }
        
        [ForeignKey("Contract")]
        public int ContractId { get; set; }

        public virtual RentableItem RentableItem { get; set; }

        public async Task Create(eFormRentableItemPnDbContext dbContext)
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            Version = 1;
            WorkflowState = Constants.WorkflowStates.Created;

            await dbContext.ContractRentableItem.AddAsync(this);
            await dbContext.SaveChangesAsync();

            await dbContext.ContractRentableItemVersion.AddAsync(MapVersion(this));
            await dbContext.SaveChangesAsync();
        }

        public async Task Update(eFormRentableItemPnDbContext dbContext)
        {
            ContractRentableItem contractRentableItem =
                await dbContext.ContractRentableItem.FirstOrDefaultAsync(x => x.Id == Id);

            if (contractRentableItem == null)
            {
                throw new NullReferenceException($"Could not find ContractRentableItem with Id:{Id}");
            }

            contractRentableItem.ContractId = ContractId;
            contractRentableItem.RentableItemId = RentableItemId;

            if (dbContext.ChangeTracker.HasChanges())
            {
                contractRentableItem.UpdatedAt = DateTime.Now;
                contractRentableItem.UpdatedByUserId = UpdatedByUserId;
                contractRentableItem.Version += 1;

                await dbContext.ContractRentableItemVersion.AddAsync(MapVersion(contractRentableItem));
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task Delete(eFormRentableItemPnDbContext dbContext)
        {
            ContractRentableItem contractRentableItem =
                await dbContext.ContractRentableItem.FirstOrDefaultAsync(x => x.Id == Id);

            if (contractRentableItem == null)
            {
                throw new NullReferenceException($"Could not find ContractRentableItem with Id:{Id}");
            }

            contractRentableItem.WorkflowState = Constants.WorkflowStates.Removed;

            if (dbContext.ChangeTracker.HasChanges())
            {
                contractRentableItem.UpdatedAt = DateTime.Now;
                contractRentableItem.UpdatedByUserId = UpdatedByUserId;
                contractRentableItem.Version += 1;

                await dbContext.ContractRentableItemVersion.AddAsync(MapVersion(contractRentableItem));
                await dbContext.SaveChangesAsync();
            }
        }

        private ContractRentableItemVersion MapVersion(ContractRentableItem contractRentableItem)
        {
            ContractRentableItemVersion contractRentableItemVersion = new ContractRentableItemVersion
            {
                ContractId = contractRentableItem.ContractId,
                CreatedAt = contractRentableItem.CreatedAt,
                CreatedByUserId = contractRentableItem.CreatedByUserId,
                RentableItemId = contractRentableItem.RentableItemId,
                UpdatedAt = contractRentableItem.UpdatedAt,
                UpdatedByUserId = contractRentableItem.UpdatedByUserId,
                Version = contractRentableItem.Version,
                ContractRentableItemId = contractRentableItem.Id
            };

            return contractRentableItemVersion;
        }
    }

}