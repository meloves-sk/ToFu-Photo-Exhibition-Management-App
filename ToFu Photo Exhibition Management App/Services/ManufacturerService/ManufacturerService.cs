namespace ToFu_Photo_Exhibition_Management_App.Services.ManufacturerService
{
	class ManufacturerService : IManufacturerService
	{
		private readonly IApiService _apiService;
		public List<ManufacturerResponseDto> Manufacturers { get; } = new List<ManufacturerResponseDto>();
		public List<ManufacturerResponseDto> ManufacturersWithAll { get; } = new List<ManufacturerResponseDto>();
		public bool IsSearch { get; set; } = false;
		public ManufacturerService(IApiService apiService)
		{
			_apiService = apiService;
		}
		public async Task GetManufacturers(int categoryId)
		{
			Manufacturers.Clear();
			IsSearch = true;
			var result = await _apiService.Get<ServiceResponse<IEnumerable<ManufacturerResponseDto>>>($"api/manufacturer/category/{categoryId}");
			if (result != null && result.Data != null)
			{
				Manufacturers.AddRange(result.Data);
				IsSearch = false;
			}
		}
		public async Task GetManufacturersWithAll(int categoryId)
		{
			ManufacturersWithAll.Clear();
			IsSearch = true;
			var result = await _apiService.Get<ServiceResponse<IEnumerable<ManufacturerResponseDto>>>($"api/manufacturer/category/{categoryId}");
			if (result != null && result.Data != null)
			{
				ManufacturersWithAll.Add(new ManufacturerResponseDto(0, "ALL"));
				ManufacturersWithAll.AddRange(result.Data);
				IsSearch = false;
			}
		}
		public async Task<ServiceResponse<bool>> AddManufacturer(ManufacturerRequestDto request)
		{
			return await _apiService.Post($"api/manufacturer", request);
		}
		public async Task<ServiceResponse<bool>> UpdateManufacturer(ManufacturerRequestDto request)
		{
			return await _apiService.Put($"api/manufacturer", request);
		}
		public async Task<ServiceResponse<bool>> DeleteManufacturer(int manufacturerId)
		{
			return await _apiService.Delete($"api/manufacturer/{manufacturerId}");
		}
	}
}
