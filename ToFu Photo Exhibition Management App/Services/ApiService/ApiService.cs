
using System.Security.Policy;

namespace ToFu_Photo_Exhibition_Management_App.Services.ApiService
{
	public class ApiService : IApiService
	{
		private readonly HttpClient _httpClient;
		private readonly string url = "APIの接続先";
		public ApiService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<T> Get<T>(string arg)
		{
			return await _httpClient.GetFromJsonAsync<T>($"{url}/{arg}");
		}

		public async Task<ServiceResponse<bool>> Post<T>(string arg, T request)
		{
			var result = await _httpClient.PostAsJsonAsync($"{url}/{arg}", request);
			return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
		}

		public async Task<ServiceResponse<bool>> Put<T>(string arg, T request)
		{
			var result = await _httpClient.PutAsJsonAsync($"{url}/{arg}", request);
			return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
		}
		public async Task<ServiceResponse<bool>> Delete(string arg)
		{
			var result = await _httpClient.DeleteAsync($"{url}/{arg}");
			return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
		}
	}
}
