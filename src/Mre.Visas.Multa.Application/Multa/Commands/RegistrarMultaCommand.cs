using FluentValidation;
using MediatR;
using Mre.Visas.Multa.Application.Wrappers;
using Mre.Visas.Multa.Application.Multa.Requests;
using Mre.Visas.Multa.Application.Shared.Handlers;
using Mre.Visas.Multa.Application.Shared.Interfaces;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Mre.Visas.Multa.Application.Multa.Commands
{
  public class RegistrarMultaCommand : RegistrarMultaRequest, IRequest<ApiResponseWrapper>
  {
    #region Constructors

    public RegistrarMultaCommand(RegistrarMultaRequest request)
    {
      UsuarioId = request.UsuarioId;
      TramiteId = request.TramiteId;
      ListaDetalleMultas = new List<RegistrarMultasDetalleRequest>();
      foreach (var registrarMultasDetalleRequest in request.ListaDetalleMultas)
      {
        ListaDetalleMultas.Add(new RegistrarMultasDetalleRequest
        {
          Estado = registrarMultasDetalleRequest.Estado,
          FechaRegistro = registrarMultasDetalleRequest.FechaRegistro,
          Observacion = registrarMultasDetalleRequest.Observacion,
          TipoMulta = registrarMultasDetalleRequest.TipoMulta
        });
      }

    }

    #endregion Constructors

    #region Handlers

    public class RegistrarMultaCommandHandler : BaseHandler, IRequestHandler<RegistrarMultaCommand, ApiResponseWrapper>
    {
      #region Constructors

      public RegistrarMultaCommandHandler(IUnitOfWork unitOfWork)
          : base(unitOfWork)
      {
      }

      #endregion Constructors

      #region Methods

      public async Task<ApiResponseWrapper> Handle(RegistrarMultaCommand command, CancellationToken cancellationToken)
      {

        // Obteniendo multas ya guardadas
        var multasGuardadas = await UnitOfWork.MultaRepository.GetByTramiteIdAsync(command.TramiteId);

        try
        {

          // Barriendo las multas que envío a guardar
          foreach (var item in command.ListaDetalleMultas)
          {

            // Determino si esta multa ya se encuentra guardada, para evitar que se duplique al guardar
            if (multasGuardadas == null || multasGuardadas.FirstOrDefault(m => m.TipoMulta.Trim() == item.TipoMulta.Trim() && m.FechaRegistro == item.FechaRegistro) == null)
            {
              // Si no está ya guardada, agregamos la nueva multa
              var multa = new Domain.Entities.Multa
              {
                Estado = item.Estado,
                FechaRegistro = item.FechaRegistro,
                Observacion = item.Observacion,
                TipoMulta = item.TipoMulta,
                TramiteId = command.TramiteId,
                Created = System.DateTime.Now,
                LastModified = System.DateTime.Now,
                CreatorId = command.UsuarioId,
                LastModifierId = command.UsuarioId

              };
              multa.AssignId();

              // Insertando en el repositorio
              var r = await UnitOfWork.MultaRepository.InsertAsync(multa);
              if (!r.Item1)
                throw new System.Exception(r.Item2);

            }

          }

          // Actualizando cambios
          var r2 = await UnitOfWork.SaveChangesAsync();

          if (!r2.Item1)
            throw new System.Exception(r2.Item2);

          var response = new ApiResponseWrapper(HttpStatusCode.OK, command.TramiteId);
          return response;
        }
        catch (System.Exception ex)
        {
          var response = new ApiResponseWrapper(HttpStatusCode.BadRequest, ex.Message == null ? ex.InnerException : ex.Message);
          return response;
        }

      }

      #endregion Methods
    }

    #endregion Handlers
  }

  public class RegistrarMultaCommandValidator : AbstractValidator<RegistrarMultaCommand>
  {
    public RegistrarMultaCommandValidator()
    {
      //RuleFor(e => e.Deadline)
      //    .NotEmpty().When(e => !string.IsNullOrEmpty(e.RecurrenceId)).WithMessage("{PropertyName} is required.")
      //    .NotNull().When(e => !string.IsNullOrEmpty(e.RecurrenceId)).WithMessage("{PropertyName} must not be null.")
      //    .GreaterThanOrEqualTo(e => e.StartDate).When(e => !string.IsNullOrEmpty(e.RecurrenceId)).WithMessage("{PropertyName} must be greater than or equal to the start date.");
    }
  }
}


//add-migration AddMulta -s Mre.Visas.Multa.Infrastructure
//update-database -s Mre.Visas.Multa.Infrastructure