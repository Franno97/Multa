using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mre.Sb.Auditar;
using Mre.Visas.Multa.Api.Extensions;
using Mre.Visas.Multa.Application;
using Mre.Visas.Multa.Infrastructure;
using Mre.Visas.Multa.Infrastructure.Persistence.Contexts;
using Newtonsoft.Json;

namespace Mre.Visas.Multa.Api
{
  public class Startup
  {
    #region Constructors

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    #endregion Constructors

    #region Properties

    public IConfiguration Configuration { get; }

    #endregion Properties

    #region Methods

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddInfrastructureLayer(Configuration);
      services.AddApplicationLayer();

      services.AddControllers().AddNewtonsoftJson(options =>
      {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
      });

      //ADD CROSS
      services.AddCors();

      services.AddSwaggerExtension("Mre.Visas.Multa.Api", "v1");

      services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

      services.AddMvc();

      //cache redis
      services.AddStackExchangeRedisCache(options =>
      {
        options.Configuration = Configuration.GetConnectionString("Redis");
        options.InstanceName = "Mre.Visas.Multa:";
      });


      services.AgregarAuditoria(Configuration);

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      //app.UseHttpsRedirection();
      app.UseRouting();
      app.UseSwaggerExtension("Mre.Visas.Multa.Api");
      app.UseApiExceptionMiddleware();

      //ADD CROSS
      // global cors policy
      app.UseCors(x => x
          .AllowAnyMethod()
          .AllowAnyHeader()
          .SetIsOriginAllowed(origin => true) // allow any origin
          .AllowCredentials()
          ); // allow credentials

      app.UseEndpoints(endpoints => endpoints.MapControllers());

      app.UsarAuditoria<ApplicationDbContext>();
    }

    #endregion Methods
  }
}