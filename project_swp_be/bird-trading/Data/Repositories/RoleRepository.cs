using bird_trading.Data.Contexts;
using bird_trading.Data.Repositories.Interfaces;

namespace bird_trading.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly BirdContext _context;

        public RoleRepository(BirdContext context)
        {
            _context = context;
        }

        public object Get()
        {
            var query = from r in _context.Roles
                        select new {
                            Id = r.Id,
                            Name = r.Name,
                        };

            return query.ToList();
        }
    }
}