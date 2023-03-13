using APIChatGPT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace APIChatGPT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public TutorController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [Route("/Tutor-English")]
        [HttpGet]
        public async Task<IActionResult> GetEnglish(string text, [FromServices] IConfiguration configuration)
        {
            var token = configuration.GetValue<string>("SecretKeyChatGpt");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //estancia
            var model = new ChatGptInputModels(text); //text = pergunta.
            model.prompt = $"Correct this english phrase:{text}";
            //transforma em string formatado json, que é como é aceita a requisição
            var requestBody = JsonSerializer.Serialize(model);
            //inicializa em json para passar na requisição
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            //faz a chamada post no chatGpt
            var response = await _httpClient.PostAsync("https://api.openai.com/v1/completions", content);

            var result = await response.Content.ReadFromJsonAsync<ChatGptResponseModels>();

            var promptResponse = result.choices.First();

            return Ok(promptResponse.text.Replace("\n", "").Replace("\t", ""));

        }

        [Route("/Tutor-Portuguese")]
        [HttpGet]
        public async Task<IActionResult> GetPortuguese(string text, [FromServices] IConfiguration configuration)
        {
            var token = configuration.GetValue<string>("SecretKeyChatGpt");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //estancia
            var model = new ChatGptInputModels(text); //text = pergunta.
            model.prompt = $"Correct this portuguese phrase:{text}";
            //transforma em string formatado json, que é como é aceita a requisição
            var requestBody = JsonSerializer.Serialize(model);
            //inicializa em json para passar na requisição
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            //faz a chamada post no chatGpt
            var response = await _httpClient.PostAsync("https://api.openai.com/v1/completions", content);

            var result = await response.Content.ReadFromJsonAsync<ChatGptResponseModels>();

            var promptResponse = result.choices.First();

            return Ok(promptResponse.text.Replace("\n", "").Replace("\t", ""));

        }

        [Route("/Tutor-Travel")]
        [HttpGet]
        public async Task<IActionResult> GetTravel(string city, [FromServices] IConfiguration configuration)
        {
            var token = configuration.GetValue<string>("SecretKeyChatGpt");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //estancia
            var model = new ChatGptInputModels(city); //text = pergunta.
            model.prompt = $"create a tour list to travel on: {city}";
            //transforma em string formatado json, que é como é aceita a requisição
            var requestBody = JsonSerializer.Serialize(model);
            //inicializa em json para passar na requisição
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            //faz a chamada post no chatGpt
            var response = await _httpClient.PostAsync("https://api.openai.com/v1/completions", content);

            var result = await response.Content.ReadFromJsonAsync<ChatGptResponseModels>();

            var promptResponse = result.choices.First();

            return Ok(promptResponse.text.Replace("\n", "").Replace("\t", ""));

        }
    }
}
