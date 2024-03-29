﻿using System;
using System.Linq;
using TourMarket.Context;
using TourMarket.Helpers;
using TourMarket.Models;

namespace TourMarket.Services
{
    public interface IUserService
    {
        Manager Authenticate(string login, string password);        
        Manager Create(Manager manager, string password);
        Manager GetById(int id);
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
            
            if (manager == null)
                return null;
           
            if (!SecurityHelper.VerifyPasswordHash(password, manager.Password))
                return null;
            
            return manager;
        }

        public Manager Create(Manager manager, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required");

            if (context.Managers.Any(x => x.Login == manager.Login))
                throw new Exception($"Username {manager.Login} is already taken");

            byte[] passwordHash;
            SecurityHelper.CreatePasswordHash(password, out passwordHash);

            manager.Password = passwordHash;            

            context.Managers.Add(manager);
            context.SaveChanges();

            return manager;
        }

        public Manager GetById(int id) => context.Managers.Find(id);
    }
}
