//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using A2Template.Models;
//using Microsoft.AspNetCore.Http;

namespace A2Template.Data
{
    public class A1DbContext : DbContext
    {
        public A1DbContext(DbContextOptions<A1DbContext> options) : base(options) {}
        /*
        public override int SaveChanges()
        {
            //var httpContext = _httpContextAccessor.HttpContext;
            //string? clientIp = httpContext?.Connection?.RemoteIpAddress?.ToString();

            var addedEntries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added && e.Entity is Comment);

            foreach (var entry in addedEntries)
            {
                Comment? comment = entry.Entity as Comment;

                if (comment != null)
                {
                    comment.Time = DateTime.UtcNow.ToString("yyyyMMddTHHmmssZ");
                    //comment.IP = clientIp ?? "Unknown";
                }
            }

            // 3. Save to database normally
            return base.SaveChanges();
            
        }
        */
        public DbSet<Event> Events { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Sign> Signs { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
