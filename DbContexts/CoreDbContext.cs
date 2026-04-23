using Microsoft.EntityFrameworkCore;
using EFCore.NamingConventions;
using Traverse.Models;
using Traverse.Models.Graph;

namespace Traverse.DbContexts
{
    public class CoreDbContext : DbContext
    {
        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options) {}
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Itinerary> Itineraries { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Transportation> Transportations { get; set; }
    }
}