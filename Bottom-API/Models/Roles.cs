using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bottom_API.Models
{
    public class Roles
    {
        [Key]
        public long Id { get; set; }
        public DateTime CreateAt { get; set; }
        [Required]
        [StringLength(20)]
        public string RoleName { get; set; }
        [Required]
        [StringLength(50)]
        public string RoleUnique { get; set; }
        public DateTime UpdateAt { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<RoleUser> RoleUser { get; set; }
    }
}