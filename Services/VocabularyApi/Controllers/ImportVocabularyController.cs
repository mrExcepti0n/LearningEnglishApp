using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VocabularyApi.Services;

namespace VocabularyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportVocabularyController : ControllerBase
    {
        private readonly VocabularyLoader _loader;

        public ImportVocabularyController(VocabularyLoader loader)
        {
            _loader = loader;
        }


        [HttpPost("Load")]
        public async Task<ActionResult> LoadDictionary(IFormFile fromFile)
        {
            await _loader.LoadAsync(fromFile.OpenReadStream());
            return Ok();
        }
    }
}
