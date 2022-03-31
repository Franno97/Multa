using FluentValidation;
using MediatR;
using Mre.Visas.Multa.Application.Wrappers;
using Mre.Visas.Multa.Application.Multa.Requests;
using Mre.Visas.Multa.Application.Shared.Handlers;
using Mre.Visas.Multa.Application.Shared.Interfaces;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Mre.Visas.Multa.Application.Multa.Queries
{
  public class ConsultarMultasPorTramiteIdQuery : ConsultarMultasPorTramiteIdRequest, IRequest<ApiResponseWrapper>
  {
    #region Constructors

    public ConsultarMultasPorTramiteIdQuery(ConsultarMultasPorTramiteIdRequest request)
    {
      TramiteId = request.TramiteId;
    }

    #endregion Constructors

    #region Handlers

    public class ConsultarMultasQueryHandler : BaseHandler, IRequestHandler<ConsultarMultasPorTramiteIdQuery, ApiResponseWrapper>
    {
      #region Constructors

      public ConsultarMultasQueryHandler(IUnitOfWork unitOfWork)
          : base(unitOfWork)
      {
      }

      #endregion Constructors

      #region Methods

      public async Task<ApiResponseWrapper> Handle(ConsultarMultasPorTramiteIdQuery query, CancellationToken cancellationToken)
      {
        try
        {

          var multas = await UnitOfWork.MultaRepository.GetByTramiteIdAsync(query.TramiteId);

          var response = new ApiResponseWrapper(HttpStatusCode.OK, multas);

          return response;
        }
        catch (System.Exception ex)
        {
          return new ApiResponseWrapper(HttpStatusCode.BadRequest, ex.Message == null ? ex.InnerException : ex.Message);
        }
      }

      #endregion Methods
    }

    #endregion Handlers
  }

  public class ConsultarMultasQueryValidator : AbstractValidator<ConsultarMultasPorTramiteIdQuery>
  {
    public ConsultarMultasQueryValidator()
    {
      //RuleFor(e => e.ProjectId).Must(e => e.Length.Equals(38)).When(e => !string.IsNullOrEmpty(e.ProjectId)).WithMessage("{PropertyName} must be exactly 38 characters.");
    }
  }


  public class ConsultarMultasPorTramiteIdObservacionQuery : ConsultarMultasPorTramiteIdObservacionRequest, IRequest<ApiResponseWrapper>
  {
    #region Constructors

    public ConsultarMultasPorTramiteIdObservacionQuery(ConsultarMultasPorTramiteIdObservacionRequest request)
    {
      TramiteId = request.TramiteId;
      Observacion = request.Observacion;
    }

    #endregion Constructors

    #region Handlers

    public class ConsultarMultasObservacionQueryHandler : BaseHandler, IRequestHandler<ConsultarMultasPorTramiteIdObservacionQuery, ApiResponseWrapper>
    {
      #region Constructors

      public ConsultarMultasObservacionQueryHandler(IUnitOfWork unitOfWork)
          : base(unitOfWork)
      {
      }

      #endregion Constructors

      #region Methods

      public async Task<ApiResponseWrapper> Handle(ConsultarMultasPorTramiteIdObservacionQuery query, CancellationToken cancellationToken)
      {
        try
        {
          var multas = await UnitOfWork.MultaRepository.GetByTramiteIdObservacionAsync(query.TramiteId, query.Observacion);

          var response = new ApiResponseWrapper(HttpStatusCode.OK, multas);

          return response;
        }
        catch (System.Exception ex)
        {
          return new ApiResponseWrapper(HttpStatusCode.BadRequest, ex.Message == null ? ex.InnerException : ex.Message);
        }

      }

      #endregion Methods
    }

    #endregion Handlers
  }

  public class ConsultarMultasObservacionQueryValidator : AbstractValidator<ConsultarMultasPorTramiteIdObservacionQuery>
  {
    public ConsultarMultasObservacionQueryValidator()
    {
      //RuleFor(e => e.ProjectId).Must(e => e.Length.Equals(38)).When(e => !string.IsNullOrEmpty(e.ProjectId)).WithMessage("{PropertyName} must be exactly 38 characters.");
    }
  }

}