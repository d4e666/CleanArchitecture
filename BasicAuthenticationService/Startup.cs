#region Usings

using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

#endregion

namespace BasicAuthenticationService
{
    public class Startup
    {
        #region Constructors

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        #endregion

        #region Properties

        public IConfiguration Configuration { get; }

        #endregion

        #region Methods

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Versioning

            services.AddApiVersioning(
                o =>
                {
                    o.AssumeDefaultVersionWhenUnspecified = true;
                    o.DefaultApiVersion = new ApiVersion(1, 0);
                });

            #endregion

            #region Swagger

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc(
                        "v1.0",
                        new Info
                        {
                            Version = "v1.0",
                            Title = "Authentication",
                            Description = "v1 API Description",
                            TermsOfService = "Terms of usage v1"
                        });
                    options.SwaggerDoc(
                        "v2.0",
                        new Info
                        {
                            Version = "v2.0",
                            Title = "Authentication",
                            Description = "v2 API Description",
                            TermsOfService = "Terms of usage v2"
                        });

                    // This call remove version from parameter, without it we will have version as parameter 
                    // for all endpoints in swagger UI
                    options.OperationFilter<RemoveVersionFromParameter>();

                    // This make replacement of v{version:apiVersion} to real version of corresponding swagger doc.
                    options.DocumentFilter<ReplaceVersionWithExactValueInPath>();

                    // This on used to exclude endpoint mapped to not specified in swagger version.
                    // In this particular example we exclude 'GET /api/v2/Values/otherget/three' endpoint,
                    // because it was mapped to v3 with attribute: MapToApiVersion("3")
                    options.DocInclusionPredicate(
                        (version, desc) =>
                        {
                            if (!desc.TryGetMethodInfo(out var methodInfo)) return false;
                            var versions = methodInfo.DeclaringType
                                                     .GetConstructors()
                                                     .SelectMany(
                                                         x =>
                                                             x.DeclaringType.CustomAttributes.Where(
                                                                  y =>
                                                                      y.AttributeType == typeof(ApiVersionAttribute))
                                                              .SelectMany(
                                                                  z =>
                                                                      z.ConstructorArguments.Select(i => i.Value)));

                            return versions.Any(v => $"v{v.ToString()}" == version);
                        });
                });

            #endregion

            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            #region Swagger

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(
                configuration =>
                {
                    configuration.SwaggerEndpoint("/swagger/v1.0/swagger.json", "v1.0");
                    configuration.SwaggerEndpoint("/swagger/v2.0/swagger.json", "v2.0");
                });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            #endregion

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        #endregion
    }

    public class RemoveVersionFromParameter : IOperationFilter
    {
        #region Methods

        public void Apply(Operation operation, OperationFilterContext context)
        {
            var versionParameter = operation.Parameters.Single(p => p.Name == "version");
            operation.Parameters.Remove(versionParameter);
        }

        #endregion
    }

    public class ReplaceVersionWithExactValueInPath : IDocumentFilter
    {
        #region Methods

        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Paths = swaggerDoc.Paths.ToDictionary(
                path => path.Key.Replace("v{version}", swaggerDoc.Info.Version),
                path => path.Value
            );
        }

        #endregion
    }
}