using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public interface IEntity<T>
    {
        T Id { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }
    }

    public abstract class BaseEntity<T> : IEntity<T>
    {
        [Key]
        public T Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime DateModified { get; set; } = DateTime.Now.ToUniversalTime();
    }
}