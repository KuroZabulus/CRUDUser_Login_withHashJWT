using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string? Address { get; set; }
        public string? ImageAvatar { get; set; }
        public string? Status { get; set; }

        [ForeignKey("RoleId")]
        public Role? Role { get; set; }

        public virtual ICollection<Post>? Posts { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }

    }
}
