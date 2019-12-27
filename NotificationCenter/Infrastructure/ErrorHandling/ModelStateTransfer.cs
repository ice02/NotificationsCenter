using Microsoft.AspNetCore.Mvc.Filters;

namespace NotificationCenter.Infrastructure.ErrorHandling
{
    public abstract class ModelStateTransfer : ActionFilterAttribute
    {
        protected const string Key = nameof(ModelStateTransfer);
    }
}