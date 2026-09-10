//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using A2Template.Dtos;
using A2Template.Models;

namespace A2Template.Data
{
    public interface IA2Repo
    {
        public void Register(User user);
        public Sign? GetSign(string id);
         public void AddEvent(Event eventInput);
        public int GetEventCount();
        public Event GetEvent(int id);
        public bool ValidUserLogin(string userName, string password);
        public bool ValidOrganizerLogin(string name, string password);
        public bool UserExists(string userName);   
    }
}

