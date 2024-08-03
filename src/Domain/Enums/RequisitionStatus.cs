using System.ComponentModel;

namespace Domain.Enums
{
    public enum RequisitionStatus
    {
        [Description("Draft")]
        Draft,
        [Description("Pending")]
        Pending,
        [Description("In-Approval")]
        InApproval,
        [Description("Approved")]
        Approved,
        [Description("Rejected")]
        Rejected,
        [Description("Processed")]
        Processed,
        [Description("Closed")]
        Closed,
        [Description("PO Generated")]
        POGenerated,
        [Description("Grant Generated")]
        GrantGenerated,
        [Description("CA Generated")]
        CAGenerated,
        [Description("Voided")]
        Voided
    }
}
