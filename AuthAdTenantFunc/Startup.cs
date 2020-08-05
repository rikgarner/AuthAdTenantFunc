using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
[assembly: FunctionsStartup(typeof(AuthAdTenantFunc.Startup))]
namespace AuthAdTenantFunc
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddMediatR(typeof(CompanyListFetchQueryHandler));
            //builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            //builder.Services.AddSingleton<IValidator<GetUserQuery>, GetUserQueryValidator>();

            //builder.Services.AddSingleton<IHttpFunctionExecutor, HttpFunctionExecutor>();
        }
    }
}
