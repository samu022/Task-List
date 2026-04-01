using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_tareas.Controllers
{
    public class DefaultController : Controller
    {
        // GET: DefaultController
        [HttpGet]
        [Route("api/[controller]")]
        public string Get()
        {
            return "Aplicacion corriendo..";
        }

        
    }
}
