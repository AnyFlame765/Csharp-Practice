using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practica02.Model.Dto;

public class MProductDto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] [MaxLength(30)] public string nserie { get; set; }

    [Required] [MaxLength(50)] public string name { get; set; }

    public bool isaviable { get; set; }
    
    public DateTime date { get; set; }

    public int cantidad { get; set; }

}