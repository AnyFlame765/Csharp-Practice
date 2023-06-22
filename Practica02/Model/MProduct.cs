using System.ComponentModel.DataAnnotations;

namespace Practica02.Model;

public class MProduct
{
    [Key] 
    public int Id  { get; set; }
    
    [Required]
    public string nserie { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string name { get; set; }
    
    [Required]
    public bool isaviable { get; set; }
    
    public DateTime date { get; set; }
    
    [Required]
    public int cantidad { get; set; }
    
    
}