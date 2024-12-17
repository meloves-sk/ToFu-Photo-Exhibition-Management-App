namespace ToFu_Photo_Exhibition_Management_App.Services.CarService
{
	class CarService : ICarService
	{
		public List<CarResponseDto> Cars { get; } = new List<CarResponseDto>();
		public List<CarResponseDto> CarsWithAll { get; } = new List<CarResponseDto>();
		public bool IsSearch { get; set; } = false;
		public async Task GetCars(int categoryId, int manufacturerId, int teamId)
		{
			Cars.Clear();
			IsSearch = true;
			var result = await Api.Get<ServiceResponse<IEnumerable<CarResponseDto>>>($"api/car/category/{categoryId}/manufacturer/{manufacturerId}/team/{teamId}");
			if (result != null && result.Data != null)
			{
				Cars.AddRange(result.Data);
				IsSearch = false;
			}
		}
		public async Task GetCarsWithAll(int categoryId, int manufacturerId, int teamId)
		{
			CarsWithAll.Clear();
			IsSearch = true;
			var result = await Api.Get<ServiceResponse<IEnumerable<CarResponseDto>>>($"api/car/category/{categoryId}/manufacturer/{manufacturerId}/team/{teamId}");
			if (result != null && result.Data != null)
			{
				CarsWithAll.Add(new CarResponseDto(0, "ALL", 0, 0, string.Empty, string.Empty, string.Empty));
				CarsWithAll.AddRange(result.Data);
				IsSearch = false;
			}
		}
		public async Task<ServiceResponse<bool>> AddCar(CarRequestDto request)
		{
			return await Api.Post("api/car", request);
		}
		public async Task<ServiceResponse<bool>> UpdateCar(CarRequestDto request)
		{
			return await Api.Post("api/car", request);
		}
		public async Task<ServiceResponse<bool>> DeleteCar(int carId)
		{
			return await Api.Delete($"api/car/{carId}");
		}
	}
}
