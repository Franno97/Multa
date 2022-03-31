using System;

namespace Mre.Visas.Multa.Application.Multa.Requests
{
  public class ConsultarMultasPorTramiteIdRequest
  {
    public Guid TramiteId { get; set; }
  }

  public class ConsultarMultasPorTramiteIdObservacionRequest
  {
    public Guid TramiteId { get; set; }

    public string Observacion { get; set; }
  }

}