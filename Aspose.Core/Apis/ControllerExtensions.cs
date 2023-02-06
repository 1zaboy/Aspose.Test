using Aspose.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aspose.Core.Apis;

public static class ControllerExtensions
{
    public static ActionResult<T> ResolveResult<T>(this ControllerBase controller, ExecutionResult<T> executionResult)
    {
        if (controller == null) throw new ArgumentNullException(nameof(controller));
        
        if (executionResult.Success)
        {
            return controller.Ok(executionResult.Result);
        }

        return controller.BadRequest(executionResult);
    }

}