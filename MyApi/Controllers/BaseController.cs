using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Filters;

namespace MyApi.Controllers
{
    [Route("[controller]")]
    [ApiResultFilter]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}
