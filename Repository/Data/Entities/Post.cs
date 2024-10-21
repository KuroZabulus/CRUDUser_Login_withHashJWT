using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Entities
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public DateTime PostedDate { get; set; }

        [ForeignKey("UserId")]
        public User? Author { get; set; }

        public virtual ICollection<Comment>? Comments { get; set; }
    }
}
