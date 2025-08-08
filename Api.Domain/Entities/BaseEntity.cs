using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        private DateTime? _createdAt;
        public DateTime? CreatedAt
        {
            get => _createdAt;
            set => _createdAt = value ?? DateTime.UtcNow;
        }

        private DateTime? UpdatedAt { get; set; }
      
    }
}