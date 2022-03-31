using Microsoft.AspNetCore.Mvc;
using Mre.Visas.Multa.Application.Multa.Commands;
using Mre.Visas.Multa.Application.Multa.Queries;
using Mre.Visas.Multa.Application.Multa.Requests;
using System.Threading.Tasks;

namespace Mre.Visas.Multa.Api.Controllers
{
  public class MultaController : BaseController
  {
    // POST: api/Multa/RegistrarMulta
    [HttpPost("RegistrarMulta")]
    [ActionName(nameof(RegistrarMultaAsync))]
    public async Task<IActionResult> RegistrarMultaAsync(RegistrarMultaRequest request) => Ok(await Mediator.Send(new RegistrarMultaCommand(request)).ConfigureAwait(false));

    // POST: api/Multa/ConsultarMultasPorTramiteId
    [HttpPost("ConsultarMultasPorTramiteId")]
    [ActionName(nameof(ConsultarMultasPorTramiteIdAsync))]
    public async Task<IActionResult> ConsultarMultasPorTramiteIdAsync(ConsultarMultasPorTramiteIdRequest request) => Ok(await Mediator.Send(new ConsultarMultasPorTramiteIdQuery(request)).ConfigureAwait(false));

    // POST: api/Multa/ConsultarMultasPorTramiteId
    [HttpPost("ConsultarMultasPorTramiteIdObservacion")]
    [ActionName(nameof(ConsultarMultasPorTramiteIdObservacionAsync))]
    public async Task<IActionResult> ConsultarMultasPorTramiteIdObservacionAsync(ConsultarMultasPorTramiteIdObservacionRequest request) => Ok(await Mediator.Send(new ConsultarMultasPorTramiteIdObservacionQuery(request)).ConfigureAwait(false));
  }
}