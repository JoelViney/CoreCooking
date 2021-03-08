using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoreCooking.Api.Controllers
{
    [ApiController]
    [Route("api/v3/[controller]")]
    public class HashtagController
    {
        private readonly HashtagService _service;

        #region Contructors...

        public HashtagController(HashtagService service)
        {
            _service = service;
        }

        #endregion

        [HttpGet("GetList")]
        public async Task GetList()
        {
            var list = _sergive.GetList();
        }
    }
}
