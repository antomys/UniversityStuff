using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gRPC.Server.Entities;

[Table(nameof(Product))]
public class ProductEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string ProductName { get; set; }
    
    public string ProductId { get; set; }
    
    public DateTimeOffset AddTime { get; set; }
}