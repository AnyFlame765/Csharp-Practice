using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practica02.Model;

[Table(name: "department")]

public class DepartmentModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int iddepartment { get; set; }   

    [Required]
    public string namedepa { get; set; }
    
}