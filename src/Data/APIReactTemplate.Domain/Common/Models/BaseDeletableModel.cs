namespace APIReactTemplate.Domain.Common.Models
{
    using System;

    using APIReactTemplate.Domain.Common.Interfaces;

    public abstract class BaseDeletableModel<TKey> : BaseModel<TKey>, IDeletable
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
