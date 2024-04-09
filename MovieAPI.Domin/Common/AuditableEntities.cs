using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAPI.Domin.Common
{
    public class AuditableEntities
    {
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? LastModified { get; set; }
        public DateTime? LastModifiedDate { get; set; }

    }
}
