using Data.Core;
using SpeechApi.Models;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SpeechApi.Controllers
{
    [RoutePrefix("api/speech")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SpeechController : ApiController
    {
        private TextToSpeech _textToSpeech;
        private SpeechToText _speechToText;

        public SpeechController(TextToSpeech textToSpeech, SpeechToText speechToText)
        {
            _textToSpeech = textToSpeech;
            _speechToText = speechToText;
        }

        public SpeechController()
        {
            _textToSpeech = new TextToSpeech();
            _speechToText = new SpeechToText();
        }

        [Route("{word}")]
        public async Task<HttpResponseMessage> GetAudio(string word, LanguageEnum language = LanguageEnum.English)
        {
            var stream = await _textToSpeech.GetAudioAsync(word, language);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(stream);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = "sound.wav";
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("audio/wav");
            return response;

        }


        [HttpPost]
        public async Task<string> GetText()
        {
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            var file = provider.Contents.First();


            using (var stream = await file.ReadAsStreamAsync())
            {
                return await _speechToText.GetTextAsync(stream);
            }
        }


        [Route("Test/Test")]
        [HttpGet]
        public async Task<string> Test()
        {
            using (var fileStream = File.Open(@"D:\sounds\sound10.wav", FileMode.Open))
            {
                return await _speechToText.GetTextAsync(fileStream);
            }
        }


        [Route("{word}/ToFile")]
        [HttpGet]
        public async Task<IHttpActionResult> SaveAudio(string word, LanguageEnum language = LanguageEnum.English, string path = @"D:\\sounds\sound.wav")
        {

            await _textToSpeech.SaveAudioAsync(word, language, path);            
            return Ok();
        }

    }
}
