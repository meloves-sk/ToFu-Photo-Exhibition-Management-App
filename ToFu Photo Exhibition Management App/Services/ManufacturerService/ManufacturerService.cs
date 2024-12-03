using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToFu_Photo_Exhibition_Management_App.Shared.Dto.Response;

namespace ToFu_Photo_Exhibition_Management_App.Services.ManufacturerService
{
	class ManufacturerService : IManufacturerService
	{
		public async Task<ServiceResponse<IEnumerable<ManufacturerResponseDto>>> GetFilterManufacturers(int categoryId)
		{
			return await Api.Get<ServiceResponse<IEnumerable<ManufacturerResponseDto>>>($"api/manufacturer/category/{categoryId}");
		}
	}
}
