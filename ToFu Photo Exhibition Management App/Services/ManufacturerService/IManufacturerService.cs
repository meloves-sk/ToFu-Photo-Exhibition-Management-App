namespace ToFu_Photo_Exhibition_Management_App.Services.ManufacturerService
{
	public interface IManufacturerService
	{
		public List<ManufacturerResponseDto> Manufacturers { get; }
		public List<ManufacturerResponseDto> ManufacturersWithAll { get; }
		Task GetManufacturers(int categoryId);
		Task GetManufacturersWithAll(int categoryId);
	}
}
