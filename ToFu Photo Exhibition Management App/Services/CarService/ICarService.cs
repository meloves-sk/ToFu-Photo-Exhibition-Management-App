namespace ToFu_Photo_Exhibition_Management_App.Services.CarService
{
	public interface ICarService
	{
		public List<CarResponseDto> Cars { get; }
		public List<CarResponseDto> CarsWithAll { get; }
		Task GetCars(int categoryId, int manufacturerId, int teamId);
		Task GetCarsWithAll(int categoryId, int manufacturerId, int teamId);
	}
}
