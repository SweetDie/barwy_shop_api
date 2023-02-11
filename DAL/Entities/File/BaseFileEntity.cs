using System.ComponentModel.DataAnnotations;

namespace DAL.Entities.Image;

    public class BaseFileEntity : BaseEntity<Guid>
    {
        [Required, MaxLength(50)]
        public string FileName { get; set; }
        [Required, MaxLength(200)]
        public string Path { get; set; }
        [Required, MaxLength(255)]
        public string FullName { get; set; }
    }