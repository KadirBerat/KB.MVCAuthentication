namespace KB.MVCAuthentication.UI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserLog
    {
        public int UserLogId { get; set; }

        public short UserId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? LogDate { get; set; }

        [Required]
        [StringLength(10)]
        public string Status { get; set; }

        public virtual User User { get; set; }
    }
}
