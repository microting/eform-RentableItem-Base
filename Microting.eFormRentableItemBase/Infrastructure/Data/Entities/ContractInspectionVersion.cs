using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microting.eFormApi.BasePn.Infrastructure.Database.Base;

namespace Microting.eFormRentableItemBase.Infrastructure.Data.Entities
{
    public class ContractInspectionVersion : BaseEntity
    {
        public DateTime? DoneAt { get; set; }

        public int SDKCaseId { get; set; }

        public int ContractId { get; set; }

        public int? Status { get; set; }

        [ForeignKey("ContractInspection")]
        public int ContractInspectionId { get; set; }

        public int SiteId { get; set; }

    }
}