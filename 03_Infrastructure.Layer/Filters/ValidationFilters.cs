using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace _03_Infrastructure.Layer.Filters
{
    public class ValidationFilters : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(x => x.Value.Errors.Any())
                    .ToDictionary(x => x.Key, x => x.Value.Errors.Select(x => x.ErrorMessage))
                    .ToArray();

            }
            await next();
        }
    }
}
