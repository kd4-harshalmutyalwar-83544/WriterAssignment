using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WriterAssign.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Column("Title", TypeName = "varchar(200)")]
        [Required]
        public string ? Title { get; set; }

        [Column("Content", TypeName = "text")]
        [Required]
        public string ? Content { get; set; }

        [Column("PublishedDate")]
        public DateTime PublishedDate { get; set; } = DateTime.Now;
    }
    }

