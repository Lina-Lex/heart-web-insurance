using System;

namespace Domain.Entities
{
    public class PassCode : BaseEntity
    {
        public string Email { get; set; }
        public string UniqueCodeId { get; set; } = Guid.NewGuid().ToString("N");
        public string CodeValue { get; set; }
        public DateTime InitiatedTime { get; set; }
        public DateTime? LastCreatedOrUpdated { get; set; }
        public bool IsApplied { get; set; }

        public PassCode()
        {
            InitiatedTime = DateTime.UtcNow;
        }
    }
}
