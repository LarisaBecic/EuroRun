using System.Text.Json;

namespace EuroRunAPI.Authentification.Helpers
{
    public class VerifyCaptchaHelper
    {
        private readonly IConfiguration _configuration;

        public VerifyCaptchaHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> VerifyCaptcha(string token)
        {
            var secret = _configuration["GoogleReCaptcha:SecretKey"];

            using var httpClient = new HttpClient();

            var response = await httpClient.PostAsync(
                $"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={token}",
                null);

            var jsonString = await response.Content.ReadAsStringAsync();

            var captchaResponse = JsonSerializer.Deserialize<RecaptchaResponse>(
                jsonString,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
            });

            return captchaResponse?.Success == true;
        }

        public class RecaptchaResponse
        {
            public bool Success { get; set; }
            public string[] ErrorCodes { get; set; }
        }
    }
}
