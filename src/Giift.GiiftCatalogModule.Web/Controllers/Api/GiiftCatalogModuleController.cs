using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Giift.GiiftCatalogModule.Core;

namespace Giift.GiiftCatalogModule.Web.Controllers.Api
{
    [Route("api/giift-catalog-module")]
    public class GiiftCatalogModuleController : Controller
    {
        // GET: api/giift-catalog-module
        /// <summary>
        /// Get message
        /// </summary>
        /// <remarks>Return "Hello world!" message</remarks>
        [HttpGet]
        [Route("")]
        [Authorize(ModuleConstants.Security.Permissions.Read)]
        public ActionResult<string> Get()
        {
            return Ok(new { result = "Hello world!" });
        }
    }
}
