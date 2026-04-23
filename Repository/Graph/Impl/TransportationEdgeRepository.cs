using Microsoft.EntityFrameworkCore;
using Traverse.DbContexts;
using Traverse.Models.Graph;

namespace Traverse.Repository.Graph.Impl
{
    public class TransportationEdgeRepository(CoreDbContext coreContext) : IEdgeRepository<Transportation>
    {
        private readonly CoreDbContext _coreContext = coreContext;
        public async Task<IEnumerable<Transportation>> GetEdgesAsync(long id)
        {
            return await _coreContext.Transportations
                .Where(t => t.ItineraryId == id)
                .ToListAsync();
        }

        public async Task SaveEdgesAsync(IEnumerable<Transportation> edges)
        {
            _coreContext.Add(edges);
            await _coreContext.SaveChangesAsync();
        }
    }
}