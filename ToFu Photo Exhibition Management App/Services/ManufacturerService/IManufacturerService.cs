namespace ToFu_Photo_Exhibition_Management_App.Services.ManufacturerService
{
	public interface IManufacturerService
	{
		public List<ManufacturerResponseDto> Manufacturers { get; }
		Task GetFilterManufacturers(int categoryId);
	}
}
