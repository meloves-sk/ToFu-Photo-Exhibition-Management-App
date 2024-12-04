namespace ToFu_Photo_Exhibition_Management_App.Services.ManufacturerService
{
	class ManufacturerService : IManufacturerService
	{
		public List<ManufacturerResponseDto> Manufacturers { get; } = new List<ManufacturerResponseDto>();
		public async Task GetFilterManufacturers(int categoryId)
		{
			Manufacturers.Clear();
			var result = await Api.Get<ServiceResponse<IEnumerable<ManufacturerResponseDto>>>($"api/manufacturer/category/{categoryId}");
			if (result != null && result.Data != null)
			{
				Manufacturers.AddRange(result.Data);
			}
		}
	}
}
