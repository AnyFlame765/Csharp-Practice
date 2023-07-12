using System.ComponentModel.DataAnnotations;

namespace Practica02.Model.Dto;

public class DepartmentDTO
{
    public int Id_Depa { get; set; }
    
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }
    
}