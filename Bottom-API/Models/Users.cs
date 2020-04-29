using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bottom_API.Models
{
    public class Users
    {
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [Required]
        [StringLength(255)]
        public string Password { get; set; }
        [Required]
        [StringLength(30)]
        public string Username { get; set; }
        [StringLength(10)]
        public string Nik { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<RoleUser> RoleUser { get; set; }
    }
}