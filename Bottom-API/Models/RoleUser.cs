using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bottom_API.Models
{
    public class RoleUser
    {
        [Key]
        public long Id { get; set; }
        public DateTime CreateAt { get; set; }
        public long RoleId { get; set; }
        public DateTime UpdateAt { get; set; }
        public long UserId { get; set; }

        [ForeignKey(nameof(RoleId))]
        [InverseProperty(nameof(Roles.RoleUser))]
        public virtual Roles Role { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(Users.RoleUser))]
        public virtual Users User { get; set; }
    }
}