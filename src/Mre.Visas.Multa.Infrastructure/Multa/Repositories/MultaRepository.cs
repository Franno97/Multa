using Mre.Visas.Multa.Application.Multa.Repositories;
using Mre.Visas.Multa.Infrastructure.Persistence.Contexts;
using Mre.Visas.Multa.Infrastructure.Shared.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Mre.Visas.Multa.Infrastructure.Multa.Repositories
{
  public class MultaRepository : Repository<Domain.Entities.Multa>, IMultaRepository
  {
    public MultaRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<List<Domain.Entities.Multa>> GetByTramiteIdAsync(Guid tramiteId)
    {
      return await _context.Multas
          .Where(e => e.TramiteId.Equals(tramiteId) && !e.IsDeleted)
          .ToListAsync();
    }

    public async Task<List<Domain.Entities.Multa>> GetByTramiteIdObservacionAsync(Guid tramiteId, string observacion)
    {
      return await _context.Multas
          .Where(e => e.TramiteId == tramiteId && e.Observacion == observacion && !e.IsDeleted)
          .ToListAsync();
    }
  }
}