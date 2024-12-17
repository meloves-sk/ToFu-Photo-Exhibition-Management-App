namespace ToFu_Photo_Exhibition_Management_App.Services.CarService
{
	public interface ICarService
	{
		List<CarResponseDto> Cars { get; }
		List<CarResponseDto> CarsWithAll { get; }
		bool IsSearch { get; set; }
		Task GetCars(int categoryId, int manufacturerId, int teamId);
		Task GetCarsWithAll(int categoryId, int manufacturerId, int teamId);
		Task<ServiceResponse<bool>> AddCar(CarRequestDto request);
		Task<ServiceResponse<bool>> UpdateCar(CarRequestDto request);
		Task<ServiceResponse<bool>> DeleteCar(int carId);
	}
}
