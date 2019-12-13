using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microting.eForm.Infrastructure.Constants;
using Microting.eFormApi.BasePn.Infrastructure.Database.Base;

namespace Microting.eFormRentableItemBase.Infrastructure.Data.Entities
{
    public class ContractInspectionItem: BaseEntity
    {
        [ForeignKey("ContractInspection")]
        public int ContractInspectionId { get; set; }
        
        [ForeignKey("RentableItem")]
        public int RentableItemId { get; set; }
        
        public int SDKCaseId { get; set; }

        public int SiteId { get; set; }

        public int? Status { get; set; }

        public async Task Create(eFormRentableItemPnDbContext dbContext)
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            Version = 1;
            WorkflowState = Constants.WorkflowStates.Created;

            await dbContext.ContractInspectionItem.AddAsync(this);
            await dbContext.SaveChangesAsync();

            await dbContext.ContractInspectionItemVersion.AddAsync(MapVersion(this));
            await dbContext.SaveChangesAsync();
        }

        public async Task Update(eFormRentableItemPnDbContext dbContext)
        {
            ContractInspectionItem inspectionItem =
                await dbContext.ContractInspectionItem.FirstOrDefaultAsync(x => x.Id == Id);

            if (inspectionItem == null)
            {
                throw new NullReferenceException($"Could not find inspection item with id: {Id}");
            }

            inspectionItem.SiteId = SiteId;
            inspectionItem.ContractInspectionId = ContractInspectionId;
            inspectionItem.RentableItemId = RentableItemId;
            inspectionItem.Status = Status;
            inspectionItem.SDKCaseId = SDKCaseId;

            if (dbContext.ChangeTracker.HasChanges())
            {
                inspectionItem.UpdatedAt = DateTime.Now;
                inspectionItem.UpdatedByUserId = UpdatedByUserId;
                inspectionItem.Version += 1;

                await dbContext.ContractInspectionItemVersion.AddAsync(MapVersion(inspectionItem));
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task Delete(eFormRentableItemPnDbContext dbContext)
        {
            ContractInspectionItem inspectionItem =
                await dbContext.ContractInspectionItem.FirstOrDefaultAsync(x => x.Id == Id);

            if (inspectionItem == null)
            {
                throw new NullReferenceException($"Could not find inspection item with id: {Id}");
            }

            inspectionItem.WorkflowState = Constants.WorkflowStates.Removed;

            if (dbContext.ChangeTracker.HasChanges())
            {
                inspectionItem.UpdatedAt = DateTime.Now;
                inspectionItem.UpdatedByUserId = UpdatedByUserId;
                inspectionItem.Version += 1;

                await dbContext.ContractInspectionItemVersion.AddAsync(MapVersion(inspectionItem));
                await dbContext.SaveChangesAsync();
            }
        }


        private ContractInspectionItemVersion MapVersion(ContractInspectionItem contractInspectionItem)
        {
            ContractInspectionItemVersion contractInspectionItemVersion = new ContractInspectionItemVersion
            {
                ContractInspectionId = contractInspectionItem.ContractInspectionId,
                CreatedAt = contractInspectionItem.CreatedAt,
                CreatedByUserId = contractInspectionItem.CreatedByUserId,
                RentableItemId = contractInspectionItem.RentableItemId,
                SDKCaseId = contractInspectionItem.SDKCaseId,
                SiteId = contractInspectionItem.SiteId,
                Status = contractInspectionItem.Status,
                UpdatedAt = contractInspectionItem.UpdatedAt,
                UpdatedByUserId = contractInspectionItem.UpdatedByUserId,
                WorkflowState = contractInspectionItem.WorkflowState,
                ContractInspectionItemId = contractInspectionItem.Id
            };
            return contractInspectionItemVersion;
        }
    }
}