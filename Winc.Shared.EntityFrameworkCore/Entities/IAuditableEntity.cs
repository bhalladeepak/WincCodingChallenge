using System;
using System.Collections.Generic;
using System.Text;

namespace Winc.Shared.EntityFrameworkCore.Entities
{
    public interface IAuditableEntity
    {
        Guid Id { get; set; }
        DateTime CreatedDate { get; set; }
        Guid CreatedBy { get; set; }
        DateTime? ModifiedDate { get; set; }
        Guid? ModifiedBy { get; set; }
    }
}
