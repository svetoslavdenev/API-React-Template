namespace APIReactTemplate.Domain.Common.Interfaces
{
    using System;

    public interface ITimeTrackable
    {
        DateTime AddedOn { get; set; }

        DateTime LastModifiedOn { get; set; }
    }
}
