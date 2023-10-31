using System.Web.Http;
using System.Web.Http.Cors;

namespace CarSpeedDataApp
{
	public class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// New code
			var cors = new EnableCorsAttribute("*", "*", "*");
			config.EnableCors(cors);

			// Map attribute routes
			config.MapHttpAttributeRoutes();

			// Map default route
			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
				);
		}
	}
}
