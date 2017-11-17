using etohum.signup.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace etohum.signup.Services
{
    // the use of repository keeps the project clean and storage tech changable
    public class EtohumRepository : IEtohumRepository
    {
        private EtohumContext _context;

        // using dependency injection to reach database context
        public EtohumRepository(EtohumContext context)
        {
            _context = context;
        }
        // using IEnumerable, the developer will get a list of users as a result of a query.
        // if needed (for data shaping , eg ..) IQueriable could be sent, then the developer can add to that query.
        public IEnumerable<AppUser> GetAppUsers()
        {
            return _context.AppUsers
                .OrderBy(u => u.Name)
                .ToList();
        }
        public AppUser GetAppUser(Guid id, bool inculdeInvitations)
        {
            // memory
            if (inculdeInvitations)
            {
                return _context.AppUsers.Include(u => u.Invitations)
                    .Where(u => u.Id == id)
                    .FirstOrDefault();
            }
            return _context.AppUsers.Where(u => u.Id == id)
               .FirstOrDefault();
        }
        public bool AppUserEmailExists(string email)
        {
            return _context.AppUsers.Any(u => u.Email == email);
        }
        public void AddAppUser(AppUser user)
        {
            user.Id = Guid.NewGuid();
            _context.AppUsers.Add(user);
        }

        // in production, only used methods will be writtin here, no need for these in this projects concept
        // only for demo purposes
        public void UpdateAppUser(AppUser user)
        {
            // no code
        }
        public void DeleteAppUser(AppUser user)
        {
            _context.AppUsers.Remove(user);
        }

        public IEnumerable<Invitation> GetInvitations()
        {
            return _context.Invitations
                .OrderBy(i => i.InvitedAt)
                .ToList();
        }
        public Invitation GetInvitation(Guid id,bool includeInviter)
        {
            return _context.Invitations
                .Include(i => i.Inviter)
                .Where(i => i.Id == id)
                .FirstOrDefault();
        }
        public void AddInvitation(Invitation invitation)
        {
            invitation.Id = Guid.NewGuid();
            _context.Invitations.Add(invitation);
        }
        public void UpdateInvitation(Invitation invitation)
        {
            // no code
        }
        public void DeleteInvitation(Invitation invitation)
        {
            _context.Invitations.Remove(invitation);
        }

        public bool SaveChanges()
        {
            // save changes will send back the number of rows affected if the query was run succesfully.
            return _context.SaveChanges() >= 0;
        }
    }
}
