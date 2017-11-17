using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace etohum.signup.Entities
{
    // when a user adds a freinds email, a new invitaion will be registered.
    public class Invitation
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("InviterId")]
        public AppUser Inviter { get; set; }
        public Guid InviterId { get; set; }

        [MaxLength(50)]
        public string Invitee { get; set; }
        public DateTimeOffset InvitedAt { get; set; }
    }
}
