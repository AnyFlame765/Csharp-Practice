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
    [Column(name: "name")]
    public string Name { get; set; }
    
    [Required]
    [MaxLength(30)]
    [Column(name: "lastname")]
    public string Lastname { get; set; }
    
    [Required]
    [Column(name: "departmentid")]
    public int iddepartment { get; set; }
    
    //relacionamos la tabla 
    [ForeignKey("iddepartment")]
    public DepartmentModel Department { get; set; }
    
} 