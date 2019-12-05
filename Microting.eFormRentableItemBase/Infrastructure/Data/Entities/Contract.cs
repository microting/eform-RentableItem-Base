using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microting.eForm.Infrastructure.Constants;
using Microting.eFormApi.BasePn.Infrastructure.Database.Base;

namespace Microting.eFormRentableItemBase.Infrastructure.Data.Entities
{
    public class Contract : BaseEntity
    {
        public Contract()
        {
            this.ContractInspections = new HashSet<ContractInspection>();
        }
        
        public int? Status { get; set; }

        public DateTime? ContractStart { get; set; }

        public DateTime? ContractEnd { get; set; }

        public int CustomerId { get; set; }

        public int? ContractNr { get; set; }

        public virtual ICollection<ContractInspection> ContractInspections { get; set; }

        public async Task Create(eFormRentableItemPnDbContext dbContext)
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            Version = 1;
            WorkflowState = Constants.WorkflowStates.Created;

            await dbContext.Contract.AddAsync(this);
            await dbContext.SaveChangesAsync();

            await dbContext.ContractVersion.AddAsync(MapVersion(this));
            await dbContext.SaveChangesAsync();
        }

        public async Task Update(eFormRentableItemPnDbContext dbContext)
        {
            Contract contract = await dbContext.Contract.FirstOrDefaultAsync(x => x.Id == Id);

            if (contract == null)
            {
                throw new NullReferenceException($"Could not find Contract with id {Id}");
            }
            contract.CustomerId = CustomerId;
            contract.WorkflowState = contract.WorkflowState;
            contract.ContractNr = ContractNr;
            contract.ContractStart = ContractStart;
            contract.ContractEnd = ContractEnd;

            if (dbContext.ChangeTracker.HasChanges())
            {
                contract.UpdatedAt = DateTime.Now;
                contract.UpdatedByUserId = UpdatedByUserId;
                contract.Version += 1;

                await dbContext.ContractVersion.AddAsync(MapVersion(contract));
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task Delete(eFormRentableItemPnDbContext dbContext)
        {
            Contract contract = await dbContext.Contract.FirstOrDefaultAsync(x => x.Id == Id);

            if (contract == null)
            {
                throw new NullReferenceException($"Could not find Contract with id {Id}");
            }

            contract.WorkflowState = Constants.WorkflowStates.Removed;
            
            if (dbContext.ChangeTracker.HasChanges())
            {
                contract.UpdatedAt = DateTime.Now;
                contract.UpdatedByUserId = UpdatedByUserId;
                contract.Version += 1;

                await dbContext.ContractVersion.AddAsync(MapVersion(contract));
                await dbContext.SaveChangesAsync();
            }
        }


        private ContractVersion MapVersion(Contract contract)
        {
            ContractVersion contractVersion = new ContractVersion
            {
                Version = contract.Version,
                Status = contract.Status,
                ContractStart = contract.ContractStart,
                ContractEnd = contract.ContractEnd,
                CustomerId = contract.CustomerId,
                ContractNr = contract.ContractNr,
                CreatedAt = contract.CreatedAt,
                UpdatedAt = contract.UpdatedAt,
                WorkflowState = contract.WorkflowState,
                CreatedByUserId = contract.CreatedByUserId,
                UpdatedByUserId = contract.UpdatedByUserId,
                ContractId = contract.Id
            };

            return contractVersion;
        }
    }
}