using System.ComponentModel.DataAnnotations.Schema;
using Microting.eFormApi.BasePn.Infrastructure.Database.Base;

namespace Microting.eFormRentableItemBase.Infrastructure.Data.Entities
{
    public class ContractInspectionItemVersion : BaseEntity
    {
        public int ContractInspectionId { get; set; }
        
        public int RentableItemId { get; set; }
        
        public int SDKCaseId { get; set; }

        public int SiteId { get; set; }

        public int? Status { get; set; }
        
        [ForeignKey("ContractInspectionItem")]
        public int ContractInspectionItemId { get; set; }
    }
}