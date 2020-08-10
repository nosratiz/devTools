using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DevTools.Api.Areas.Admin
{
    [Area("Admin")]
    [Route("[Area]/[Controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class BaseAdminController : Controller
    {
    }
}
