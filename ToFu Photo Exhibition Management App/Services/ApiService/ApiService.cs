using Microsoft.Extensions.Configuration;

namespace ToFu_Photo_Exhibition_Management_App.Services.ApiService
{
	public class ApiService : IApiService
	{
		private IConfiguration _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
		private readonly HttpClient _httpClient;
		private readonly string _url;
		public ApiService(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_url = _configuration.GetSection("ApiUrl").Value;
		}
		public async Task<T> Get<T>(string arg)
		{
			return await _httpClient.GetFromJsonAsync<T>($"{_url}/{arg}");
		}

		public async Task<ServiceResponse<bool>> Post<T>(string arg, T request)
		{
			var result = await _httpClient.PostAsJsonAsync($"{_url}/{arg}", request);
			return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
		}

		public async Task<ServiceResponse<bool>> Put<T>(string arg, T request)
		{
			var result = await _httpClient.PutAsJsonAsync($"{_url}/{arg}", request);
			return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
		}
		public async Task<ServiceResponse<bool>> Delete(string arg)
		{
			var result = await _httpClient.DeleteAsync($"{_url}/{arg}");
			return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
		}
	}
}
