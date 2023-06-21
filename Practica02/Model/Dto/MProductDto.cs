using System.ComponentModel.DataAnnotations;

namespace Practica02.Model.Dto;

public class MProductDto
{
    public int Id { get; set; }

    [Required] [MaxLength(30)] public string nserie { get; set; }

    [Required] [MaxLength(30)] public string name { get; set; }

    public bool isaviable { get; set; }

    public int cantidad { get; set; }

}