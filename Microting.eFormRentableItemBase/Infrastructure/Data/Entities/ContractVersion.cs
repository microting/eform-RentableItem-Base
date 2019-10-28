using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microting.eFormApi.BasePn.Infrastructure.Database.Base;

namespace Microting.eFormRentableItemBase.Infrastructure.Data.Entities
{
    public class ContractVersion : BaseEntity
    {
        public int? Status { get; set; }

        public DateTime? ContractStart { get; set; }

        public DateTime? ContractEnd { get; set; }

        public int CustomerId { get; set; }

        public int? ContractNr { get; set; }

        [ForeignKey("Contract")]
        public int ContractId { get; set; }
    }
}