using System;
using System.Web.Http;

namespace AspNetHealthCheck
{
    public static class HealthCheckExtensions
    {
        private const string DefaultRouteTemplate = "healthcheck";

        public static void EnableHealthChecks(
            this HttpConfiguration httpConfig,
            string applicationVersion = null,
            Action<HealthCheckConfig> configure = null)
        {
            EnableHealthChecks(httpConfig, DefaultRouteTemplate, applicationVersion, configure);
        }

        public static void EnableHealthChecks(
            this HttpConfiguration httpConfig,
            string routeTemplate,
            string applicationVersion = null,
            Action<HealthCheckConfig> configure = null
            )
        {
            var config = new HealthCheckConfig();
            if (configure != null) configure(config);

            httpConfig.Routes.MapHttpRoute(
                name: "health_check",
                routeTemplate: routeTemplate,
                defaults: null,
                constraints: null,
                handler: new HealthCheckHandler(config, applicationVersion)
                );
        }

    }
}
