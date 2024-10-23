using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OpenDoor.Models;

[Table("comics")]
[Index("AuthorId", Name = "author_id")]
[Index("GenreId", Name = "genre_id")]
public partial class Comic
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("image_url")]
    [StringLength(255)]
    public string? ImageUrl { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("price")]
    [Precision(10, 2)]
    public decimal Price { get; set; }

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    [Column("author_id", TypeName = "int(11)")]
    public int? AuthorId { get; set; }

    [Column("genre_id", TypeName = "int(11)")]
    public int? GenreId { get; set; }

    [Column("published_date")]
    public DateTime? PublishedDate { get; set; }

    [Column("stock", TypeName = "int(11)")]
    public int? Stock { get; set; }

    [ForeignKey("AuthorId")]
    [InverseProperty("Comics")]
    public virtual Author? Author { get; set; }

    [ForeignKey("GenreId")]
    [InverseProperty("Comics")]
    public virtual Genre? Genre { get; set; }
}
