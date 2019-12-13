using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microting.eForm.Infrastructure.Constants;
using Microting.eFormApi.BasePn.Infrastructure.Database.Base;

namespace Microting.eFormRentableItemBase.Infrastructure.Data.Entities
{
    public class ContractInspection : BaseEntity
    {
        
        public DateTime? DoneAt { get; set; }

        [ForeignKey("Contract")]
        public int ContractId { get; set; }
        

        public virtual Contract Contract { get; set; }

        public async Task Create(eFormRentableItemPnDbContext dbContext)
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            Version = 1;
            WorkflowState = Constants.WorkflowStates.Created;

            await dbContext.ContractInspection.AddAsync(this);
            await dbContext.SaveChangesAsync();

            await dbContext.ContractInspectionVersion.AddAsync(MapVersion(this));
            await dbContext.SaveChangesAsync();
        }

        public async Task Update(eFormRentableItemPnDbContext dbContext)
        {
            ContractInspection contractInspection =
                await dbContext.ContractInspection.FirstOrDefaultAsync(x => x.Id == Id);
            
            if (contractInspection == null)
            {
                throw new NullReferenceException($"Could not find Contract Inspection with id {Id}");
            }

            contractInspection.DoneAt = DoneAt;
           

            if (dbContext.ChangeTracker.HasChanges())
            {
                contractInspection.UpdatedAt = DateTime.Now;
                contractInspection.UpdatedByUserId = UpdatedByUserId;
                contractInspection.Version += 1;

                await dbContext.ContractInspectionVersion.AddAsync(MapVersion(contractInspection));
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task Delete(eFormRentableItemPnDbContext dbContext)
        {
            ContractInspection contractInspection =
                await dbContext.ContractInspection.FirstOrDefaultAsync(x => x.Id == Id);
            
            if (contractInspection == null)
            {
                throw new NullReferenceException($"Could not find Contract Inspection with id {Id}");
            }

            contractInspection.WorkflowState = Constants.WorkflowStates.Removed;
            if (dbContext.ChangeTracker.HasChanges())
            {
                contractInspection.UpdatedAt = DateTime.Now;
                contractInspection.UpdatedByUserId = UpdatedByUserId;
                contractInspection.Version += 1;

                await dbContext.ContractInspectionVersion.AddAsync(MapVersion(contractInspection));
                await dbContext.SaveChangesAsync();
            }
        }
        
        private ContractInspectionVersion MapVersion(ContractInspection contractInspection)
        {
            ContractInspectionVersion contractInspectionVer = new ContractInspectionVersion();

            contractInspectionVer.ContractId = contractInspection.ContractId;
            contractInspectionVer.CreatedAt = contractInspection.CreatedAt;
            contractInspectionVer.CreatedByUserId = contractInspection.CreatedByUserId;
            contractInspectionVer.DoneAt = contractInspection.DoneAt;
            contractInspectionVer.UpdatedAt = contractInspection.UpdatedAt;
            contractInspectionVer.UpdatedByUserId = contractInspection.UpdatedByUserId;
            contractInspectionVer.Version = contractInspection.Version;
            contractInspectionVer.WorkflowState = contractInspection.WorkflowState;
            
            contractInspectionVer.ContractInspectionId = contractInspection.Id;

            return contractInspectionVer;
        }
    }
}