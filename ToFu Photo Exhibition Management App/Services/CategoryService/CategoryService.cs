namespace ToFu_Photo_Exhibition_Management_App.Services.CategoryService
{
	class CategoryService : ICategoryService
	{
		public List<CategoryResponseDto> Categories { get; } = new List<CategoryResponseDto>();
		public List<CategoryResponseDto> CategoriesWithAll { get; } = new List<CategoryResponseDto>();
		public bool IsSearch { get; set; } = false;
		public async Task GetCategories()
		{
			Categories.Clear();
			IsSearch = true;
			var result = await Api.Get<ServiceResponse<IEnumerable<CategoryResponseDto>>>("api/category");
			if (result != null && result.Data != null)
			{
				Categories.AddRange(result.Data);
				IsSearch = false;
			}
		}

		public async Task GetCategoriesWithAll()
		{
			CategoriesWithAll.Clear();
			IsSearch = true;
			var result = await Api.Get<ServiceResponse<IEnumerable<CategoryResponseDto>>>("api/category");
			if (result != null && result.Data != null)
			{
				CategoriesWithAll.Add(new CategoryResponseDto(0, "ALL"));
				CategoriesWithAll.AddRange(result.Data);
				IsSearch = false;
			}
		}
	}
}

