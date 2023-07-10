using bird_trading.Core.Services.Interfaces;
using bird_trading.Data.Repositories.Interfaces;

namespace bird_trading.Core.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;
        public RoleService(IRoleRepository repository)
        {
            _repository = repository;
        }

        public object Get()
        {
            return _repository.Get();
        }
    }
}