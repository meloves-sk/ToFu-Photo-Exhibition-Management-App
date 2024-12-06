namespace ToFu_Photo_Exhibition_Management_App.Services.ManufacturerService
{
	class ManufacturerService : IManufacturerService
	{
		public List<ManufacturerResponseDto> Manufacturers { get; } = new List<ManufacturerResponseDto>();
		public List<ManufacturerResponseDto> ManufacturersWithAll { get; } = new List<ManufacturerResponseDto>();
		public async Task GetManufacturers(int categoryId)
		{
			Manufacturers.Clear();
			var result = await Api.Get<ServiceResponse<IEnumerable<ManufacturerResponseDto>>>($"api/manufacturer/category/{categoryId}");
			if (result != null && result.Data != null)
			{
				Manufacturers.AddRange(result.Data);
			}
		}
		public async Task GetManufacturersWithAll(int categoryId)
		{
			ManufacturersWithAll.Clear();
			var result = await Api.Get<ServiceResponse<IEnumerable<ManufacturerResponseDto>>>($"api/manufacturer/category/{categoryId}");
			if (result != null && result.Data != null)
			{
				ManufacturersWithAll.Add(new ManufacturerResponseDto(0, "ALL"));
				ManufacturersWithAll.AddRange(result.Data);
			}
		}
	}
}
