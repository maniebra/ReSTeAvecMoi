using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReSTerAvecMoi.Generics;

public class CrudEntityBase<TKey>
where TKey : IComparable<TKey>, IQueryable<TKey>
{
    [Key]
    public virtual TKey Id { get; init; }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTimeOffset CreatedAt { get; init; }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTimeOffset? UpdatedAt { get; set; }
}