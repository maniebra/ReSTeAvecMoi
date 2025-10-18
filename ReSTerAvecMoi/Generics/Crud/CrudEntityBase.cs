using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReSTerAvecMoi.Generics.Crud;

public abstract partial class CrudEntityBase<TKey>
where TKey : IComparable<TKey>, IEquatable<TKey>
{
    [Key]
    public virtual TKey Id { get; init; }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTimeOffset CreatedAt { get; init; }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTimeOffset? UpdatedAt { get; set; }

    public bool Diff(CrudEntityBase<TKey> other)
    {
        // TODO: COMPLETE THIS
        return true;
    }
}