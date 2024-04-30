using Location.Application.IRepository;

namespace Location.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        //1-Déclaration d'objets repository
        public ILocationRepository locationRepository { get; }

        //2-Constructeur DI
        public UnitOfWork(ILocationRepository locationRepositorie)
        {
            locationRepository = locationRepositorie;
        }
    }
}
