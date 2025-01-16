namespace ToFu_Photo_Exhibition_Management_App.Services.ApiService
{
	public interface IApiService
	{
		Task<T> Get<T>(string arg);
		Task<ServiceResponse<bool>> Post<T>(string arg, T request);
		Task<ServiceResponse<bool>> Put<T>(string arg, T request);
		Task<ServiceResponse<bool>> Delete(string arg);
	}
}
