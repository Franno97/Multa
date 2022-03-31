using Mre.Visas.Multa.Domain.Enums;
using System;

namespace Mre.Visas.Multa.Domain.Entities
{
    public class Multa : AuditableEntity
    {
        #region Constructors

        public Multa()
        {
        }

        #endregion Constructors

        #region Properties

        public string Estado { get; set; }

        public DateTime FechaRegistro { get; set; }

        public string Observacion { get; set; }

        public string TipoMulta { get; set; }

        public Guid TramiteId { get; set; }

        #endregion Properties
    }
}