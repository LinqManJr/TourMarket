using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourMarket.Context;
using TourMarket.Models;

namespace TourMarket.Services
{
    public interface IUserService
    {
        Manager Authenticate(string login, string password);        
        Manager Create(Manager manager, string password);
        void Update(Manager manager, string password = null);
        void Delete(int id);
    }

    public class ManagerService : IUserService
    {
        private readonly MarketContext context;

        public ManagerService(MarketContext context)
        {
            this.context = context;
        }
        public Manager Authenticate(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return null;

            var manager = context.Managers.SingleOrDefault(x => x.Login == login);

            // check if username exists
            if (manager == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return manager;
        }

        public Manager Create(Manager manager, string password)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Manager user, string password = null)
        {
            throw new NotImplementedException();
        }
    }
}
