namespace ToFu_Photo_Exhibition_Management_App.Services.CarService
{
	class CarService : ICarService
	{
		public List<CarResponseDto> Cars { get; } = new List<CarResponseDto>();
		public async Task GetFilterCars(int categoryId, int manufacturerId, int teamId)
		{
			Cars.Clear();
			var result = await Api.Get<ServiceResponse<IEnumerable<CarResponseDto>>>($"api/car/category/{categoryId}/manufacturer/{manufacturerId}/team/{teamId}");
			if (result != null && result.Data != null)
			{
				Cars.AddRange(result.Data);
			}
		}
	}
}
