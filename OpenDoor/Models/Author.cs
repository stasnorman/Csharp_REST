using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OpenDoor.Models;

[Table("authors")]
public partial class Author
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("bio", TypeName = "text")]
    public string? Bio { get; set; }

    [InverseProperty("Author")]
    public virtual ICollection<Comic> Comics { get; set; } = new List<Comic>();
}
