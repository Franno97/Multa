using Mre.Visas.Multa.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mre.Visas.Multa.Application.Multa.Repositories
{
  public interface IMultaRepository : IRepository<Domain.Entities.Multa>
  {
    Task<List<Domain.Entities.Multa>> GetByTramiteIdAsync(Guid tramiteId);

    Task<List<Domain.Entities.Multa>> GetByTramiteIdObservacionAsync(Guid tramiteId, string observacion);
  }
}