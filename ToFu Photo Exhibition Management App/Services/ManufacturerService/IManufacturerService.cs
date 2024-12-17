namespace ToFu_Photo_Exhibition_Management_App.Services.ManufacturerService
{
	public interface IManufacturerService
	{
		public List<ManufacturerResponseDto> Manufacturers { get; }
		public List<ManufacturerResponseDto> ManufacturersWithAll { get; }
		public bool IsSearch { get; set; }
		Task GetManufacturers(int categoryId);
		Task GetManufacturersWithAll(int categoryId);
		Task<ServiceResponse<bool>> AddManufacturer(ManufacturerRequestDto request);
		Task<ServiceResponse<bool>> UpdateManufacturer(ManufacturerRequestDto request);
		Task<ServiceResponse<bool>> DeleteManufacturer(int manufacturerId);
	}
}
