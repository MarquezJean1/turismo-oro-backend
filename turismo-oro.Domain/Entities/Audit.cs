using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace turismo_oro.Domain.Entities
{
    public class Audit
    {
        public bool Active { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(307)]
        public string? CreatedBy { get; set; }
        [MaxLength(100)]
        public string? CreatedIp { get; set; }

        public DateTime? UpdatedAt { get; set; }
        [MaxLength(307)]
        public string? UpdatedBy { get; set; }
        [MaxLength(100)]
        public string? UpdatedIp { get; set; }

        public DateTime? DeletedAt { get; set; }
        [MaxLength(307)]
        public string? DeletedBy { get; set; }
        [MaxLength(100)]
        public string? DeletedIp { get; set; }
    }
}
