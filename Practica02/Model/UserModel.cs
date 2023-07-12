using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Practica02.Model;

[Table(name: "users")]
public class UserModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }
    
    [Required]
    [MaxLength(30)]
    public string Lastname { get; set; }
    
    [Required]
    public int iddepartment { get; set; }
    
    //relacionamos la tabla 
    [ForeignKey("iddepartment")]
    public DepartmentModel Department { get; set; }
    
} 