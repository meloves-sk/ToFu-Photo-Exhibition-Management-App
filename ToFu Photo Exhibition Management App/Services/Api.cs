using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ToFu_Photo_Exhibition_Management_App.Shared.Dto.Response;

namespace ToFu_Photo_Exhibition_Management_App
{
	public static class Api
	{
		private static string url = "APIの接続先";
		public static async Task<T> Get<T>(string args)
		{
			using (HttpClient client = new HttpClient())
			{
				return await client.GetFromJsonAsync<T>($"{url}/{args}");
			}
		}

		public static async Task<ServiceResponse<bool>> Post<T>(string args, T request)
		{
			using (HttpClient client = new HttpClient())
			{
				var result = await client.PostAsJsonAsync($"{url}/{args}", request);
				return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
			}
		}

		public static async Task<ServiceResponse<bool>> Put<T>(string args, T request)
		{
			using (HttpClient client = new HttpClient())
			{
				var result = await client.PutAsJsonAsync($"{url}/{args}", request);
				return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
			}
		}
	}
}
