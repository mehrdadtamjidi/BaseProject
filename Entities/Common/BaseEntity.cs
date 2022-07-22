using System.ComponentModel.DataAnnotations;

namespace Entities.Common
{
    public interface IEntity
    {
    }
    public abstract class BaseEntity<TKey> : IEntity
    {
        [Key]
        public TKey Id { get; set; }

        public bool IsDelete { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LastUpdateDate { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<long>
    {
    }
}
