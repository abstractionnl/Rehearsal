using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Rehearsal.WebApi.Infrastructure
{
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
                
                //context.Response = context.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
            }
            
            base.OnActionExecuting(context);
        }
    }
}