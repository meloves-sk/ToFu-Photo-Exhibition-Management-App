using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToFu_Photo_Exhibition_Management_App.Shared.Dto.Response;

namespace ToFu_Photo_Exhibition_Management_App.Services.CategoryService
{
	class CategoryService : ICategoryService
	{
		public async Task<ServiceResponse<IEnumerable<CategoryResponseDto>>> GetCategories()
		{
			return await Api.Get<ServiceResponse<IEnumerable<CategoryResponseDto>>>("api/category");
		}
	}
}
