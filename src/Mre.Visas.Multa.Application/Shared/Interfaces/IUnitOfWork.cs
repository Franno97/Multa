using Mre.Visas.Multa.Application.Multa.Repositories;
using System.Threading.Tasks;

namespace Mre.Visas.Multa.Application.Shared.Interfaces
{
    public interface IUnitOfWork
    {
        IMultaRepository MultaRepository { get; }

        Task<(bool, string)> SaveChangesAsync();
    }
}