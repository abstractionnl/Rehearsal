using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Rehearsal.WebApi.Infrastructure
{
    public class ValidateActionParametersAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;

            if (descriptor != null)
            {
                foreach (var parameter in descriptor.MethodInfo.GetParameters())
                {
                    if (parameter.CustomAttributes.Any(attr => attr.AttributeType == typeof(FromBodyAttribute)))
                    {
                        if (!context.ActionArguments.ContainsKey(parameter.Name) || context.ActionArguments[parameter.Name] == null)
                        {
                            context.ModelState.AddModelError("", "The request body was empty.");
                        }
                    }
                }
            }
            
            base.OnActionExecuting(context);
        }
    }
}