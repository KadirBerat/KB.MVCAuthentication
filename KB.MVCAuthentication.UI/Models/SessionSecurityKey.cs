namespace KB.MVCAuthentication.UI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SessionSecurityKey
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte KeyId { get; set; }

        public string TripleDESKey { get; set; }

        public string TripleDesIV { get; set; }
    }
}
