namespace ToFu_Photo_Exhibition_Management_App.Services.CarService
{
	public interface ICarService
	{
		public List<CarResponseDto> Cars { get; }
		Task GetFilterCars(int categoryId, int manufacturerId, int teamId);
	}
}
