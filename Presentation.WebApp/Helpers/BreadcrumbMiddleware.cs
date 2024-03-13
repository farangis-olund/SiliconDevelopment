namespace Presentation.WebApp.Helpers;

public class BreadcrumbMiddleware
{
	private readonly RequestDelegate _next;

	public BreadcrumbMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task Invoke(HttpContext context)
	{
		var controller = context.GetRouteValue("controller")?.ToString();
		var action = context.GetRouteValue("action")?.ToString();

		context.Items["BreadcrumbController"] = controller;
		context.Items["BreadcrumbAction"] = action;

		await _next(context);
	}
}
