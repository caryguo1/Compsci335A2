//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using A2Template.Models;
using A2Template.Dtos;

namespace A2Template.Data
{
    public class A2Repo : IA2Repo
    {
        private readonly A1DbContext _dbContext;

        public A2Repo(A1DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Register(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public Sign? GetSign(string id)
        {
            Sign sign = _dbContext.Signs.FirstOrDefault(s => s.Id == id);
            return sign;
        }
        
        public void AddEvent(Event eventInput)
        {
            _dbContext.Events.Add(eventInput);
            _dbContext.SaveChanges();
        }

        public int GetEventCount()
        {
            return _dbContext.Events.Count();
        }
        
        public Event GetEvent(int id)
        {
            Event retrievedEvent = _dbContext.Events.FirstOrDefault(e => e.Id == id);
            return retrievedEvent;
        }

        public bool ValidUserLogin(string userName, string password)
        {
            User user = _dbContext.Users.FirstOrDefault(u => u.UserName == userName && u.Password == password);
            if (user == null)
                return false;
            else
                return true;
        }

        public bool ValidOrganizerLogin(string name, string password)
        {
            Organizer organizer = _dbContext.Organizers.FirstOrDefault(o => o.Name == name && o.Password == password);
            if (organizer == null)
                return false;
            else
                return true;
        }

        public bool UserExists(string userName)
        {
            User user = _dbContext.Users.FirstOrDefault(u => u.UserName == userName);
            if (user == null)
                return false;
            else
                return true;
        }
    }
}
