using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using WebFramework.ActionFilter;

namespace WebFramework.Api
{
    [ApiResultFilter]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseController:Controller
    {
    }
}
