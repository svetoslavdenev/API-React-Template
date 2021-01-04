namespace APIReactTemplate.Domain.Identity
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class JWT
    {
        [Key]
        public int Id { get; set; }

        public string Value { get; set; }

        public DateTime AddedOn { get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool IsActive { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
