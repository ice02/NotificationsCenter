using Microsoft.AspNetCore.Mvc;

namespace NotificationCenter.Infrastructure
{
    [Route("[controller]/[action]", Name = "[controller]_[action]")]
    public abstract class BaseController : Controller
    {
    }
}
