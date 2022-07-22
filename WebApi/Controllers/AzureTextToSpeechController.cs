using AzureSpeech;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class AzureTextToSpeechController : ControllerBase
    {
        private readonly AzureTextToSpeechService _AzureTextToSpeechService;

        public AzureTextToSpeechController(AzureTextToSpeechService azureTextToSpeechService)
        {
            _AzureTextToSpeechService = azureTextToSpeechService;
        }


        [HttpPost]
        //如果要传入简单的类型，比如int 和 string，则在Postman中要把内容加上双引号。
        public IActionResult Speak([FromBody]string text)
        {
            _AzureTextToSpeechService.TextToSpeechAsync(text);
            return Ok(text);
        }

        [HttpGet]
        public IActionResult Speak()
        {
            return Ok("Hello World.");
        }
    }
}
