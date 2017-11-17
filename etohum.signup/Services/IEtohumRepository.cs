using etohum.signup.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace etohum.signup.Services
{
    public interface IEtohumRepository
    {
        // using IEnumerable and IQuerable 
        IEnumerable<AppUser> GetAppUsers();
        AppUser GetAppUser(Guid id, bool inculdeInvitations);
        bool AppUserEmailExists(string email);
        void AddAppUser(AppUser user);
        void UpdateAppUser(AppUser user);
        void DeleteAppUser(AppUser user);

        IEnumerable<Invitation> GetInvitations();
        Invitation GetInvitation(Guid id, bool includeInviter);
        void AddInvitation(Invitation user);
        void UpdateInvitation(Invitation user);
        void DeleteInvitation(Invitation user);

        bool SaveChanges();
    }
}
