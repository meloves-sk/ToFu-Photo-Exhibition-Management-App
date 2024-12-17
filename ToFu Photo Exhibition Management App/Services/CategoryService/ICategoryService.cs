namespace ToFu_Photo_Exhibition_Management_App.Services.CategoryService
{
	public interface ICategoryService
	{
		List<CategoryResponseDto> Categories { get; }
		List<CategoryResponseDto> CategoriesWithAll { get; }
		bool IsSearch { get; set; }
		Task GetCategories();
		Task GetCategoriesWithAll();
	}
}
