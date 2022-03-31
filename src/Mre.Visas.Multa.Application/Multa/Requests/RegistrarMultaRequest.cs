using Mre.Visas.Multa.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Mre.Visas.Multa.Application.Multa.Requests
{
  public class RegistrarMultaRequest
  {
    public Guid TramiteId { get; set; }
    public Guid UsuarioId { get; set; }

    public List<RegistrarMultasDetalleRequest> ListaDetalleMultas { get; set; }

  }


  public class RegistrarMultasDetalleRequest
  {
    public string Estado { get; set; }

    public DateTime FechaRegistro { get; set; }

    public string Observacion { get; set; }

    public string TipoMulta { get; set; }

  }


}