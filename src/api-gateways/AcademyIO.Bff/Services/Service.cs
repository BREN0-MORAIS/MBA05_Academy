using AcademyIO.Core.Communication;
using System.Net;
using System.Text;
using System.Text.Json;

namespace AcademyIO.Bff.Services
{
    public abstract class Service
    {
        protected StringContent GetContent(object dado)
        {
            return new StringContent(
                JsonSerializer.Serialize(dado),
                Encoding.UTF8,
                "application/json");
        }

        protected async Task<T> DeserializeResponse<T>(HttpResponseMessage ResponseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var content = await ResponseMessage.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(content))
            {
                return default!;
            }

            return JsonSerializer.Deserialize<T>(content, options)!;
        }

        protected bool ManageHttpResponse(HttpResponseMessage Response)
        {
            if (Response.StatusCode == HttpStatusCode.BadRequest) return false;

            Response.EnsureSuccessStatusCode();
            return true;
        }

        protected ResponseResult Ok()
        {
            return new ResponseResult();
        }
    }
}