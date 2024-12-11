namespace ToFu_Photo_Exhibition_Management_App
{
	public static class Api
	{
		private static string url = "APIの接続先";
		public static async Task<T> Get<T>(string arg)
		{
			using (HttpClient client = new HttpClient())
			{
				return await client.GetFromJsonAsync<T>($"{url}/{arg}");
			}
		}

		public static async Task<ServiceResponse<bool>> Post<T>(string arg, T request)
		{
			using (HttpClient client = new HttpClient())
			{
				var result = await client.PostAsJsonAsync($"{url}/{arg}", request);
				return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
			}
		}

		public static async Task<ServiceResponse<bool>> Put<T>(string arg, T request)
		{
			using (HttpClient client = new HttpClient())
			{
				var result = await client.PutAsJsonAsync($"{url}/{arg}", request);
				return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
			}
		}

		public static async Task<ServiceResponse<bool>> Delete(string arg)
		{
			using (HttpClient client = new HttpClient())
			{
				return await client.DeleteFromJsonAsync<ServiceResponse<bool>>($"{url}/{arg}");
			}
		}
	}
}
