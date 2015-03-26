using System;

namespace Domain
{
    public interface IAuditable
    {
        string CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        string LastUpdatedBy { get; set; }
        DateTime LastUpdatedDate { get; set; }
    }
}
