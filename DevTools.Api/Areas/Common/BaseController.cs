using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DevTools.Api.Areas.Common
{
    [Route("[Controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class BaseController : Controller
    {
    }
}
