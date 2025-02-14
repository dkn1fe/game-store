using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GameStore.Authentication
{
    public class ApiKeyAuthFilter : IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;
        private readonly string[] _allowedRoles;

        public ApiKeyAuthFilter(IConfiguration configuration, params string[] allowedRoles)
        {
            _configuration = configuration;
            _allowedRoles = allowedRoles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated || !_allowedRoles.Any(role => user.IsInRole(role)))
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ApiKeyAuthWithRolesAttribute : Attribute, IFilterFactory
    {
        public string[] Roles { get; }
        public bool IsReusable => false;

        public ApiKeyAuthWithRolesAttribute(params string[] roles)
        {
            Roles = roles;
        }

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            return new ApiKeyAuthFilter(configuration, Roles);
        }
    }
}
