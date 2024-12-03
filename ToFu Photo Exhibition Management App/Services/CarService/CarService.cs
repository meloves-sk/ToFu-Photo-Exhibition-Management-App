using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToFu_Photo_Exhibition_Management_App.Shared.Dto.Response;

namespace ToFu_Photo_Exhibition_Management_App.Services.CarService
{
	class CarService : ICarService
	{
		public async Task<ServiceResponse<IEnumerable<CarResponseDto>>> GetFilterCars(int categoryId, int manufacturerId, int teamId)
		{
			return await Api.Get<ServiceResponse<IEnumerable<CarResponseDto>>>($"api/car/category/{categoryId}/manufacturer/{manufacturerId}/team/{teamId}");
		}
	}
}
