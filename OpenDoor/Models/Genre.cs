using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OpenDoor.Models;

[Table("genres")]
public partial class Genre
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("genre_name")]
    [StringLength(50)]
    public string GenreName { get; set; } = null!;

    [InverseProperty("Genre")]
    public virtual ICollection<Comic> Comics { get; set; } = new List<Comic>();
}
