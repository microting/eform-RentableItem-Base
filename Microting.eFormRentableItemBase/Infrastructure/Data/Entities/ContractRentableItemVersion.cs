using System.ComponentModel.DataAnnotations.Schema;
using Microting.eFormApi.BasePn.Infrastructure.Database.Base;

namespace Microting.eFormRentableItemBase.Infrastructure.Data.Entities
{
    public class ContractRentableItemVersion : BaseEntity
    {
        public int RentableItemId { get; set; }

        public int ContractId { get; set; }

        [ForeignKey("ContractRentableItem")]
        public int ContractRentableItemId { get; set; }
    }
}