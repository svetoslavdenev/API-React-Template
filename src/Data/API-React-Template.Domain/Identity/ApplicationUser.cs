namespace APIReactTemplate.Domain.Identity
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    using APIReactTemplate.Domain.Common.Interfaces;

    public class ApplicationUser : IdentityUser, IDeletable, ITimeTrackable
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.JWTokens = new HashSet<JWT>();
        }

        public virtual ICollection<JWT> JWTokens { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime AddedOn { get; set; }

        public DateTime LastModifiedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
