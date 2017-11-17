using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace etohum.signup.Entities
{
    public class AppUser
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(20)]
        public string Surname { get; set; }
        [MaxLength(50)]
        [Required]
        public string Email { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        // one to many relation is for future wher a user can invite more of his friends.
        public ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();
    }
}
