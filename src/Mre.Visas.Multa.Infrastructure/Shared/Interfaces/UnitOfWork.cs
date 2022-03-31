using Mre.Visas.Multa.Application.Multa.Repositories;
using Mre.Visas.Multa.Application.Shared.Interfaces;
using Mre.Visas.Multa.Infrastructure.Persistence.Contexts;
using System;
using System.Threading.Tasks;

namespace Mre.Visas.Multa.Infrastructure.Shared.Interfaces
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Constructors

        public UnitOfWork(
            ApplicationDbContext context,
            IMultaRepository multaRepository)
        {
            _context = context;
            MultaRepository = multaRepository;
        }

        #endregion Constructors

        #region Attributes

        protected readonly ApplicationDbContext _context;

        #endregion Attributes

        #region Properties

        public IMultaRepository MultaRepository { get; }

        #endregion Properties

        #region Methods

        public async Task<(bool, string)> SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.InnerException is null ? ex.Message : ex.InnerException.Message);
            }
        }

        #endregion Methods
    }
}